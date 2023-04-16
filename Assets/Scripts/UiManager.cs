using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public static event Action onGameStop;
    public static event Action onResume;
    public static event Action onWin;
    public static event Action onLose;

    [SerializeField] InputActionAsset _playerInput;

    [SerializeField] AudioClip door;
    [SerializeField] AudioClip yippee;

    [SerializeField] GameObject pausePopup;
    [SerializeField] GameObject failPopup;
    [SerializeField] GameObject winPopUp;

    [SerializeField] GameObject reticle;
    [SerializeField] GameObject text;

    private InputAction _pause;
    private void OnEnable()
    {
        pausePopup.gameObject.SetActive(false);
        failPopup.gameObject.SetActive(false);
        winPopUp.gameObject.SetActive(false);
    }
    private void Start()
    {
        _pause = _playerInput.FindActionMap("Player").FindAction("Pause");
        _pause.Enable();
        _pause.performed += Pause;

        pausePopup.gameObject.SetActive(false);
        failPopup.gameObject.SetActive(false);
        winPopUp.gameObject.SetActive(false);

        Door.WinGame += Win;

        Instance = this;
    }
    private void OnDestroy()
    {
        Door.WinGame -= Win;
        _pause.performed -= Pause;
    }

    public void Resume()
    {
        reticle.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        onResume?.Invoke();
        pausePopup.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        onResume?.Invoke();
        pausePopup.gameObject.SetActive(false);
        failPopup.gameObject.SetActive(false);
        winPopUp.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        reticle.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        onGameStop?.Invoke();
        pausePopup.gameObject.SetActive(true);
    }

    public void Fail()
    {
        reticle.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        _pause.Disable();
        SoundManager.Instance.PlaySound(door);
        pausePopup.gameObject.SetActive(false);
        failPopup.gameObject.SetActive(true);
        onGameStop?.Invoke();
        onLose?.Invoke();
    }

    public void Win()
    {
        reticle.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        _pause.Disable();
        SoundManager.Instance.PlaySound(yippee);
        winPopUp.gameObject.SetActive(true);
        onGameStop?.Invoke();
        onWin?.Invoke();
    }
}
