using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int _bossHealth;
    [SerializeField] GameObject _separatedPrefab;

    public bool IsDead => _isDead;

    private bool _isDead;
    private bool _isFightStarted;
    private HeroController _hero;

    public void StartFigth(HeroController hero)
    {
        _isFightStarted = true;
        _hero = hero;
    }
    
    public void HitBoss()
    {
        _bossHealth--;
        if(_bossHealth <= 0)
        {
            GameEvents.RaiseOnGameWinEvent();
            Instantiate(_separatedPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _isFightStarted = false;
        _isDead = false;
    }

    private void Update()
    {
        if (_isFightStarted)
        {
            transform.LookAt(_hero.transform.position);
        }
    }

}
