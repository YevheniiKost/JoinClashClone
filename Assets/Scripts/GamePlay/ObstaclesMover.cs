using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ObstaclesMover : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _duration;
    [SerializeField] private MoveType _moveType;


    private void Start()
    {
        switch (_moveType)
        {
            case MoveType.UpDown:
                MoveUp();
                break;
            case MoveType.LeftRight:
                MoveLeft();
                break;
            case MoveType.Rotating:
                Rotate();
                break;
            default:
                break;
        }
    }

    private void Rotate()
    {
        transform.DORotate(new Vector3(0, 0, 180), _duration).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void MoveLeft()
    {
        transform.DOMoveX(transform.position.x - _amplitude, _duration).SetEase(Ease.OutCubic).OnComplete(MoveRight);
    }
    private void MoveRight()
    {
        transform.DOMoveX(transform.position.x + _amplitude, _duration).SetEase(Ease.OutCubic).OnComplete(MoveLeft);
    }

    private void MoveDown()
    {
        transform.DOMoveY(transform.position.y - _amplitude, _duration).SetEase(Ease.OutCubic).OnComplete(MoveUp);
    }
    private void MoveUp()
    {
        transform.DOMoveY(transform.position.y + _amplitude, _duration).SetEase(Ease.OutCubic).OnComplete(MoveDown);
    }
}

public enum MoveType
{
    UpDown,
    LeftRight,
    Rotating
}
