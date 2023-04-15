using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LockController : MonoBehaviour
{
    public static event Action OnLockOpened;

    [SerializeField]
    private List<int> _combination = new List<int>();

    private List<int> _playerCombination = new List<int> { 0, 0, 0, 0 };

    private int _playerComboIndex = 0;

    [SerializeField]
    private List<TextMeshProUGUI> _indexes = new List<TextMeshProUGUI>();

    [SerializeField]
    private Button _up;
    [SerializeField]
    private Button _down;
    [SerializeField]
    private Button _right;
    [SerializeField]
    private Button _left;
    [SerializeField]
    private Button _checkCombo;

    private void Start()
    {
        foreach (TextMeshProUGUI text in _indexes)
        {
            text.text = _playerCombination[0].ToString();
        }
    }

    public void increaseIndex()
    {
        if(_playerComboIndex == 0)
        {
            _playerComboIndex = _playerCombination.Count - 1;
        }
    }

    public void decreaseIndex()
    {
        if(_playerComboIndex == _playerCombination.Count - 1)
        {
            _playerComboIndex = 0;
        }
    }

    public void addValue()
    {
        if(_playerCombination[_playerComboIndex] == 9)
        {
            _playerCombination[_playerComboIndex] = 0;
        }
        else
        {
            _playerCombination[_playerComboIndex]++;
        }
        _indexes[_playerComboIndex].text = _playerCombination[_playerComboIndex].ToString();
    }

    public void subtrackValue()
    {
        if (_playerCombination[_playerComboIndex] == 0)
        {
            _playerCombination[_playerComboIndex] = 9;
        }
        else
        {
            _playerCombination[_playerComboIndex]--;
        }
        _indexes[_playerComboIndex].text = _playerCombination[_playerComboIndex].ToString();
    }
    
    public void checkCombo()
    {
        bool isEqual = true;
        for (int i = 0; i <= 3; i++)
        {
            if(_playerCombination[i] != _combination[i])
            {
                isEqual = false;
                break;
            }
        }
        if (!isEqual)
        {
            
        }
        else if (isEqual)
        {
            Debug.Log("Combo correct");
            OnLockOpened?.Invoke();
        }
    }
}
