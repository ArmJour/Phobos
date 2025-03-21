using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    // ========== REFERENSI OBJECT ==========
    [Header("Object References")]
    [SerializeField] private GameObject playerUI;
    [SerializeField] private Boss boss1;
    [SerializeField] private Boss boss2;
    [SerializeField] private PlayerCombatActions playerActions;

    // ========== UI REFERENCES ==========
    [Header("UI References")]
    [SerializeField] private Slider fearMeterSlider;
    [SerializeField] private Text turnText;
    [SerializeField] private Text gameOverText;

    // ========== SYSTEM SETTINGS ==========
    [Header("Settings")]
    [SerializeField] private float turnDelay = 1f;
    [SerializeField] private int maxFear = 15;

    // ========== STATUS BATTLE ==========
    public Boss currentBoss;
    public bool isPlayerTurn = true;
    private int currentBossIndex;
    private int extraTurns = 0;

    void Start()
    {
        StartCoroutine(StartBattle());
    }

    // ========== INISIALISASI BATTLE ==========
    IEnumerator StartBattle()
    {
        // Ambil data boss yang diinteract dari scene utama
        currentBossIndex = PlayerPrefs.GetInt("CurrentBossIndex", 0);
        currentBoss = (currentBossIndex == 0) ? boss1 : boss2;

        // Setup UI awal
        UpdateFearUI(0);
        turnText.text = "Persiapan Battle...";
        playerUI.SetActive(false);

        // Tunggu 1 detik untuk efek dramatis
        yield return new WaitForSeconds(1f);

        StartPlayerTurn();
    }

    // ========== GILIRAN PLAYER ==========
    public void StartPlayerTurn()
    {
        isPlayerTurn = true;
        playerUI.SetActive(true);
        UpdateTurnUI("Giliran Player");
    }

    public void EndPlayerTurn()
    {
        if (extraTurns > 0)
        {
            extraTurns--;
            Debug.Log($"Menggunakan extra turn! Sisa: {extraTurns}");
            StartPlayerTurn(); // Langsung mulai giliran player lagi
            return;
        }

        playerUI.SetActive(false);
        StartCoroutine(EnemyTurn());
    }

    // ========== GILIRAN MUSUH ==========

    IEnumerator EnemyTurn()
    {
        isPlayerTurn = false;
        UpdateTurnUI($"Giliran {currentBoss.name}");

        // Tunggu sebentar untuk efek visual
        yield return new WaitForSeconds(turnDelay);

        // Pilih serangan acak
        int randomMove = Random.Range(0, 3);
        currentBoss.ExecuteMove(randomMove);

        // Tunggu eksekusi serangan selesai
        yield return new WaitForSeconds(turnDelay);

        // Cek kondisi kekalahan
        if (fearMeterSlider.value >= maxFear)
        {
            GameOver("RASA TAKUT TERLALU BESAR!", false);
            yield break;
        }

        StartPlayerTurn();
    }
    public void GrantExtraTurn()
    {
        extraTurns++;
        Debug.Log($"Extra turn granted! Sisa: {extraTurns}");
    }

    // ========== SISTEM FEAR METER ==========
    public void UpdateFearUI(int fearValue)
    {
        fearMeterSlider.value = fearValue;

        if (fearValue >= maxFear)
            GameOver("RASA TAKUT TERLALU BESAR!", false);
    }

    // ========== SISTEM KEMENANGAN/KEGAGALAN ==========
    public void CheckBossDefeat()
    {
        if (currentBoss.CurrentHP <= 0)
        {
            GameOver("ANDA MENGALAHKAN RASA TAKUT!", true);
        }
    }

    public void GameOver(string message, bool isWin)
    {
        gameOverText.text = message;
        gameOverText.color = isWin ? Color.green : Color.red;
        gameOverText.gameObject.SetActive(true);

        StartCoroutine(ReturnToMainScene(isWin));
    }

    // ========== TRANSISI SCENE ==========
    IEnumerator ReturnToMainScene(bool isWin)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(isWin ? "MainScene" : "GameOverScene");
    }

    // ========== UI UPDATES ==========
    void UpdateTurnUI(string text)
    {
        turnText.text = text;
        turnText.color = isPlayerTurn ? Color.blue : Color.red;
    }


}