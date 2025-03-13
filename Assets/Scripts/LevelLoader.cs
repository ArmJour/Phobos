using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadBattleView();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadMenuView();
        }
    }

    public void LoadBattleView()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 0) 
        {
            StartCoroutine(LoadScene(2)); // battle scene
        }
        else
        {
            StartCoroutine(LoadScene(1)); // main scene
        }
        
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
