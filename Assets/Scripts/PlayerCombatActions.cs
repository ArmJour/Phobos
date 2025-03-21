using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatActions : MonoBehaviour
{
    // ========== REFERENSI SISTEM ==========
    [Header("System References")]
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private Boss currentBoss;
    [SerializeField] private AudioClip breathingSFX; // SFX untuk move3

    // ========== STATUS PLAYER ==========
    [Header("Player Stats")]
    [SerializeField] private int fearMeter;
    [SerializeField] private int maxFear = 15;
    private float missChance;

    // ========== UI REFERENCES ==========
    [Header("UI References")]
    [SerializeField] private Slider fearMeterSlider;
    [SerializeField] private Text actionLogText;

    void Start()
    {
        fearMeter = 0; // Fear meter mulai dari 0
        UpdateFearUI();
        currentBoss = battleSystem.currentBoss;
    }

    // ========== DAFTAR MOVE ==========
    public void Move1() // Random damage 0-20
    {
        int damage = Random.Range(0, 21);
        currentBoss.TakeDamage(damage);
        LogAction($"Serangan dasar! Damage: {damage}");
        EndTurn();
    }

    public void Move2() // Damage 10 + 25% miss chance
    {
        currentBoss.TakeDamage(10);
        currentBoss.ApplyMissDebuff(25);
        LogAction("Serangan presisi! Damage 10 + 25% musuh meleset");
        EndTurn();
    }

    public void Move3() // Reduksi fear 1-3
    {
        int reduce = Random.Range(1, 4);
        UpdateFearMeter(-reduce);
        PlaySFX(breathingSFX); // SFX bernafas
        LogAction($"Menenangkan diri! Fear -{reduce}");
        EndTurn();
    }

    public void Move4() // Placeholder
    {
        LogAction("Move4 belum diimplementasi!");
        EndTurn();
    }

    public void Move5() // Extra turn + reduksi fear 2-5
    {
        int reduce = Random.Range(2, 6);
        UpdateFearMeter(-reduce);
        battleSystem.GrantExtraTurn();
        LogAction($"Persiapan khusus! Fear -{reduce} + Extra turn");
        EndTurn();
    }

    public void Move6() // Damage besar + reduksi fear 7
    {
        int damage = Random.Range(20, 101);
        currentBoss.TakeDamage(damage);
        UpdateFearMeter(-7);
        LogAction($"Serangan putus asa! Damage {damage} + Fear -7");
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
    void LogAction(string message)
    {
        actionLogText.text = message;
    }
}