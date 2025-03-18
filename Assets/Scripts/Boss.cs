using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int maxHP;
    private int currentHP;
    private bool hasMissDebuff; // Untuk move2
    public Slider healthSlider;

    void Start()
    {
        currentHP = maxHP;
        InitializeHealthSlider();
    }

    public void TakeDamage(int damage) => currentHP -= damage;

    private void InitializeHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = maxHP;
            healthSlider.gameObject.SetActive(true); // Aktifkan slider
        }
    }
    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }
    }

    public void ApplyMissDebuff(int chance)
    {
        hasMissDebuff = true;
        // Debuff hanya berlaku 1 ronde (reset di BattleSystem)
    }

    public bool CheckMiss()
    {
        if (!hasMissDebuff) return false;
        bool isMiss = (Random.Range(0, 100) < 25);
        hasMissDebuff = false; // Reset setelah dicek
        return isMiss;
    }
}