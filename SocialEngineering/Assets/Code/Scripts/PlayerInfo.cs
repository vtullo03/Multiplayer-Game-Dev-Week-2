using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private int health;
    private Sprite protrait;

    public void SetHealth(int h) { health = h; }
    public void SetProtrait(Sprite p) { protrait = p; }

    public int GetHealth() { return health; } 
    public Sprite GetProtrait() { return protrait; }
}
