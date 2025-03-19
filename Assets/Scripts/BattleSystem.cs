using System.Collections;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public Boss[] bosses;
    private int currentBossIndex = 0;
    private bool isPlayerTurn = true;
    private int extraTurns = 0;
    public GameObject boss1; // Drag Boss1 GameObject ke slot ini di Inspector
    public GameObject boss2; // Drag Boss2 GameObject ke slot ini di Inspector
    private Boss currentBossScript; // Script Boss dari boss yang aktif
    

    public Boss GetCurrentBoss() => bosses[currentBossIndex];

    public void GrantExtraTurn() => extraTurns = 1;

    void Start() => StartBattle();

    void StartBattle()
    {
        int currentBossIndex = PlayerPrefs.GetInt("CurrentBossIndex", 0);
        // Inisialisasi battle (panggil dari trigger interact)
        boss1.SetActive(false);
        boss2.SetActive(false);// Atau sesuai progress
        boss1.SetActive(currentBossIndex == 0);
        boss2.SetActive(currentBossIndex == 1);
    }

    public void StartBossBattle(int bossIndex)
    {
        currentBossIndex = bossIndex;

        // Aktifkan boss sesuai index
        if (bossIndex == 0)
        {
            boss1.SetActive(true);
            boss2.SetActive(false);
            currentBossScript = boss1.GetComponent<Boss>();
        }
        else if (bossIndex == 1)
        {
            boss1.SetActive(false);
            boss2.SetActive(true);
            currentBossScript = boss2.GetComponent<Boss>();
        }

        Debug.Log($"Memulai battle dengan Boss {currentBossIndex + 1}");
        // Mulai logika battle...
    }



    // Dipanggil ketika player selesai memilih move
    public void EndPlayerTurn()
    {
        if (extraTurns > 0)
        {
            extraTurns--;
            return; // Lanjut player turn
        }

        StartCoroutine(EnemyTurn());
    }

    private int CalculateBossAttack()
    {
        int fearDamage = 0;

        // Sesuaikan damage berdasarkan boss yang sedang aktif
        if (currentBossIndex == 0) // Boss 1 (125 HP)
        {
            fearDamage = Random.Range(1, 4); // Random 1-3 fear damage
        }
        else if (currentBossIndex == 1) // Boss 2 (175 HP)
        {
            fearDamage = Random.Range(2, 6); // Random 2-5 fear damage
        }

        Debug.Log($"Boss menyerang! Fear Meter +{fearDamage}");
        return fearDamage;
    }

    IEnumerator EnemyTurn()
    {
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);

        // Contoh: Boss menyerang dan menambah fear meter
        int fearDamage = CalculateBossAttack();
        FindFirstObjectByType<PlayerCombatActions>().UpdateFearMeter(fearDamage);

        isPlayerTurn = true;
    }
}