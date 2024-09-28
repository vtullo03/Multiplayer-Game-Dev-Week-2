using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();
    private List<PlayerInput> playerPool = new List<PlayerInput>();
    private List<GameObject> redTeam = new List<GameObject>();
    GameObject blueTeam;

    /* messy solution but can be optimized later -- OPTIMIZE THIS LATER */
    /* sprite references for player health */
    public Image heartSpriteFull;
    public Image heartSpriteEmpty;

    private bool pickedStartingTeams = false;

    /**
    * Add all players in game to the list of possible team candidates
    * 
    * @param void
    * @return void
    */
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            if (!pickedStartingTeams) 
            {
                playerPool = players;
                PickTeam();
                pickedStartingTeams = true;
            }
        }
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
    * Pick a random player from the candidates of possibilites and make it a certain team
    * 
    * @param team - the team that the random player needs to go to
    * @return void
    */
    GameObject MoveRandomPlayer(List<GameObject> team)
    {
        System.Random rng = new System.Random();
        int rngIndex = rng.Next(playerPool.Count);
        PlayerInput randomPlayer = playerPool[rngIndex];
        team.Add(randomPlayer.gameObject);
        playerPool.Remove(randomPlayer);
        return randomPlayer.gameObject;
    }

    /**
    * Pick two random players for the red team and put the remaining in blue
    * 
    * @param void
    * @return void
    */
    void PickTeam()
    {
        /* Pick all for red team */
        MoveRandomPlayer(redTeam);
        MoveRandomPlayer(redTeam);
        /* Add remaining to blue -- one player */
        blueTeam = playerPool[0].gameObject;
        playerPool.Remove(playerPool[0]);
    }

    /**
    * Clear both teams and use the const list of players to reset the possible team selection candidates
    * 
    * @param void
    * @return void
    */
    void ResetTeams()
    {
        redTeam.Clear();
        blueTeam = null;
        playerPool = players;
    }

    /* Getter functions for private variables needed by other scripts */
    public List<GameObject> GetRedTeam() { return redTeam; }
    public GameObject GetBlueTeam() { return blueTeam; }
    public bool GetStartingStatus() { return pickedStartingTeams; }
}