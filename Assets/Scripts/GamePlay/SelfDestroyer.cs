using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [SerializeField] private float _destroyDelay;
    private void OnEnable()
    {
        Destroy(this.gameObject, _destroyDelay);   
    }
}
