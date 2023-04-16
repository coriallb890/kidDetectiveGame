using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.ComponentModel;
using UnityEngine.InputSystem;

public class LockController : MonoBehaviour
{
    public static event Action OnLockOpened;
    public static event Action OnLockPressed;
    public static event Action OnLockPressedAgain;

    [SerializeField]
    private List<int> _combination = new List<int>();

    private List<int> _playerCombination = new List<int> { 0, 0, 0, 0 };

    private int _playerComboIndex = 0;

    [SerializeField]
    private List<TextMeshProUGUI> _indexes = new List<TextMeshProUGUI>();

    [SerializeField] InputActionAsset _playerInput;
    [SerializeField] GameObject lockPopup;
    [SerializeField] GameObject lockModel;

    [SerializeField] GameObject reticle;
    [SerializeField] GameObject text;

    private InputAction _lock;

    private bool onLock = false;
    private bool lockPopupOpen = false;
    private bool unlocked = false;

    private void Start()
    {
        _lock = _playerInput.FindActionMap("Player").FindAction("Lock");
        _lock.Enable();
        _lock.performed += openLock;

        CameraRaycast.OnLock += islooking;
        CameraRaycast.OffInteractable += notlooking;

        lockPopup.SetActive(false);

        foreach (TextMeshProUGUI text in _indexes)
        {
            text.text = _playerCombination[0].ToString();
        }

        print(_combination);
        print(_playerCombination);
    }

    private void OnDestroy()
    {
        CameraRaycast.OnLock -= islooking;
        CameraRaycast.OffInteractable -= notlooking;
    }

    void openLock(InputAction.CallbackContext context)
    {
        if(unlocked)
        {
            return;
        }
        if(onLock && !lockPopupOpen)
        {
            reticle.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            lockPopupOpen = true;
            lockPopup.SetActive(true);
            OnLockPressed?.Invoke();
        }
        else if (lockPopupOpen)
        {
            reticle.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            lockPopupOpen = false;
            lockPopup.SetActive(false);
            OnLockPressedAgain?.Invoke();
        }
    }

    void islooking()
    {
        if(!onLock)
        {
            onLock = true;
        }
        print("looking at lock: " + onLock);
    }

    void notlooking()
    {
        if (onLock)
        {
            onLock = false;
        }
        print("looking at lock: " + onLock);
    }

    public void addValue(int index)
    {
        if(_playerCombination[index] == 9)
        {
            _playerCombination[index] = 0;
        }
        else
        {
            _playerCombination[index]++;
        }
        _indexes[index].text = _playerCombination[index].ToString();
    }

    public void subtrackValue(int index)
    {
        if (_playerCombination[index] == 0)
        {
            _playerCombination[index] = 9;
        }
        else
        {
            _playerCombination[index]--;
        }
        _indexes[index].text = _playerCombination[index].ToString();
    }

    public void checkCombo()
    {
        if (_combination[0] == _playerCombination[0] && _combination[1] == _playerCombination[1] && _combination[2] == _playerCombination[2] && _combination[3] == _playerCombination[3])
        {
            lockModel.gameObject.tag = "Untagged";
            unlocked = true;
            lockPopup.SetActive(false);
            OnLockPressedAgain?.Invoke();
        }
    }
}
