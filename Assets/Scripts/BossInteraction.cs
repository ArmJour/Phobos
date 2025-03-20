using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossInteraction : MonoBehaviour
{
    // ======= ORIGINAL CODE =======
    public int bossIndex;
    public GameObject interactPrompt; // Assign UI Text "Press E"

    // ======= FITUR BARU: Transisi =======
    public Animator sceneTransition; // Assign animator fade

    private bool isInRange;

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetInt("CurrentBossIndex", bossIndex);
            StartCoroutine(LoadBattleScene());
        }
    }

    // ======= FITUR BARU: Transisi Animasi =======
    IEnumerator LoadBattleScene()
    {
        sceneTransition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BattleScene");
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