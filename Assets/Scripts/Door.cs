using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Door : MonoBehaviour
{
    private bool canLeave = false;

    public static event Action WinGame;

    [SerializeField] InputActionAsset _playerInput;

    private InputAction _leave;
    void Start()
    {
        ListController.OnAllItemsCollected += updateLeave;

        _leave = _playerInput.FindActionMap("Player").FindAction("Leave");
        _leave.Enable();
        _leave.performed += Leave;
    }

    void updateLeave()
    {
        if (!canLeave)
        {
            canLeave = true;
        }
    }

    void Leave(InputAction.CallbackContext context)
    {
        if(canLeave)
        {
            WinGame?.Invoke();
        }
    }
}
