using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();
    public List<int> playerPool = new List<int>() { 0, 1, 2 };
    private List<GameObject> redTeam = new List<GameObject>();
    GameObject blueTeam;

    private Vector3[] redTeamPositions = { new Vector3(8f, 2.0f, 0.0f), new Vector3(13.0f, 2.0f, 0.0f) };
    private Vector3 blueTeamPosition = new Vector3(-1.0f, 2.0f, 0.0f);

    /* messy solution but can be optimized later -- OPTIMIZE THIS LATER */
    /* sprite references for player health */
    public Image heartSpriteFull;
    public Image heartSpriteEmpty;

    private bool pickedStartingTeams = false;

    private SpriteManager spriteManager;

    void Start()
    {
        spriteManager = GetComponent<SpriteManager>();
    }

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
        int randomPlayer = playerPool[rngIndex];
        team.Add(players[randomPlayer].gameObject);
        playerPool.Remove(randomPlayer);
        return players[randomPlayer].gameObject;
    }

    /**
    * Pick two random players for the red team and put the remaining in blue
    * 
    * @param void
    * @return void
    */
    void PickTeam()
    {
        /* Check for dead players and remove them */
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].gameObject.GetComponent<PlayerInfo>().GetHealth() == 0)
            {
                Debug.Log("ge");
                spriteManager.RemovePlayer(players[i]);
                players.Remove(players[i]);
                playerPool.RemoveAt(playerPool.Count - 1);
                redTeamPositions = redTeamPositions.Take(redTeamPositions.Length - 1).ToArray();
            }
        }

        /* Pick all for red team */
        MoveRandomPlayer(redTeam);
        if(players.Count > 2) MoveRandomPlayer(redTeam);
        /* Add remaining to blue -- one player */
        blueTeam = players[playerPool[0]].gameObject;
        playerPool.Clear();
        playerPool = new List<int>() { 0, 1, 2 }; /* now that the picking is done reset pool for future use */
        MoveAllPlayers();
    }

    /**
    * Clear both teams and use the const list of players to reset the possible team selection candidates
    * 
    * @param void
    * @return void
    */
    public void ResetTeams()
    {
        redTeam.Clear();
        blueTeam = null;
        PickTeam();
    }

    /**
    * Move all the players to their respective positions for their teams
    * 
    * @param void
    * @return void
    */
    void MoveAllPlayers()
    {
        for (int i = 0; i < redTeam.Count; i++) redTeam[i].transform.position = redTeamPositions[i];
        blueTeam.transform.position = blueTeamPosition;
    }

    /* Getter functions for private variables needed by other scripts */
    public List<GameObject> GetRedTeam() { return redTeam; }
    public GameObject GetBlueTeam() { return blueTeam; }
    public bool GetStartingStatus() { return pickedStartingTeams; }
}