using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        SceneManager.LoadScene(0);
    }
}
