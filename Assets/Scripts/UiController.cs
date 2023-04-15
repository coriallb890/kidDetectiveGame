using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class UiController : MonoBehaviour
{
    public static event Action<bool> OnListPress;

    [SerializeField]
    private InputActionAsset _playerInput;
    [SerializeField]
    private TextMeshProUGUI _interact;
    [SerializeField]
    private TextMeshProUGUI _pointer;
    [SerializeField]
    private GameObject _list;
    [SerializeField]
    private GameObject _Diary;

    private InputAction _listOn;
    private bool _listOpen = false;
    private bool _diaryOpen = false;

    private void Start()
    {
        _listOn = _playerInput.FindActionMap("Player").FindAction("OpenList");
        _listOn.Enable();
        _listOn.performed += toggleList;
        clearInteract();
        CameraRaycast.OnInteractable += updateInteract;
        CameraRaycast.OffInteractable += clearInteract;
        Interactable.OnOpenDiary += openDiaryStart;
        _list.SetActive(_listOpen);
        _Diary.SetActive(_diaryOpen);
    }

    void updateInteract()
    {
        _interact.text = "Press E";
    }

    void clearInteract()
    {
        _interact.text = "";
    }

    void toggleList(InputAction.CallbackContext value)
    {
        _listOpen = _listOpen ? false : true;
        OnListPress?.Invoke(_listOpen);
        _list.SetActive(_listOpen);
    }

    void openDiaryStart()
    {
        StartCoroutine(openDiary());
    }
    IEnumerator openDiary()
    {
        _Diary.SetActive(true);
        yield return new WaitForSeconds(2f);
        _Diary.SetActive(false);
        StopCoroutine(openDiary());
    }
}
