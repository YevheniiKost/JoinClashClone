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

    public static void RaiseOnImplementInputHandlerEvent(InputHandler input)
    {
        OnImplementInputHandler?.Invoke(input);
    }
    public static void RaiseOnDestroyBotEvent(Bot bot)
    {
        OnDestroyBot?.Invoke(bot);
    }
}
