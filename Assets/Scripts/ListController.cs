using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListController : MonoBehaviour
{
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
    private AudioClip _scratchOff;
    [SerializeField]
    private AudioClip _openList;

    private bool _firstEnable = true;

    private void Awake()
    {
        Interactable.OnListItemPickUp += checkItemFound;
    }

    private void OnEnable()
    {
        if (_firstEnable)
        {
            _firstEnable = false;
        }
        else if(!_firstEnable)
        {
            SoundManager.Instance.PlaySound(_openList);
        }

    }

    void checkItemFound(listItem foundItem)
    {
        SoundManager.Instance.PlaySound(_scratchOff);
        Image newScratch = Instantiate(_scratchOut[Random.Range(0, _scratchOut.Count - 1)]);
        newScratch.transform.SetParent(transform);
        newScratch.rectTransform.localScale = new Vector3(5.15f, 0.452f, 0.452f);
        switch (foundItem)
        {
            case listItem.Blanket:
                newScratch.rectTransform.localPosition = new Vector3(0, _blanket.rectTransform.localPosition.y, _blanket.rectTransform.localPosition.z);
                break;
            case listItem.SillyBand:
                break;
            case listItem.TeddyBear:
                newScratch.rectTransform.localPosition = new Vector3(0, _TeddyBear.rectTransform.localPosition.y, _TeddyBear.rectTransform.localPosition.z);
                break;
            case listItem.Diary:
                newScratch.rectTransform.localPosition = new Vector3(0, _diary.rectTransform.localPosition.y, _diary.rectTransform.localPosition.z);
                break;
        }
    }
}
