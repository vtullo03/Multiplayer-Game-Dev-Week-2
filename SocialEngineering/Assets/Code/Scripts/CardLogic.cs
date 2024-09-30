using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogic : MonoBehaviour
{
    private Sprite cardType;
    private Controls controls;

    private bool canSelect = true;
    private bool gotSelected = false;
    private bool considered = false;

    public void SetCard(Sprite type) { cardType = type; }
    public Sprite GetCard() { return cardType; }
    public void SetSelect(bool sel) { canSelect = sel; }
    public bool CheckIfSelected() { return gotSelected; }
    public void UnSelect() { gotSelected = false; }
    public void ChangeConsideration(bool con) { considered = con; }
    public bool CheckIfConsidered() { return considered; }

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
            if (controls.ActionTriggered() && canSelect) gotSelected = true;
        }
    }
}
