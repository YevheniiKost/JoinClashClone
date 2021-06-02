using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bot : MonoBehaviour
{
    [SerializeField] private Material _botMaterialWhite;
    [SerializeField] private Material _botMaterialYellow;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _separateBotPrefab;

    private int _motionBlendParam;

    public void SetPlayerColor()
    {
        SetColor(_botMaterialYellow);
    }

    public void SetMovementAnimation(bool isMoving)
    {
        _animator.SetFloat(_motionBlendParam, isMoving ? 1 : 0);
    }

    public void SetFightingAnimation(bool isFighting)
    {
        _animator.SetBool("IsFighting", isFighting);
    }

    public void DestroyBot()
    {
        Instantiate(_separateBotPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        _motionBlendParam = Animator.StringToHash("MotionBlend");
    }

    private void Start()
    {
        SetColor(_botMaterialWhite);
        SetMovementAnimation(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>())
        {
            GameEvents.RaiseOnDestroyBotEvent(this);
            DestroyBot();
        }
    }

    private void SetColor(Material mat)
    {
        _meshRenderer.material = mat;
    }
}
