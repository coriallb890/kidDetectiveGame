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
    private TextMeshProUGUI _reticle;
    [SerializeField]
    private GameObject _list;
    [SerializeField]
    private GameObject _Diary;

    private InputAction _listOn;

    private bool _listOpen = false;
    private bool _diaryOpen = false;
    private bool _canLeave = false;
    private void Awake()
    {
        _listOn = _playerInput.FindActionMap("Player").FindAction("OpenList");
        _listOn.Enable();
        _listOn.performed += toggleList;
    }
    private void Start()
    {
        clearInteract();
        CameraRaycast.OnInteractable += updateInteract;
        CameraRaycast.OffInteractable += clearInteract;
        CameraRaycast.OnDoor += updateInteractDoor;
        CameraRaycast.OnLock += updateInteractLock;
        ListController.OnAllItemsCollected += updateLeave;



        Interactable.OnOpenDiary += openDiaryStart;
        _list.SetActive(_listOpen);
        _Diary.SetActive(_diaryOpen);
    }

    private void OnDestroy()
    {
        CameraRaycast.OnInteractable -= updateInteract;
        CameraRaycast.OffInteractable -= clearInteract;
        CameraRaycast.OnDoor -= updateInteractDoor;
        CameraRaycast.OnLock -= updateInteractLock;
        ListController.OnAllItemsCollected -= updateLeave;
        Interactable.OnOpenDiary -= openDiaryStart;
        _listOn.performed -= toggleList;
    }

    void updateInteract()
    {
        _interact.text = "Press E";
        _reticle.color = Color.white;
    }

    void clearInteract()
    {
        _interact.text = "";
        _reticle.color = Color.black;
    }

    void updateLeave()
    {
        _canLeave = true;
        print("Text change: "+_canLeave);
    }

    void updateInteractDoor()
    {
        if (_canLeave)
        {
            _interact.text = "Press R to Leave";
            _reticle.color = Color.white;
        }
        else
        {
            _interact.text = "Can't Leave Yet";
        }
    }

    private void updateInteractLock()
    {
        _interact.text = "Press F";
        _reticle.color = Color.white;
    }

    void toggleList(InputAction.CallbackContext value)
    {
        _listOpen = _listOpen ? false : true;
        OnListPress?.Invoke(_listOpen);
        print("list");
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
