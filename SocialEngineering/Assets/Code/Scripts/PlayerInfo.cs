using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private int health = 3;
    private Sprite protrait;
    private Image currUI;

    public void SetHealth(int h) { health = h; }
    public void SetProtrait(Sprite p) { protrait = p; }
    public void SetUI(Image image) { currUI = image; }

    public int GetHealth() { return health; } 
    public Sprite GetProtrait() { return protrait; }
    public Image GetUI() { return currUI; }
}
