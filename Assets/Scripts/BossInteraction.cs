using UnityEngine;
using UnityEngine.SceneManagement;

public class BossInteraction : MonoBehaviour
{
    public LevelLoader LevelLoader;
    public GameObject promptText; // Assign UI Text ke sini
    public string battleSceneName = "BattleScene"; // Nama scene battle
    public int bossIndex; // 0 untuk Boss1, 1 untuk Boss2

    private bool isPlayerInRange = false;


    private void Start()
    {
        LevelLoader = FindFirstObjectByType<LevelLoader>();

    }
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Simpan index boss yang diinteract
            PlayerPrefs.SetInt("CurrentBossIndex", bossIndex);
            // Pindah ke scene battle
            LevelLoader.LoadBattleView();
            Debug.Log("pressing E");
        }
    }

    // Saat player masuk area trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            promptText.SetActive(true);
            isPlayerInRange = true;
        }
    }

    // Saat player keluar area trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            promptText.SetActive(false);
            isPlayerInRange = false;
        }
    }
}