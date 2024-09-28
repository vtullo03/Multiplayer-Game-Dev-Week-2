using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReadyChecker : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();
    private bool gameLoaded = false;

    /**
    * Load the next scence only when the flag is not set
    * 
    * @param void
    * @return void
    * @note - needed because Unity's scene manager will bug and load multiple copies
    * of the same scene if you use Update() to switch
    */

    void LoadGame()
    {
        if (gameLoaded) return;
        SceneManager.LoadScene("MainGame");
    }

    /**
    * Checks the ready flag of all players
    * 
    * @param void
    * @return bool - true if all players have the ready flag checked
    */
    bool CheckAllReady()
    {
        for (int i = 0; i < players.Count; i++) 
        {
            PlayerReady ready = players[i].GetComponent<PlayerReady>();
            if (ready.getReadyStatus() == false) return false;
        }
        return true;
    }

    /**
    * Keep track of players that join the game
    * 
    * @param void
    * @return void
    */
    public void OnPlayerJoined(PlayerInput input)
    {
        players.Add(input);
    }

    /**
    * Constantly check if all players are ready - switch scene if so
    * 
    * @param void
    * @return void
    */
    void Update()
    {
        if (CheckAllReady() && players.Count >= 3)
        {
            LoadGame();
            gameLoaded = true;
        }
    }
}
