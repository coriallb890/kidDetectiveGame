using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PauseMenu : MonoBehaviour
{
    public static event Action onPause;
    public static event Action onResume;
    [SerializeField] InputActionAsset _playerInput;
    [SerializeField] GameObject pausePopup;

    private InputAction _pause;
    private void Start()
    {
        _pause = _playerInput.FindActionMap("Player").FindAction("Pause");
        _pause.Enable();
        _pause.performed += Pause;
    }

    private void Awake()
    {
        pausePopup.gameObject.SetActive(false);
    }
    public void Resume()
    {
        onResume?.Invoke();
        print("some");
        pausePopup.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        onPause?.Invoke();
        pausePopup.gameObject.SetActive(true);
        print("pausing");
    }
}
