using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatActions : MonoBehaviour
{
    // ========== REFERENSI SISTEM ==========
    [Header("System References")]
    private BattleSystem battleSystem;
    private Boss currentBoss;
    [SerializeField] private AudioClip breathingSFX; // SFX untuk move3

    // ========== STATUS PLAYER ==========
    [Header("Player Stats")]
    [SerializeField] private int fearMeter;
    [SerializeField] private int maxFear = 15;
    private float missChance;

    // ========== UI REFERENCES ==========
    [Header("UI References")]
    [SerializeField] private Slider fearMeterSlider;

    void Start()
    {
        fearMeter = 0; // Fear meter mulai dari 0
        UpdateFearUI();
        currentBoss = FindFirstObjectByType<Boss>();
        battleSystem = FindFirstObjectByType<BattleSystem>();
    }

    // ========== DAFTAR MOVE ==========
    public void Move1() // Random damage 0-20
    {
        int damage = Random.Range(0, 21);
        currentBoss.TakeDamage(damage);
        EndTurn();
    }

    public void Move2() // Damage 10 + 25% miss chance
    {
        currentBoss.TakeDamage(10);
        currentBoss.ApplyMissDebuff(25);
        EndTurn();
    }

    public void Move3() // Reduksi fear 1-3
    {
        int reduce = Random.Range(1, 4);
        UpdateFearMeter(-reduce);
        PlaySFX(breathingSFX); // SFX bernafas
        EndTurn();
    }

    public void Move4() // Placeholder
    {
        UpdateFearMeter(-15);
        currentBoss.ApplyMissDebuff(50);
        EndTurn();
    }

    public void Move5() // Extra turn + reduksi fear 2-5
    {
        int reduce = Random.Range(2, 6);
        UpdateFearMeter(-reduce);
        battleSystem.GrantExtraTurn();
        EndTurn();
    }

    public void Move6() // Damage besar + reduksi fear 7
    {
        int damage = Random.Range(20, 101);
        currentBoss.TakeDamage(damage);
        UpdateFearMeter(-7);
        EndTurn();
    }

    // ========== SISTEM FEAR METER ==========
    public void UpdateFearMeter(int amount)
    {
        fearMeter = Mathf.Clamp(fearMeter + amount, 0, maxFear);
        UpdateFearUI();

        if (fearMeter >= maxFear)
            battleSystem.GameOver("RASA TAKUT TERLALU BESAR!", false); ;
    }

    void UpdateFearUI()
    {
        fearMeterSlider.value = fearMeter;
    }

    // ========== SISTEM MISS CHANCE ==========
    public void ApplyMissChance(float chance)
    {
        missChance = chance;
    }

    public bool CheckForMiss()
    {
        bool isMiss = Random.Range(0f, 100f) < missChance;
        if (isMiss) missChance = 0; // Reset setelah dicek
        return isMiss;
    }

    // ========== AUDIO SYSTEM ==========
    void PlaySFX(AudioClip clip)
    {
        // Implementasi audio sesuai sistem yang dipakai
        Debug.Log("Memainkan SFX: " + clip.name);
    }

    // ========== MANAJEMEN GILIRAN ==========
    void EndTurn()
    {
        battleSystem.EndPlayerTurn();
        missChance = 0; // Reset miss chance setiap giliran
    }

    // ========== UI SYSTEM ==========
    
}