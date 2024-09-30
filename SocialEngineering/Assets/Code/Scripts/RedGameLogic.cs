using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedGameLogic : MonoBehaviour
{
    private GameTimer gameTimer;
    private PlayerManager playerManager;
    private SpriteManager spriteManager;

    void CheckGameTimer()
    {
        List<GameObject> redTeam = playerManager.GetRedTeam();
        if (gameTimer.GetTimerStatus())
        {
            for (int i = 0; i < redTeam.Count; i++)
            {
                redTeam[i].GetComponent<PlayerInfo>().LoseHealth();
                redTeam[i].GetComponent<RumbleManager>().RumblePulse(0.25f, 0.75f, 1.0f);
            }
            gameTimer.ResetTimer();
            playerManager.ResetTeams();
            spriteManager.RespawnProtraits();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        spriteManager = GetComponent<SpriteManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            if (playerManager.GetStartingStatus())
            {
                gameTimer = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<GameTimer>(); // waste of resources -- fix later if have time
                CheckGameTimer();
            }
        }
    }
}
