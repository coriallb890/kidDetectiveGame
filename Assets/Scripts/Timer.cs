using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeRemaining;
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] AudioClip schoolBell;
    [SerializeField] AudioClip footsteps;

    public bool gameOn = true;
    public bool isPaused = false;

    private void Start()
    {
        timeRemaining += 1;
        UiManager.onGameStop += Paused;
        UiManager.onResume += Resumed;
    }

    private void OnDestroy()
    {
        UiManager.onGameStop -= Paused;
        UiManager.onResume -= Resumed;
    }
    void Update()
    {
        if(gameOn)
        {
            if (!isPaused)
            {
                timeRemaining -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            if (timeText.text == "00:00")
            {
                gameOn = false;
                UiManager.Instance.Fail();
                timeText.text = "";
            }

            if (timeText.text == "00:16")
            {
                SoundManager.Instance.PlaySound(footsteps);
            }

            if (timeText.text == "01:00")
            {
                SoundManager.Instance.PlaySound(schoolBell);
            }
        }
    }

    private void Paused()
    {
        if (!isPaused)
        {
            isPaused = true;
        }
    }

    private void Resumed()
    {
        if (isPaused)
        {
            isPaused = false;
        }
    }
}
