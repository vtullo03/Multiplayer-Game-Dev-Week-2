using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadyChecker : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();

    bool CheckAllReady()
    {
        for (int i = 0; i < players.Count; i++) 
        {
            PlayerReady ready = players[i].GetComponent<PlayerReady>();
            if (ready.getReadyStatus() == false) return false;
        }
        return true;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        players.Add(input);
    }

    void Update()
    {
        if (CheckAllReady() && players.Count != 0) Debug.Log("ALL READY");
    }
}
