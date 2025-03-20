using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossInteraction : MonoBehaviour
{
    public int bossIndex;
    [SerializeField] private LevelLoader levelLoader;
    public GameObject interactPrompt; // Assign UI Text "Press E

    private bool isInRange;

    private void Start()
    {
        levelLoader = FindFirstObjectByType<LevelLoader>();
    }
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (levelLoader != null) // Tambahkan pengecekan null
            {
                PlayerPrefs.SetInt("CurrentBossIndex", bossIndex);
                levelLoader.LoadBattleView(2);
            }
            else
            {
                Debug.LogError("LevelLoader tidak ditemukan!");
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(false);
            isInRange = false;
        }
    }
}