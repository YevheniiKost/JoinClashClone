using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents 
{
    public delegate void GameEvent();
    public delegate void GameEventInputHandler(InputHandler input);
    public delegate void GameEventBot(Bot bot);

    public static event GameEventInputHandler OnImplementInputHandler;
    public static event GameEventBot OnDestroyBot;
    public static event GameEvent OnPlayerDeath;
    public static event GameEvent OnGameStarted;
    public static event GameEvent OnGameWin;

    public static void RaiseOnGameWinEvent()
    {
        OnGameWin?.Invoke();
    }
    public static void RaiseOnGameStartedEvent()
    {
        OnGameStarted?.Invoke();
    }
    public static void RaiseOnPlayerDeathEvent()
    {
        OnPlayerDeath?.Invoke();
    }
    public static void RaiseOnImplementInputHandlerEvent(InputHandler input)
    {
        OnImplementInputHandler?.Invoke(input);
    }
    public static void RaiseOnDestroyBotEvent(Bot bot)
    {
        OnDestroyBot?.Invoke(bot);
    }
}
