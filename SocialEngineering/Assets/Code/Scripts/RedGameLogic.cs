using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedGameLogic : MonoBehaviour
{
    private GameTimer gameTimer;
    private PlayerManager playerManager;
    private SpriteManager spriteManager;

    public TextAsset File;
    public TMP_Text CurrGuess;
    public GameObject Letter;
    private string wordToSolve;
    private char[] scrambledWord;
    private List<GameObject> letters = new List<GameObject>();

    private float currX = 0f;
    // private float currY = 0f;
    // private float maxX = 15.5f;
    private Vector3 startingPoint = new Vector3(7.5f, -1.5f, 0.0f);

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

    void GetRandomLine()
    {
        var lines = File.text.Split('\n');
        var randomIndex = Random.Range(0, lines.Length);
        wordToSolve = lines[randomIndex];
    }

    void ScrambleLine()
    {
        System.Random random = new System.Random();
        char[] brokenLine = wordToSolve.ToCharArray();

        /* Fisher–Yates shuffle */
        int n = brokenLine.Length;
        while (n > 1)
        {
            int k = random.Next(n--);
            char temp = brokenLine[n];
            brokenLine[n] = brokenLine[k];
            brokenLine[k] = temp;
        }

        scrambledWord = brokenLine;
    }

    void DisplayScramble()
    {
        for (int i = 0; i < scrambledWord.Length; i++)
        {
            Sprite currLetter = Resources.Load<Sprite>("" + char.ToUpper(scrambledWord[i]));
            GameObject l = Instantiate(Letter);
            letters.Add(l);
            l.transform.position = startingPoint + new Vector3(currX, 0.0f, 0.0f);
            currX += 2.0f;
            l.GetComponent<SpriteRenderer>().sprite = currLetter;
            l.GetComponent<LetterLogic>().SetLetter(scrambledWord[i]);
        }
    }

    public void CreatePuzzle()
    {
        /* Reset these if not already */
        CurrGuess.text = "";
        currX = 0.0f;
        letters.Clear();
        GetRandomLine();
        ScrambleLine();
        DisplayScramble();
        Debug.Log(wordToSolve);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("GameManager").GetComponent<PlayerManager>();
        spriteManager = GameObject.Find("GameManager").GetComponent<SpriteManager>();
        CreatePuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.GetStartingStatus())
        {

            if (CurrGuess.text.Length == wordToSolve.Length)
            {
                if (CurrGuess.text != wordToSolve)
                {
                    Debug.Log(CurrGuess.text);
                    CreatePuzzle();
                }
                else
                {
                    CreatePuzzle();
                    playerManager.GetBlueTeam().GetComponent<PlayerInfo>().LoseHealth();
                    gameTimer.ResetTimer();
                    playerManager.ResetTeams();
                    spriteManager.RespawnProtraits();
                }
            }

            gameTimer = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<GameTimer>(); // waste of resources -- fix later if have time
            CheckGameTimer();

            for (int i = 0; i < letters.Count; i++)
            {
                if (letters[i].GetComponent<LetterLogic>().IsSelected())
                {
                    CurrGuess.text += letters[i].GetComponent<LetterLogic>().GetLetter();
                    Destroy(letters[i]);
                    letters.RemoveAt(i);
                }
            }
        }
    }
}
