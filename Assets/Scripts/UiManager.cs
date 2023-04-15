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
    [SerializeField] InputActionAsset _playerInput;


    [SerializeField] GameObject pausePopup;
    [SerializeField] GameObject failPopup;

    private InputAction _pause;
    private void Start()
    {
        _pause = _playerInput.FindActionMap("Player").FindAction("Pause");
        _pause.Enable();
        _pause.performed += Pause;

        pausePopup.gameObject.SetActive(false);
        failPopup.gameObject.SetActive(false);

        Instance = this;
    }

    public void Resume()
    {
        onResume?.Invoke();
        pausePopup.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        onResume?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        onGameStop?.Invoke();
        pausePopup.gameObject.SetActive(true);
    }

    public void Fail()
    {
        onGameStop?.Invoke();
        pausePopup.gameObject.SetActive(false);
        failPopup.gameObject.SetActive(true);
    }
}
