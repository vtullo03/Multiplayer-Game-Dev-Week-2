using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterLogic : MonoBehaviour
{
    private Controls controls;
    private char letter;
    private bool gotSelected = false;

    public void SetLetter(char l) { letter = l; }
    public char GetLetter() { return letter; }
    public bool IsSelected() { return gotSelected; }

    void OnTriggerEnter2D(Collider2D other)
    {
        controls = other.gameObject.GetComponent<Controls>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        controls = null;
    }

    void Update()
    {
        if (controls != null)
        {
            if (controls.ActionTriggered()) gotSelected = true;
        }
    }
}
