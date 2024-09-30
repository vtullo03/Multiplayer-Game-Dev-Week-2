using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] Sprites;
    public Sprite Mouse;
    public Sprite HeartFull;
    public Sprite HeartEmpty;
    public Image CharUI;
    private List<GameObject> players = new List<GameObject>();

    private bool spawnedUI = false;

    /* Team Management Variables */
    private PlayerManager playerManager;
    private GameObject bluePlayer;
    private List<GameObject> redPlayers = new List<GameObject>();
    private int blueUIXPos = -513;
    private int[] redUIXPos = { 50, 325 };

    /**
    * Keeps track of players and sets their character protraits
    * 
    * @param input - the input component of the player joining
    * @return void
    */
    public void OnPlayerJoined(PlayerInput input)
    {
        input.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Sprites[players.Count];
        input.gameObject.GetComponent<PlayerInfo>().SetProtrait(Sprites[players.Count]);
        players.Add(input.gameObject);
    }

    /**
    * Spawn a single UI object for a player
    * 
    * @param location - the location of the UI object on the screen
    * player - the player that the UI is representing
    * @return void
    */
    void SpawnOneProtrait(Vector3 location, GameObject player)
    {
        GameObject canvas = GameObject.Find("Canvas");
        Image protrait = Instantiate(CharUI);
        player.GetComponent<PlayerInfo>().SetUI(protrait);
        protrait.transform.SetParent(canvas.transform, false);
        protrait.GetComponent<RectTransform>().anchoredPosition = location;
        protrait.sprite = player.GetComponent<PlayerInfo>().GetProtrait();
    }

    /**
    * Spawn UI objects for all players in the game, seperate red and blue players
    * 
    * @param void
    * @return void
    */
    private void SpawnAllProtraits()
    {
        CheckTeams();
        for (int i = 0; i < redPlayers.Count; i++) SpawnOneProtrait(new Vector3(redUIXPos[i], -184, 0), redPlayers[i]);
        SpawnOneProtrait(new Vector3(blueUIXPos, -184, 0), bluePlayer);
    }

    /**
    * Change all players' sprites into a mouse cursor for the main game
    * 
    * @param void
    * @return void
    */
    void TurnIntoMice()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Mouse;
            players[i].GetComponent<BoxCollider2D>().size = new Vector3(0.5f, 0.5f, 0.0f);
            players[i].GetComponent<BoxCollider2D>().offset = new Vector3(0.0f, 0.0f, 0.0f);
            players[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    /**
    * Consult PlayerManager.cs and find out who's on which team
    * 
    * @param void
    * @return void
    */
    void CheckTeams()
    {
        bluePlayer = playerManager.GetBlueTeam();
        redPlayers = playerManager.GetRedTeam();
    }

    /**
    * Display hearts on screen according to the amount of health each player has
    * 
    * @param void
    * @return void
    */
    void UpdateHearts()
    {
        Debug.Log(players.Count);
        for (int i = 0; i < players.Count; ++i)
        {
            PlayerInfo playerInfo = players[i].GetComponent<PlayerInfo>();
            int playerHealth = playerInfo.GetHealth();
            for (int j = 0; j < playerHealth; ++j)
            {
                playerInfo.GetUI().transform.GetChild(j).GetComponent<Image>().sprite = HeartFull;
            }
            for (int k = playerHealth; k < 3;  ++k)
            {
                playerInfo.GetUI().transform.GetChild(k).GetComponent<Image>().sprite = HeartEmpty;
            }
        }
    }

    /**
    * Delete and spawn new character protraits -- likely on team switch
    * 
    * @param void
    * @return void
    */
    public void RespawnProtraits()
    {
        for(int i = 0; i < players.Count; ++i) players[i].GetComponent<PlayerInfo>().DeleteProtrait();
        SpawnAllProtraits();
    }

    public void RemovePlayer(PlayerInput player)
    {
        players.Remove(player.gameObject);
    }

    /**
    * Check if in the main game and make all UI changes needed
    * 
    * @param void
    * @return void
    */
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            if (!spawnedUI && playerManager.GetStartingStatus())
            {
                SpawnAllProtraits();
                TurnIntoMice();
                spawnedUI = true;
            }
            if (spawnedUI)
            {
                UpdateHearts();
            }
        }
        if (SceneManager.GetActiveScene().name == "EpicWin")
        {
            GameObject canvas = GameObject.Find("Canvas");
            canvas.transform.GetChild(0).GetComponent<Image>().sprite = players[0].GetComponent<PlayerInfo>().GetProtrait();
        }
    }

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }
}
