using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    /* Does not work with Xbox debug later */
    public void RumblePulse(float lowFreq, float highFreq, float duration)
    {
        Gamepad pad = Gamepad.current; 

        /* Check if player is actually using a gamepad -- they should be! */
        if (pad != null)
        {
            pad.SetMotorSpeeds(lowFreq, highFreq);
            StartCoroutine(StopRumble(duration, pad));

        }
    }

    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elaspedTime = 0.0f;
        while (elaspedTime < duration)
        {
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        pad.SetMotorSpeeds(0.0f, 0.0f);
    }
}
