using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private BossInteraction bossInteraction;
    [SerializeField] private Animator transition;

    // Update is called once per frame

    private void Start()
    {
        bossInteraction = FindFirstObjectByType<BossInteraction>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadBattleView(2);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadMenuView();
        }
    }

    public void LoadBattleView(int scene)
    {
        StartCoroutine(LoadScene(scene)); // battle scene   
    }

    public void LoadMenuView()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(LoadScene(0)); // menu scene
        }
        else
        {
            StartCoroutine(LoadScene(1)); // main scene
        }
    }

    public void LoadSpawnView()
    {
        StartCoroutine(LoadScene(4));
    }
    public void LoadMainView()
    {
        StartCoroutine(LoadScene(1));
    }

    private IEnumerator LoadScene(int SceneIndex)
    {
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(1);

        // load scene
        SceneManager.LoadScene(SceneIndex);
    }
}
