using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CameraRaycast : MonoBehaviour
{
    public static event Action<itemType> OnListItemPickUp;

    [SerializeField]
    private InputActionAsset _playerInput;
    [SerializeField]
    private Transform _holdPosition;

    private InputAction _grabItem;
    private bool _canGrab = false;
    private bool _holdingItem = false;
    private GameObject _itemPickedUp;

    private void Start()
    {
        _grabItem = _playerInput.FindActionMap("Player").FindAction("PickUp");
        _grabItem.Enable();
    }

    private void Update()
    {
        RaycastHit hit;

        if (!_holdingItem)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
            {
                if (hit.collider.CompareTag("pickUp") && !_canGrab)
                {
                    _itemPickedUp = hit.collider.gameObject;
                    _canGrab = true;
                    _grabItem.performed += handleItem;
                }
                if(hit.collider.CompareTag("listItem") && !_canGrab)
                {
                    _itemPickedUp = hit.collider.gameObject;
                    _grabItem.performed += handleListItem;
                }
            }
            else
            {
                if (!_canGrab)
                {
                    return;
                }
                else
                {
                    _itemPickedUp = null;
                    _canGrab = false;
                    _grabItem.performed -= handleItem;
                    _grabItem.performed -= handleListItem;
                }
            }
        }
    }
    
    void handleItem(InputAction.CallbackContext value)
    {
        if (!_holdingItem)
        {
            _holdingItem = true;
            _itemPickedUp.transform.position = _holdPosition.position;
            _itemPickedUp.GetComponent<Rigidbody>().useGravity = false;
            _itemPickedUp.transform.SetParent(_holdPosition);
        }
        else if (_holdingItem)
        {
            _holdingItem = false;
            _itemPickedUp.GetComponent<Rigidbody>().useGravity = true;
            _holdPosition.DetachChildren();
            _canGrab = false;
        }
    }

    void handleListItem(InputAction.CallbackContext value)
    {
        itemType item = _itemPickedUp.GetComponent<ListItem>().ItemType;
        OnListItemPickUp?.Invoke(item);
        Destroy(_itemPickedUp);
    }
}
