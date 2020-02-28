using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMusics : MonoBehaviour
{
    public AudioSource[] gameSounds;

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        DontDestroyOnLoad(transform.gameObject);
        PlayAppropriateSound();
    }

    private void PlayAppropriateSound()
    {
        string sceneName = SceneManager.GetActiveScene().name;   
        if(sceneName.Equals("Menu"))
        {
            gameSounds[0].Stop();
        }
        else if(sceneName.Equals("Level Chooser"))
        {
            gameSounds[0].Stop();
        }
        else
        {
            if (!gameSounds[0].isPlaying)
            {
                PlayGameSound();
            }
        }
    }

    public void PlayGameSound()
    {
        gameSounds[0].Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }


    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    
}
