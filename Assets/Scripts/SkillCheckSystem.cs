using UnityEngine;
using UnityEngine.UI;

public class SkillCheckSystem : MonoBehaviour
{
    public Image needle; // Gambar jarum (Needle)
    public Image hitZoneVisual; // Gambar visualisasi zona hit
    public float needleSpeed = 200f; // Kecepatan jarum

    private bool isSkillCheckActive = false;
    private float needleAngle = 0f;
    private int remainingSkillChecks; // Jumlah Skill Check yang tersisa
    private float hitZoneSize; // Ukuran area hit zone
    private float currentHitZoneStart; // Zona hit saat ini
    private float currentHitZoneEnd; // Zona hit saat ini
    private int damage; // Damage yang akan diberikan
    private System.Action<int> onSkillCheckComplete; // Callback setelah Skill Check selesai

    void Start()
    {
        // Nonaktifkan Skill Check saat game dimulai
        isSkillCheckActive = false;
        
        hitZoneVisual.gameObject.SetActive(false);
        needle.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isSkillCheckActive)
        {
            // Putar jarum
            needleAngle += needleSpeed * Time.deltaTime;
            if (needleAngle >= 360f) needleAngle = 0f;
            needle.transform.rotation = Quaternion.Euler(0, 0, -needleAngle);

            // Input pemain (contoh: tombol Space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckSkillCheckResult();
            }
        }
    }

    // Method untuk memulai Skill Check
    public void StartSkillCheck(float zoneSize, int skillCheckCount, int damage, System.Action<int> onComplete)
    {
        this.hitZoneSize = zoneSize;
        this.remainingSkillChecks = skillCheckCount;
        this.damage = damage;
        this.onSkillCheckComplete = onComplete;

        isSkillCheckActive = true;
        StartNextSkillCheck();
    }

    // Method untuk memulai Skill Check berikutnya
    private void StartNextSkillCheck()
    {
        if (remainingSkillChecks > 0)
        {
            // Reset rotasi jarum
            needleAngle = 0f;
            needle.transform.rotation = Quaternion.Euler(0, 0, 0);

            // Tentukan zona hit secara random di sekitar lingkaran
            float hitZoneCenter = Random.Range(0f, 360f); // Zona hit random
            currentHitZoneStart = hitZoneCenter - hitZoneSize / 2;
            currentHitZoneEnd = hitZoneCenter + hitZoneSize / 2;

            // Update visualisasi zona hit
            UpdateHitZoneVisual(hitZoneCenter, hitZoneSize);

            Debug.Log($"Skill Check {remainingSkillChecks} dimulai! Zona: {currentHitZoneStart} - {currentHitZoneEnd}");
        }
        else
        {
            // Selesai Skill Check
            isSkillCheckActive = false;
            hitZoneVisual.gameObject.SetActive(false); // Nonaktifkan visualisasi zona hit
            onSkillCheckComplete?.Invoke(damage); // Berikan damage ke musuh
            Debug.Log("Skill Check selesai!");
        }
    }

    // Method untuk mengupdate visualisasi zona hit
    private void UpdateHitZoneVisual(float center, float size)
    {
        if (hitZoneVisual != null)
        {
            // Aktifkan visualisasi zona hit
            hitZoneVisual.gameObject.SetActive(true);

            // Atur rotasi zona hit
            hitZoneVisual.transform.rotation = Quaternion.Euler(0, 0, -center);

            // Atur ukuran zona hit
            RectTransform rectTransform = hitZoneVisual.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(size, size);
        }
    }

    // Method untuk mengecek hasil Skill Check
    private void CheckSkillCheckResult()
    {
        float hitPosition = needleAngle;

        if (hitPosition >= currentHitZoneStart && hitPosition <= currentHitZoneEnd)
        {
            Debug.Log("Hit!");
        }
        else
        {
            Debug.Log("Miss!");
        }

        // Kurangi jumlah Skill Check yang tersisa
        remainingSkillChecks--;

        if (remainingSkillChecks > 0)
        {
            // Mulai Skill Check berikutnya
            StartNextSkillCheck();
        }
        else
        {
            // Selesai Skill Check
            isSkillCheckActive = false;
            hitZoneVisual.gameObject.SetActive(false); // Nonaktifkan visualisasi zona hit
            needle.gameObject.SetActive(false); // Nonaktifkan jarum
            onSkillCheckComplete?.Invoke(damage); // Berikan damage ke musuh
            Debug.Log("Skill Check selesai!");
        }
    }
}