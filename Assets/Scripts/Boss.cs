using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHP = 125;
    private int currentHP;
    private bool hasMissDebuff; // Untuk efek Move2

    [Header("UI")]
    [SerializeField] private Slider healthSlider; // Assign di Inspector
    [SerializeField] private GameObject associatedBackground; // Background khusus boss ini

    [Header("Combat")]
    [SerializeField] private int minFearDamage = 1; // Damage fear minimal (atur di Inspector)
    [SerializeField] private int maxFearDamage = 3; // Damage fear maksimal

    void Start()
    {
        currentHP = maxHP;
        InitializeUI();
    }

    // ===== UI SETUP =====
    void InitializeUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = maxHP;
        }
    }

    // ===== DAMAGE & DEBUFF =====
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Max(currentHP - damage, 0);
        UpdateHealthUI();

        if (currentHP <= 0) Debug.Log("Boss dikalahkan!");
    }

    public void ApplyMissDebuff(int missChance)
    {
        hasMissDebuff = true;
        Debug.Log($"Debuff applied: {missChance}% chance miss");
    }

    public bool CheckMiss()
    {
        if (!hasMissDebuff) return false;

        bool isMiss = (Random.Range(0, 100) < 25); // 25% chance miss
        hasMissDebuff = false; // Reset setelah dicek
        return isMiss;
    }

    // ===== FEAR DAMAGE CALCULATION =====
    public int CalculateBossAttack()
    {
        // Atur damage berbeda untuk tiap boss
        return Random.Range(minFearDamage, maxFearDamage + 1);
    }

    // ===== UI UPDATE =====
    void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHP;
    }
}