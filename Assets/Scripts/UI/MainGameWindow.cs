using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private Button _pauseButton;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);

        GameEvents.OnGameStarted += StartGameHandler;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= StartGameHandler;
    }

    private void StartGameHandler()
    {
        _startText.gameObject.SetActive(false);
    }

    private void OnPauseButtonClick()
    {
        throw new NotImplementedException();
    }
}
