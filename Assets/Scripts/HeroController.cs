using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _sideMovementSpeed = 5f;

    [SerializeField] private InputCatcher _input;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_input.IsMoving)
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        Vector3 movementDirection = Vector3.forward * Time.deltaTime * _movementSpeed;
        
        if(Mathf.Abs(_input.SideShift) > 0)
        {
            movementDirection += Vector3.right * _input.SideShift * Time.deltaTime * _sideMovementSpeed;
        }

        transform.position += movementDirection;
    }
}
