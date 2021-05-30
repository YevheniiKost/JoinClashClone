using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private InputHandler _input;
    private int _motionBlendParam;

    private void Awake()
    {
        _motionBlendParam = Animator.StringToHash("MotionBlend");

        GameEvents.OnImplementInputHandler += AddInputHandler;
    }

    private void OnDestroy()
    {
        GameEvents.OnImplementInputHandler += AddInputHandler;
    }

    private void Update()
    {
        ProcessLocomotion();
    }

    private void ProcessLocomotion()
    {
        if (_input.IsMoving)
            _animator.SetFloat(_motionBlendParam, 1);
        else
            _animator.SetFloat(_motionBlendParam, 0);
    }

    private void AddInputHandler(InputHandler input)
    {
        _input = input;
    }
}
