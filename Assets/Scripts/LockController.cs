using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    [SerializeField]
    private List<int> _combination = new List<int>();

    private List<int> _playerCombination = new List<int> { 0, 0, 0, 0 };

    private void Start()
    {
        for (int i = 0; i < _combination.Count; i++)
        {
            _playerCombination.Add(Random.Range(0, 9));
        }
    }
}
