using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.MusicSource.volume = 0.25f;
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void QuitScene()
    {
        Application.Quit();
    }
}
