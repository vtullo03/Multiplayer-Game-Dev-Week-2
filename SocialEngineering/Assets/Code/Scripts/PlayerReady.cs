using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReady : MonoBehaviour
{
    private Controls controls;
    private GameObject readySprite;

    private bool isReady = false;

    void Awake()
    {
        controls = GetComponent<Controls>();
        readySprite = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (controls.ActionTriggered() && SceneManager.GetActiveScene().name == "PlayerJoin")
        {
            isReady = true;
            readySprite.SetActive(true);
        }
    }

    public bool getReadyStatus() 
    { 
        return isReady; 
    }
}
