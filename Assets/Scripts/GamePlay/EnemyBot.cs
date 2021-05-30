using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _separateEnemyBotPrefab;

    public void DestroyEnemyBot()
    {
        Instantiate(_separateEnemyBotPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HeroController hero))
        {
            hero.AttackEnemy(this);
        }
    }
}
