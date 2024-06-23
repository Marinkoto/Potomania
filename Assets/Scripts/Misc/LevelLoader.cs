using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject[] levels;
    void Start()
    {
        LoadLevel();
    }
    private void LoadLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if(i == LevelManager.currentIndex)
            {
                levels[i].SetActive(true);
            }
        }
    }
}
