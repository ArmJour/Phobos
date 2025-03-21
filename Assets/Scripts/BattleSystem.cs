using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // ========== REFERENSI OBJECT ==========
    [Header("Object References")]
    [SerializeField] private GameObject boss1;
    [SerializeField] private GameObject boss2;
    private PlayerCombatActions playerActions;
    private Boss currentBoss;
    private LevelLoader levelLoader;

    // ========== UI REFERENCES ==========
    [Header("UI References")]
    [SerializeField] private Slider fearMeterSlider;

    // ========== SYSTEM SETTINGS ==========
    [Header("Settings")]
    [SerializeField] private float turnDelay = 1f;
    [SerializeField] private int maxFear = 15;

    // ========== STATUS BATTLE ==========
    public bool isPlayerTurn = true;
    private int currentBossIndex;
    private int extraTurns = 0;

    void Start()
    {
        StartCoroutine(StartBattle());
        currentBoss = FindFirstObjectByType<Boss>();
        levelLoader = FindFirstObjectByType<LevelLoader>();

        boss1.SetActive(false);
        boss2.SetActive(false);

        // Tampilkan boss sesuai index
        int bossIndex = PlayerPrefs.GetInt("CurrentBossIndex", 0);
        currentBoss = (bossIndex == 0) ? boss1 : boss2;
        currentBoss.SetActive(true); // Tampilkan boss yang dipilih
    }

    // ========== INISIALISASI BATTLE ==========
    IEnumerator StartBattle()
    {
        // Ambil data boss yang diinteract dari scene utama
        currentBossIndex = PlayerPrefs.GetInt("CurrentBossIndex", 0);
        currentBoss = (currentBossIndex == 0) ? boss1 : boss2;

        // Setup UI awal
        UpdateFearUI(0);

        // Tunggu 1 detik untuk efek dramatis
        yield return new WaitForSeconds(1f);

        StartPlayerTurn();
    }

    // ========== GILIRAN PLAYER ==========
    public void StartPlayerTurn()
    {
        isPlayerTurn = true;
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

        StartCoroutine(EnemyTurn());
    }

    // ========== GILIRAN MUSUH ==========

    IEnumerator EnemyTurn()
    {
        isPlayerTurn = false;

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

        StartCoroutine(ReturnToMainScene(isWin));
    }

    // ========== TRANSISI SCENE ==========
    IEnumerator ReturnToMainScene(bool isWin)
    {
        yield return new WaitForSeconds(3f);
        levelLoader.LoadSpawnView();
    }

    // ========== UI UPDATES ==========
    


}