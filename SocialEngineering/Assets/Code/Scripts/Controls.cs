using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private PlayerInput playerInput;

    public Vector2 MoveInput() => playerInput.actions["Move"].ReadValue<Vector2>();

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
}
