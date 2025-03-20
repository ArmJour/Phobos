using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossInteraction : MonoBehaviour
{
    public int bossIndex;
    LevelLoader LevelLoader;
    public GameObject interactPrompt; // Assign UI Text "Press E"


    private bool isInRange;


    private void Start()
    {
        LevelLoader = FindFirstObjectByType<LevelLoader>();
    }
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetInt("CurrentBossIndex", bossIndex);
            LevelLoader.LoadBattleView();
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