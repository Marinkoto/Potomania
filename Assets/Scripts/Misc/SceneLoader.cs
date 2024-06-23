using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject optionsMenu;
    private void Awake()
    {
        optionsMenu.SetActive(true);
    }
    private void Start()
    {
        optionsMenu.SetActive(false);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
    public void QuitScene()
    {
        Application.Quit();
    }
}
