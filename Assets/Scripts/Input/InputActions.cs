using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActions : MonoBehaviour
{
    public static InputActions Instance { get; private set; }
    public PlayerInput PlayerInput { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerInput = new PlayerInput();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PlayerInput.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
    }
}
