using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossInteraction : MonoBehaviour
{
    public int bossIndex;
    PlayerController playerController;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private GameObject interactPrompt; // Assign UI Text "Press E
    [SerializeField] private GameObject interactFace;

    private bool isInRange;

    private void Start()
    {
        levelLoader = FindFirstObjectByType<LevelLoader>();
        playerController = FindFirstObjectByType<PlayerController>();
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


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);

            if (playerController.moving == false)
            {
                interactFace.SetActive(true);
            }
            else
            {
                interactFace.SetActive(false);
            }
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(false);
            interactFace.SetActive(false);
            isInRange = false;
        }
    }
}