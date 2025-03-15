using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHP = 100; // HP maksimal
    public int currentHP; // HP saat ini

    public Slider healthSlider; // Slider untuk health bar di Canvas Utama

    void Start()
    {
        currentHP = maxHP; // Set HP awal
        UpdateHealthUI(); // Perbarui UI health bar
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // Kurangi HP
        if (currentHP < 0) currentHP = 0; // Pastikan HP tidak kurang dari 0
        UpdateHealthUI(); // Perbarui UI health bar
    }

    public void Heal(int amount)
    {
        currentHP += amount; // Tambah HP
        if (currentHP > maxHP) currentHP = maxHP; // Pastikan HP tidak melebihi maksimal
        UpdateHealthUI(); // Perbarui UI health bar
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHP / maxHP; // Perbarui nilai Slider
        }
    }

    public bool IsDefeated()
    {
        return currentHP <= 0; // Cek apakah HP sudah 0 atau kurang
    }
}