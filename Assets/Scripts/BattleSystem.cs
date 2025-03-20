using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    // ==== REFERENSI BOSS & UI ====
    public GameObject boss1; // Drag Boss1 dari Hierarchy
    public GameObject boss2; // Drag Boss2 dari Hierarchy
    public GameObject background1;
    public GameObject background2;
    public Slider fearMeterSlider;

    // ==== VARIABEL TURN & BOSS ====
    private int currentBossIndex;
    private Boss currentBoss;
    private bool isPlayerTurn = true;
    private int extraTurns = 0;

    void Start()
    {
        // Ambil index boss dari PlayerPrefs
        currentBossIndex = PlayerPrefs.GetInt("CurrentBossIndex", 0);
        SetupBattleEnvironment();
    }

    // ==== SETUP AWAL BATTLE ====
    void SetupBattleEnvironment()
    {
        // Matikan semua boss dan background
        boss1.SetActive(false);
        boss2.SetActive(false);
        background1.SetActive(false);
        background2.SetActive(false);

        // Aktifkan sesuai index
        if (currentBossIndex == 0)
        {
            boss1.SetActive(true);
            background1.SetActive(true);
            currentBoss = boss1.GetComponent<Boss>(); // Dapatkan komponen Boss
        }
        else
        {
            boss2.SetActive(true);
            background2.SetActive(true);
            currentBoss = boss2.GetComponent<Boss>();
        }
    }

    // ==== METHOD UNTUK PLAYERCOMBATACTIONS ====
    public Boss GetCurrentBoss()
    {
        return currentBoss; // Kembalikan boss yang aktif
    }

    public void GrantExtraTurn()
    {
        extraTurns = 1; // Beri 1 extra turn
        Debug.Log("Extra turn diberikan!");
    }

    // ==== UPDATE UI FEAR METER ====
    public void UpdateFearUI(int value)
    {
        fearMeterSlider.value = value;
    }

    // ==== LOGIKA GILIRAN PLAYER/MUSUH ====
    public void EndPlayerTurn()
    {
        if (extraTurns > 0)
        {
            extraTurns--;
            Debug.Log("Player dapat giliran lagi!");
            return; // Lanjutkan giliran player
        }

        StartCoroutine(EnemyTurn());
    }

    // ==== GILIRAN MUSUH ====
    IEnumerator EnemyTurn()
    {
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);

        // Musuh menyerang dan tambah fear meter
        isPlayerTurn = true; // Kembali ke giliran player

        if (!currentBoss.CheckMiss())
        {
            int fearDamage = currentBoss.CalculateBossAttack();
            FindFirstObjectByType<PlayerCombatActions>().UpdateFearMeter(fearDamage);
        }
        else
        {
            Debug.Log("Serangan boss meleset!");
        }
    }
}