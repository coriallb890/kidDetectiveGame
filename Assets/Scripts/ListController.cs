using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ListController : MonoBehaviour
{
    public static event Action OnAllItemsCollected;

    [SerializeField]
    private List<Image> _scratchOut = new List<Image>();
    [SerializeField]
    private TextMeshProUGUI _blanket;
    [SerializeField]
    private TextMeshProUGUI _TeddyBear;
    [SerializeField]
    private TextMeshProUGUI _sillyBandz;
    [SerializeField]
    private TextMeshProUGUI _diary;
    [SerializeField]
    private TextMeshProUGUI _mushy;
    [SerializeField]
    private TextMeshProUGUI _frog;
    [SerializeField]
    private TextMeshProUGUI _fCard;


    [SerializeField]
    private AudioClip _scratchOff;
    [SerializeField]
    private AudioClip _openList;
    [SerializeField]
    private int _numItems = 0;

    private bool _firstEnable = true;


    private void Start()
    {
        Interactable.OnListItemPickUp += checkItemFound;
    }
    private void OnEnable()
    {
        if (_firstEnable)
        {
            _firstEnable = false;
        }
        else if (!_firstEnable)
        {
            SoundManager.Instance.PlaySound(_openList);
        }
    }

    private void OnDestroy()
    {
        Interactable.OnListItemPickUp -= checkItemFound;
    }


    void checkItemFound(listItem foundItem)
    {
        SoundManager.Instance.PlaySound(_scratchOff);
        Image newScratch = Instantiate(_scratchOut[UnityEngine.Random.Range(0, _scratchOut.Count - 1)]);
        newScratch.transform.SetParent(transform);
        newScratch.rectTransform.localScale = new Vector3(5.15f, 0.452f, 0.452f);
        _numItems++;
        switch (foundItem)
        {
            case listItem.Blanket:
                newScratch.rectTransform.localPosition = new Vector3(0, _blanket.rectTransform.localPosition.y, _blanket.rectTransform.localPosition.z);
                break;
            case listItem.SillyBand:
                newScratch.rectTransform.localPosition = new Vector3(0, _sillyBandz.rectTransform.localPosition.y, _sillyBandz.rectTransform.localPosition.z);
                break;
            case listItem.TeddyBear:
                newScratch.rectTransform.localPosition = new Vector3(0, _TeddyBear.rectTransform.localPosition.y, _TeddyBear.rectTransform.localPosition.z);
                break;
            case listItem.Diary:
                newScratch.rectTransform.localPosition = new Vector3(0, _diary.rectTransform.localPosition.y, _diary.rectTransform.localPosition.z);
                break;
            case listItem.Mushy:
                newScratch.rectTransform.localPosition = new Vector3(0, _mushy.rectTransform.localPosition.y, _mushy.rectTransform.localPosition.z);
                break;
            case listItem.Froggy:
                newScratch.rectTransform.localPosition = new Vector3(0, _frog.rectTransform.localPosition.y, _frog.rectTransform.localPosition.z);
                break;
            case listItem.FCard:
                newScratch.rectTransform.localPosition = new Vector3(0, _fCard.rectTransform.localPosition.y, _fCard.rectTransform.localPosition.z);
                break;
        }
        if(_numItems == 7)
        {
            OnAllItemsCollected?.Invoke();
        }
    }
}
