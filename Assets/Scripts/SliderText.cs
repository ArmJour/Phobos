using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public Slider healthSlider; // Slider untuk health
    public Text healthText; // Text untuk menampilkan nilai

    void Start()
    {
        // Set teks awal
        UpdateText(healthSlider.value);

        // Tambahkan listener untuk update teks saat nilai Slider berubah
        healthSlider.onValueChanged.AddListener(UpdateText);
    }

    void UpdateText(float value)
    {
        // Update teks dengan format "current/max"
        healthText.text = $"{value}/{healthSlider.maxValue}";

        // Ubah warna teks berdasarkan nilai Slider
        float t = value / healthSlider.maxValue; // Nilai antara 0 dan 1
        healthText.color = Color.Lerp(Color.black, Color.white, t);
    }
}