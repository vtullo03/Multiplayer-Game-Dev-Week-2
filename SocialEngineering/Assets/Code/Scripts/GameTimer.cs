using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private Image circle;

    private float duration = 60f;
    private float remainingDuration;

    private bool timerDone;

    /**
    * Resets the remaining time variable and flag for the timer
    * 
    * @param void
    * @return void
    */
    public void ResetTimer()
    {
        timerDone = false;
        circle.fillAmount = 1;
        remainingDuration = duration;
        StartCoroutine(UpdateTimer());
    }

    /**
    * Counts down by a sec each and calculates the remaining time fraction for the circle fill
    * 
    * @param void
    * @return void
    */
    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            circle.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        timerDone = true;
    }

    /**
    * Get the circle UI component and immediately start the game timer
    * 
    * @param void
    * @return void
    */
    void Start()
    {
        circle = GetComponent<Image>();
        ResetTimer();
    }

    /* Getter function */
    public bool GetTimerStatus() { return timerDone; }
}
