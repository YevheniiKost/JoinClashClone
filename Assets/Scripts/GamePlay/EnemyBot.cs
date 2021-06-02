using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _separateEnemyBotPrefab;

    private bool _isAttacked;

    private void Start()
    {
        _isAttacked = false;
    }

    public void DestroyEnemyBot()
    {
        Instantiate(_separateEnemyBotPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HeroController hero) && !_isAttacked)
        {
            hero.AttackEnemy(this);
            _isAttacked = true;
        }
    }
}
