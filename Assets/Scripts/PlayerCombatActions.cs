using UnityEngine;

public class PlayerCombatActions : MonoBehaviour
{
    private BattleSystem battleSystem;
    private Boss currentBoss;
    private int fearMeter;

    void Start()
    {
        battleSystem = FindFirstObjectByType<BattleSystem>();
        currentBoss = battleSystem.GetCurrentBoss();
        fearMeter = 0; // Fear meter selalu mulai dari 0
    }

    // ======= MOVE 1-6 ======= (ORIGINAL CODE)
    public void Move1()
    {
        int damage = Random.Range(0, 21);
        currentBoss.TakeDamage(damage);
        Debug.Log($"Serangan dasar! Damage: {damage}");
    }

    public void Move2()
    {
        currentBoss.TakeDamage(10);
        currentBoss.ApplyMissDebuff(25);
        Debug.Log("Serangan presisi! Damage 10 + 25% musuh meleset");
    }

    public void Move3()
    {
        int reduceAmount = Random.Range(1, 4);
        UpdateFearMeter(-reduceAmount);
        Debug.Log($"Menenangkan diri! Fear -{reduceAmount}");
    }

    public void Move4()
    {
        Debug.Log("Move4 (placeholder)");
    }

    public void Move5()
    {
        int reduceAmount = Random.Range(2, 6);
        UpdateFearMeter(-reduceAmount);
        battleSystem.GrantExtraTurn();
        Debug.Log($"Persiapan khusus! Fear -{reduceAmount} + Extra turn");
    }

    public void Move6()
    {
        int damage = Random.Range(20, 101);
        currentBoss.TakeDamage(damage);
        UpdateFearMeter(-7);
        Debug.Log($"Serangan putus asa! Damage {damage} + Fear -7");
    }

    // ======= FITUR BARU: Fear Meter System =======
    public void UpdateFearMeter(int amount)
    {
        fearMeter = Mathf.Clamp(fearMeter + amount, 0, 15);
        battleSystem.UpdateFearUI(fearMeter); // Update UI

        if (fearMeter >= 15)
        {
            Debug.Log("Game Over! Fear meter penuh");
            // Tambahkan logika game over
        }
    }
}