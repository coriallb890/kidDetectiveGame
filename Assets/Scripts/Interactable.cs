using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum interactType
{
    pickUp,
    listItem,
    Cabinet
}
public class Interactable : MonoBehaviour
{
    public static event Action<interactType> OnListItemPickUp;

    [SerializeField]
    private interactType _interactType;
    [SerializeField]
    private float _openTime;

    private bool _isOpen = false;
    private bool _openCabinet = false;
    private bool _closeCabinet = false;
    private bool _beingHeld = false;
    private Rigidbody _myBody;
    private Vector3 _positionToGo;
    private Vector3 _startingPosition;

    private void Start()
    {
        _myBody = GetComponent<Rigidbody>();
        if(_interactType == interactType.Cabinet)
        {
            _startingPosition = transform.position;
            _positionToGo = transform.position + (transform.forward * 2f);
        }
    }

    public void Interact(Transform _parentLocation)
    {
        switch (_interactType)
        {
            case interactType.pickUp:
                if (!_beingHeld)
                {
                    transform.SetParent(_parentLocation);
                    _myBody.useGravity = false;
                    _beingHeld = true;
                    break;
                }
                else
                {
                    _myBody.useGravity = true;
                    transform.SetParent(null);
                    _beingHeld = false;
                    break;
                }
            case interactType.listItem:
                OnListItemPickUp?.Invoke(_interactType);
                Destroy(gameObject);
                break;
            case interactType.Cabinet:
                if (!_isOpen)
                {
                    _openCabinet = true;
                    break;
                }
                else
                {
                    _closeCabinet = true;
                    break;
                }
            default:
                break;
        }       
    }

    private void FixedUpdate()
    {
        if(_interactType != interactType.Cabinet)
        {
            return;
        }
        else
        {
            if (_openCabinet)
            {
                transform.position = Vector3.MoveTowards(transform.position, _positionToGo, _openTime * Time.deltaTime);
                if (transform.position == _positionToGo)
                {
                    _isOpen = true;
                    _openCabinet = false;
                }
            }
            else if (_closeCabinet)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startingPosition, _openTime * Time.deltaTime);
                if (transform.position == _startingPosition)
                {
                    _isOpen = false;
                    _closeCabinet = false;
                }
            }
        }
    }
}
