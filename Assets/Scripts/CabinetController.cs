using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    [SerializeField]
    private float _openTime;

    private bool _isOpen = false;
    private bool _openCabinet = false;
    private bool _closeCabinet = false;
    private Vector3 _positionToGo;
    private Vector3 _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position;
        _positionToGo = transform.position + (transform.forward * 2f);
    }

    public void Interact()
    {
        if (!_isOpen)
        {
            _openCabinet = true;
        }
        else if (_isOpen)
        {
            _closeCabinet = true;
        }
    }

    private void FixedUpdate()
    {
        if (_openCabinet)
        {
            transform.position = Vector3.MoveTowards(transform.position, _positionToGo, _openTime * Time.deltaTime);
            if(transform.position == _positionToGo)
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
