using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGameLogic : MonoBehaviour
{
    public GameObject card;
    public Sprite back;
    public Sprite[] cardTypes;

    private List<GameObject> cards = new List<GameObject>();

    private float currX = 0f;
    private float currY = 0f;
    private float maxX = 2.0f;
    private Vector3 startingPoint = new Vector3(-4.0f, 3.0f, 0.0f);
    private List<GameObject> selectedCards = new List<GameObject>();
    private bool puzzleCreated = false;

    void MoveCards()
    {
        for (int i = 0; i < cards.Count; i++) 
        {
            if (startingPoint.x + currX <= maxX)
            {
                cards[i].transform.position = startingPoint + new Vector3(currX, currY, 0.0f);
                currX += 2.0f;
            }
            else
            {
                currX = 0.0f;
                currY -= 2.0f;
                cards[i].transform.position = startingPoint + new Vector3(0.0f, currY, 0.0f);
            }
        }
        /* Need this extra call for some reason? */
        cards[cards.Count - 1].transform.position = startingPoint + new Vector3(currX, currY, 0.0f);
        currX = 0f;
        currY = 0f;
    }

    /**
    * Shuffle all the cards we created to so that the order is completely random for the player
    * 
    * @param void
    * @return void
    */
    void ShuffleCards()
    {
        /* Fisher-Yates Shuffle Algorithm */
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            GameObject temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    /**
    * Create a list of all cards needed for the blue minigame
    * 
    * @param void
    * @return void
    */
    void CreateListofCards()
    {
        for(int i = 0; i < cardTypes.Length; ++i)
        {
            /* Instantiate two of each card to make a pair */
            for (int j = 0; j < 2; ++j)
            {
                GameObject c = Instantiate(card);
                c.GetComponent<CardLogic>().SetCard(cardTypes[i]);
                cards.Add(c);
            }
        }
    }

    /**
    * Temporarily show the card
    * 
    * @param c - card that needs to be shown
    * @return void
    */
    IEnumerator ShowCard(GameObject c)
    {
        CardLogic logic = c.GetComponent<CardLogic>();
        c.GetComponent<SpriteRenderer>().sprite = logic.GetCard();
        logic.SetSelect(false);
        yield return new WaitForSeconds(2.0f);
        c.GetComponent<SpriteRenderer>().sprite = back;
        logic.SetSelect(true);
    }

    /**
    * Hide and unselect all cards
    * 
    * @param void
    * @return void
    */
    void HideAllCards()
    {
        selectedCards.Clear();
        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].GetComponent<SpriteRenderer>().sprite = back;
            cards[i].GetComponent<CardLogic>().ChangeConsideration(false);
            cards[i].GetComponent<CardLogic>().UnSelect();
        }
    }

    /**
    * Create a new memory puzzle
    * 
    * @param void
    * @return void
    */
    public void CreatePuzzle()
    {
        puzzleCreated = true;
        selectedCards.Clear();
        cards.Clear();
        CreateListofCards();
        ShuffleCards();
        MoveCards();
        for (int i = 0; i <= cards.Count; ++i) StartCoroutine(ShowCard(cards[i]));
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePuzzle();
    }

    void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            CardLogic logic = cards[i].GetComponent<CardLogic>();
            if (logic.CheckIfSelected() && !logic.CheckIfConsidered())
            {
                cards[i].GetComponent<SpriteRenderer>().sprite = cards[i].GetComponent<CardLogic>().GetCard();
                selectedCards.Add(cards[i]);
                logic.ChangeConsideration(true);
            }
        }

        if (selectedCards.Count >= 2)
        {
            if (selectedCards.Count > 2) HideAllCards();
            else
            {
                if (selectedCards[0].GetComponent<CardLogic>().GetCard() == selectedCards[1].GetComponent<CardLogic>().GetCard())
                {
                    for (int i = 0; i < selectedCards.Count; ++i)
                    {
                        cards.Remove(selectedCards[i]);
                        Destroy(selectedCards[i]);
                    }
                    selectedCards.Clear();
                }
                else HideAllCards();
            }
        }

        if (puzzleCreated && cards.Count == 0)
        {
            Debug.Log("shuffle red!");
            puzzleCreated = false;
            CreatePuzzle();
        }
    }
}
