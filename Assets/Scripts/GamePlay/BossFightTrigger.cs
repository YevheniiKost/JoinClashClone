using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    private bool _isBossFightStarted;

    private void Start()
    {
        _isBossFightStarted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HeroController hero) && !_isBossFightStarted)
        {
            _isBossFightStarted = false;
            hero.StartBossFight(_boss);
            _boss.StartFigth(hero);
        }
    }
}
