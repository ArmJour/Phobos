using UnityEngine;
using System.Collections.Generic;

public class PlayerCombatActions : MonoBehaviour
{
    private BattleSystem battleSystem; // Reference ke sistem battle
    private int fearMeter; // 0-15
    private Boss currentBoss;

    void Start()
    {
        battleSystem = FindFirstObjectByType<BattleSystem>();
        currentBoss = FindFirstObjectByType<Boss>();
        currentBoss = battleSystem.GetCurrentBoss();
    }

    // Method untuk update Fear Meter (pastikan clamp 0-15)
    public void UpdateFearMeter(int amount)
    {
        fearMeter = Mathf.Clamp(fearMeter + amount, 0, 15);
        // Trigger SFX bernafas jika fearMeter berkurang (sesuaikan dengan kebutuhan)
        if (amount < 0) Debug.Log("SFX: Bernafas");
    }

    // ==== DAFTAR MOVE ====
    public void Move1()
    {
        int damage = Random.Range(0, 21);
        currentBoss.TakeDamage(damage);
        Debug.Log($"Move 1: Damage {damage}");
    }

    public void Move2()
    {
        currentBoss.TakeDamage(10);
        currentBoss.ApplyMissDebuff(25); // 25% chance miss
        Debug.Log("Move 2: Damage 10 + Apply Miss Debuff");
    }

    public void Move3()
    {
        int reduceAmount = Random.Range(1, 4);
        UpdateFearMeter(-reduceAmount);
        Debug.Log($"Move 3: Fear -{reduceAmount}");
    }

    public void Move4()
    {
        // Kosongkan dulu atau tambahkan placeholder
        Debug.LogWarning("Move4 belum diimplementasi!");
    }

    public void Move5()
    {
        int reduceAmount = Random.Range(2, 6);
        UpdateFearMeter(-reduceAmount);
        battleSystem.GrantExtraTurn(); // Beri extra turn
        Debug.Log($"Move 5: Fear -{reduceAmount} + Extra Turn");
    }

    public void Move6()
    {
        int damage = Random.Range(20, 101);
        currentBoss.TakeDamage(damage);
        UpdateFearMeter(-7);
        Debug.Log($"Move 6: Damage {damage} + Fear -7");
    }
}