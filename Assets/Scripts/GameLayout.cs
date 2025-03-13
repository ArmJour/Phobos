using UnityEngine;

public class GameLayout : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60; // framerate fixed 60 fps 

        Screen.SetResolution(1920, 1080, true); // resolusi 16:9, true = fullscreen, bisa diganti false untuk windowed
    }
}
