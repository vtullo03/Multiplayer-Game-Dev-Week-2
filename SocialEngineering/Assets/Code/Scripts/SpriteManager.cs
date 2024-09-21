using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites;
    private int playerCounter = 0;

    public void OnPlayerJoined(PlayerInput input)
    {
        input.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[playerCounter];
        playerCounter++;
    }
}
