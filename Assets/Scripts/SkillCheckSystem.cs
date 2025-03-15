using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillCheckSystem : MonoBehaviour
{
    public GameObject skillCheckUI; // UI untuk Skill Check
    public Image skillCheckBar; // Lingkaran Skill Check
    public Image needle; // Jarum yang berputar
    public float needleSpeed = 200f; // Kecepatan jarum

    private bool isSkillCheckActive = false;
    private float needleAngle = 0f;
    private float hitZoneSize; // Ukuran area hit zone
    private int skillCheckCount; // Jumlah Skill Check yang diperlukan
    private int currentSkillCheck; // Skill Check saat ini
    private int totalDamage; // Total damage yang akan diberikan
    private Action<int> onSkillCheckComplete; // Callback setelah Skill Check selesai

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

    public void StartSkillCheck(float zoneSize, int count, int damage, Action<int> onComplete)
    {
        hitZoneSize = zoneSize;
        skillCheckCount = count;
        currentSkillCheck = 0;
        totalDamage = damage;
        onSkillCheckComplete = onComplete;
        skillCheckUI.SetActive(true);
        isSkillCheckActive = true;
        needleAngle = 0f; // Reset jarum
    }

    void CheckSkillCheckResult()
    {
        float hitPosition = needleAngle;
        float hitZoneStart = 180f - hitZoneSize / 2;
        float hitZoneEnd = 180f + hitZoneSize / 2;

        if (hitPosition >= hitZoneStart && hitPosition <= hitZoneEnd)
        {
            Debug.Log("Hit!");
            currentSkillCheck++;
            if (currentSkillCheck >= skillCheckCount)
            {
                // Berikan damage setelah semua Skill Check selesai
                onSkillCheckComplete?.Invoke(totalDamage);
                EndSkillCheck();
            }
        }
        else
        {
            Debug.Log("Miss!");
            EndSkillCheck();
        }
    }

    void EndSkillCheck()
    {
        isSkillCheckActive = false;
        skillCheckUI.SetActive(false);
    }
}