using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeroController))]
public class HeroAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private InputHandler _input;
    private int _motionBlendParam;
    private int _isFightingParam;

    private List<Bot> _bots;
    private HeroController _hero;

    private void Awake()
    {
        _motionBlendParam = Animator.StringToHash("MotionBlend");
        _isFightingParam = Animator.StringToHash("IsFighting");

        _hero = GetComponent<HeroController>();
        _bots = _hero.BotsList;

        GameEvents.OnImplementInputHandler += AddInputHandler;
    }

    private void OnDestroy()
    {
        GameEvents.OnImplementInputHandler += AddInputHandler;
    }

    private void Update()
    {
        ProcessLocomotion();
        ProcessFightingAnimation();
    }

    private void ProcessFightingAnimation()
    {
        _animator.SetBool(_isFightingParam, _hero.IsFighting);
        HandleBotFightAnimation(_hero.IsFighting);
    }

    private void ProcessLocomotion()
    {
        if (_input.IsMoving)
        {
            _animator.SetFloat(_motionBlendParam, 1);
            HandleBotAnimation(_input.IsMoving);
        }
        else
        {
            _animator.SetFloat(_motionBlendParam, 0);
            HandleBotAnimation(_input.IsMoving);
        }

    }

    private void AddInputHandler(InputHandler input)
    {
        _input = input;
    }

    private void HandleBotAnimation(bool isMoving)
    {
        if (_bots.Count <= 0)
            return;

        for (int i = 0; i < _bots.Count; i++)
        {
            var bot = _bots[i];
            bot.SetMovementAnimation(isMoving);
        }
    }

    private void HandleBotFightAnimation(bool isFighting)
    {
        if (_bots.Count <= 0)
            return;

        for (int i = 0; i < _bots.Count; i++)
        {
            var bot = _bots[i];
            bot.SetFightingAnimation(isFighting);
        }
    }
}
