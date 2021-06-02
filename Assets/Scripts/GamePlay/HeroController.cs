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
    [SerializeField] private float _bossKillBotCooldown = 1f;

    public List<Bot> BotsList => _bots;
    public bool IsFighting => !_isContolledByPlayer;

    private float _currentLeftSideMovementLimit;
    private float _currentRightSideMovementLimit;
    private List<Bot> _bots = new List<Bot>();
    private InputHandler _input;
    private bool _isContolledByPlayer;

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
            return;
        }

        var bot = _bots.OrderBy(t => -t.transform.position.z).FirstOrDefault();
        _bots.Remove(bot);
        StartCoroutine(AttackEnemyCoroutine(bot, enemyBot));
    }

    public void StartBossFight(Boss boss)
    {
        StartCoroutine(BossFight(boss));
        _isContolledByPlayer = false;
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
        _isContolledByPlayer = true;
        StartCoroutine(ResetLimitsCoroutine());
    }

    private void Update()
    {
        if (_input.IsMoving && _isContolledByPlayer)
        {
            MoveForward();
        }
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

    private IEnumerator BossFight(Boss boss)
    {
        for (int i = 0; i < _bots.Count; i++)
        {
            if (boss == null)
            {
                yield return null;
                break;
            }
            var bot = _bots[i];
            bot.transform.DOMove(boss.transform.position, _bossKillBotCooldown);

            yield return new WaitForSeconds(_bossKillBotCooldown);
            RemoveBotFromList(bot);
            bot.DestroyBot();
            boss.HitBoss();
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
        bot.SetFightingAnimation(true);

        yield return new WaitForSeconds(_timeForBotToRunToEnemy);

        bot.DestroyBot();
        enemyBot.DestroyEnemyBot();
    }

    private void AddInputHandler(InputHandler input)
    {
        _input = input;
    }
}
