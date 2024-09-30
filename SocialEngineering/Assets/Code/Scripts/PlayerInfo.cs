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

    private RumbleManager rumbleManager;
    private AudioSource audioSource;
    public AudioClip HurtSound;
    private float volume = 1.0f;

    public void DeleteProtrait() { Destroy(currUI.gameObject); }

    public void LoseHealth()
    {
        rumbleManager.RumblePulse(0.25f, 0.75f, 1.0f);
        audioSource.PlayOneShot(HurtSound, volume);
        health--;
    }

    void Start()
    {
        rumbleManager = GetComponent<RumbleManager>();
        audioSource = GetComponent<AudioSource>();
    }
}
