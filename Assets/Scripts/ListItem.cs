using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    Blanket,
    Diary,
    SillyBand,
    TeddyBear
}
public class ListItem : MonoBehaviour
{
    [SerializeField]
    private itemType _itemType;

    public itemType ItemType { get { return _itemType; } set { _itemType = value; } }
}
