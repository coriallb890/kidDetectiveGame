using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CameraRaycast : MonoBehaviour
{
    public static event Action OnInteractable;
    public static event Action OffInteractable;
    public static event Action OnDoor;

    [SerializeField]
    private InputActionAsset _playerInput;
    [SerializeField]
    private Transform _holdPosition;

    private InputAction _grabItem;
    private bool _canGrab = false;
    private GameObject _itemPickedUp;
    private bool _listOpen = false;

    private void Start()
    {
        _grabItem = _playerInput.FindActionMap("Player").FindAction("PickUp");
        _grabItem.Enable();
        OffInteractable?.Invoke();
        UiController.OnListPress += toggleListOpen;
    }

    private void Update()
    {
        RaycastHit hit;
        if (_listOpen)
        {
            return;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f)) //if raycast hits something
        {
            if (hit.collider.CompareTag("Interactable") && !_canGrab) //only fires once when hitting interactable
            {
                OnInteractable?.Invoke();
                _itemPickedUp = hit.collider.gameObject;
                _grabItem.performed += handleItem;
                _canGrab = true;
            }

            if (hit.collider.CompareTag("Door") && !_canGrab)
            {
                OnDoor?.Invoke();
                _canGrab = true;
            }
        }
        else //if raycast misses
        {
            if (!_canGrab)
            {
                return;
            }
            else //allows for it to only fire once on interactable
            {
                OffInteractable?.Invoke();
                _itemPickedUp = null;
                _canGrab = false;
                _grabItem.performed -= handleItem;
            }
        }
    }
    
    void handleItem(InputAction.CallbackContext value)
    {
        _itemPickedUp.GetComponent<Interactable>().Interact(_holdPosition);
    }

    void toggleListOpen(bool toggle)
    {
        _listOpen = toggle;
    }
}
