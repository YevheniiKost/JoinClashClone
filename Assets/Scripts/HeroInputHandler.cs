using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInputHandler : MonoBehaviour
{
    public bool IsMoving => _isMoving;
    public float SideShift => _sideShift;

    private bool _isMoving;
    private float _sideShift;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _isMoving = true;
        } else
        {
            _isMoving = false;
        }
    }
}
