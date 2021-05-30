using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class HeroController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _sideMovementSpeed = 5f;
    [SerializeField] private float _sideMovementStartLimit;
    [SerializeField] private float _minDistanceBetweenBots = .5f;
    [SerializeField] private float _botNewPositionDisnace = .8f;
    [SerializeField] private float _timeForBotToRunToEnemy = 1f;


    private float _currentLeftSideMovementLimit;
    private float _currentRightSideMovementLimit;
    private List<Bot> _bots = new List<Bot>();
    private InputHandler _input;


    public void AddBotToList(Bot bot)
    {
        for (int i = 0; i < _bots.Count; i++)
        {
            if (_bots[i] == bot)
            {
                return;
            }
            else { continue; }
        }

        ResetBotPosition(bot);
        _bots.Add(bot);
    }

    public void RemoveBotFromList(Bot bot)
    {
        for (int i = 0; i < _bots.Count; i++)
        {
            if (_bots[i] == bot)
            {
                _bots.Remove(_bots[i]);
            }
            else { continue; }
        }
    }

    public void AttackEnemy(EnemyBot enemyBot)
    {
        if(_bots.Count == 0)
        {
            GameEvents.RaiseOnPlayerDeathEvent();
        }

        var bot = _bots.OrderBy(t => t.transform.position.z).LastOrDefault();
        _bots.Remove(bot);
        StartCoroutine(AttackEnemyCoroutine(bot, enemyBot));
    }


    private void Awake()
    {
        GameEvents.OnDestroyBot += RemoveBotFromList;
        GameEvents.OnImplementInputHandler += AddInputHandler;
    }

    private void OnDestroy()
    {
        GameEvents.OnDestroyBot -= RemoveBotFromList;
        GameEvents.OnImplementInputHandler -= AddInputHandler;
    }

    private void Start()
    {
        StartCoroutine(ResetLimitsCoroutine());
    }

    private void Update()
    {
        if (_input.IsMoving)
        {
            MoveForward();
        }

        HandleBotAnimation(_input.IsMoving);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>())
        {
            GameEvents.RaiseOnPlayerDeathEvent();
        }
    }

    private IEnumerator ResetLimitsCoroutine()
    {
        while (true)
        {
            ResetMovementLimits();
            yield return new WaitForSeconds(.3f);
        }
    }

    private void MoveForward()
    {
        Vector3 movementDirection = Vector3.forward * Time.deltaTime * _movementSpeed;

        if (Mathf.Abs(_input.SideShift) > 0)
        {
            movementDirection += Vector3.right * _input.SideShift * Time.deltaTime * _sideMovementSpeed;
        }

        transform.position += movementDirection;

        Vector3 newPosition = transform.position;

        newPosition.x = Mathf.Clamp(newPosition.x, _currentLeftSideMovementLimit, _currentRightSideMovementLimit);
        transform.position = newPosition;
    }

    private void ResetMovementLimits()
    {
        if (_bots.Count > 0)
        {
            var rightBot = _bots.OrderBy(t => t.transform.position.x).Last();
            var leftBot = _bots.OrderBy(t => t.transform.position.x).First();


            if (rightBot.transform.position.x > 0)
            {
                _currentRightSideMovementLimit = _sideMovementStartLimit - (rightBot.transform.position.x - transform.position.x);
            }
            else if (rightBot.transform.position.x < 0)
            {
                _currentLeftSideMovementLimit = -_sideMovementStartLimit - (rightBot.transform.position.x - transform.position.x);
            }

            if (leftBot.transform.position.x < 0)
            {
                _currentLeftSideMovementLimit = -_sideMovementStartLimit - (leftBot.transform.position.x - transform.position.x);
            }
        }
        else
        {
            _currentRightSideMovementLimit = _sideMovementStartLimit;
            _currentLeftSideMovementLimit = -_sideMovementStartLimit;
        }
    }

    private void ResetBotPosition(Bot bot)
    {
        for (int i = 0; i < _bots.Count; i++)
        {
            if (Mathf.Abs(bot.transform.position.z - _bots[i].transform.position.z) < _minDistanceBetweenBots && Mathf.Abs(bot.transform.position.x - _bots[i].transform.position.x) < _minDistanceBetweenBots)
            {
                bot.transform.localPosition += new Vector3(0, 0, _botNewPositionDisnace);
            }
        }
    }


    private IEnumerator AttackEnemyCoroutine(Bot bot, EnemyBot enemyBot)
    {
        bot.transform.DOMove(enemyBot.transform.position, _timeForBotToRunToEnemy);

        yield return new WaitForSeconds(_timeForBotToRunToEnemy);

        bot.DestroyBot();
        enemyBot.DestroyEnemyBot();
    }

    private void HandleBotAnimation(bool isMoving)
    {
        if (_bots.Count <= 0)
            return;

        for (int i = 0; i < _bots.Count; i++)
        {
            _bots[i].SetMovementAnimation(isMoving);
        }
    }

    private void AddInputHandler(InputHandler input)
    {
        _input = input;
    }
}
