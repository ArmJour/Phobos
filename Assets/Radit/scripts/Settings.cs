using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    public AudioMixer audioMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SavedVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("SavedVolume");
            audioMixer.SetFloat("volume", Mathf.Log10(savedVolume) * 20);
        }
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    public void SetVolume(float volume) 
    {
        Debug.Log("Slider Value (dB): " + volume);
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("SavedVolume", volume);
        PlayerPrefs.Save();
    }

    public void quitGame() {
        Application.Quit();
    }

     void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
