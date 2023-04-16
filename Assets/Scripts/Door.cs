using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Door : MonoBehaviour
{
    private bool looking = false;
    private bool jobsDone = false;

    public static event Action WinGame;

    [SerializeField] InputActionAsset _playerInput;

    private InputAction _leave;
    void Start()
    {
        ListController.OnAllItemsCollected += updateJobs;
        CameraRaycast.OnDoor += isLook;
        CameraRaycast.OffInteractable += notLook;

        _leave = _playerInput.FindActionMap("Player").FindAction("Leave");
        _leave.Enable();
        _leave.performed += Leave;
    }

    private void OnDestroy()
    {
        ListController.OnAllItemsCollected -= updateJobs;
        CameraRaycast.OnDoor -= isLook;
        CameraRaycast.OffInteractable -= notLook;
        _leave.performed -= Leave;
    }

    void updateJobs()
    {
        if (!jobsDone)
        {
            jobsDone = true;
        }
    }

    void isLook()
    {
        if(!looking)
        {
            looking = true;
        }
    }

    void notLook()
    {
        if (looking)
        {
            looking = false;
        }
    }

    void Leave(InputAction.CallbackContext context)
    {
        if(looking && jobsDone)
        {
            WinGame?.Invoke();
        }
    }
}
