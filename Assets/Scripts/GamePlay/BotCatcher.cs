using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCatcher : MonoBehaviour
{
    [SerializeField] private HeroController _hero;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bot bot))
        {
            bot.transform.parent = this.transform.parent;
            bot.SetPlayerColor();
            _hero.AddBotToList(bot);
        }
    }
}
