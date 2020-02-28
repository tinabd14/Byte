using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject soundManager;
    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("SoundController") == null)
        {
            //soundManager = Instantiate(soundManager);
            //soundManager.GetComponent<PlayMusics>().AwakeSoundManager();
        }
    }
}
