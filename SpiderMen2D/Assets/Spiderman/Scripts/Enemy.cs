using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public static Enemy Create(Vector3 spawnPosition)
    {
        Transform enemyTransform = Instantiate(MyGameAssets.instanse.pfEnemy, spawnPosition, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();

        return enemy;
    }

    public static Enemy GetClosestEnemy(Vector3 position, float maxRange)
    {
        Enemy closest = null;
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.IsDead()) continue;
            if (Vector3.Distance(position, enemy.GetPosition()) <= maxRange)
            {
                if(closest == null)
                {
                    closest = enemy;
                }
                else
                {
                    if(Vector3.Distance(position,enemy.GetPosition()) < Vector3.Distance(position, closest.GetPosition()))
                    {
                        closest = enemy;
                    }
                }
            }
        }
        return closest;
    }

    private static List<Enemy> enemyList = new List<Enemy>();

    private const float SPEED = 30f;

    private Enemy_Base enemyBase;

    private int health;
    private Spiderman target;

    private State state;
    private enum State
    {
        Normal,
        Busy,
    }

    private void Awake()
    {
        enemyList.Add(this);
        enemyBase = gameObject.GetComponent<Enemy_Base>();
        health = 3;
        SetStateNormal();
    }

    private void Start()
    {
        target = Spiderman.instance;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleMovement();
                break;
            case State.Busy:
                break;
        }
    }
    private void HandleMovement()
    {
        float stopMovingDistance = 30f;
        if (Vector3.Distance(target.GetPosition(), GetPosition()) >= stopMovingDistance)
        {
            Vector3 targetDir = (target.GetPosition() - GetPosition()).normalized;
            transform.position += targetDir * SPEED * Time.deltaTime;
            enemyBase.PlayMoveAnim(targetDir);
        }
        else
        {
            //Stop
            enemyBase.PlayIdleAnim();
        }
    }
    private void SetStateNormal()
    {
        state = State.Normal;
    }
    private void SetStateBusy()
    {
        state = State.Busy;
    }
    public void Damage(Vector3 attackerPosition)
    {
        health -= 1;
        Vector3 dirToAttacker = (attackerPosition - GetPosition()).normalized;
        
        if (IsDead())
        {
            FlyingBody.Create(MyGameAssets.instanse.pfEnemyFlyingBody, GetPosition(), dirToAttacker * -1f);
            Destroy(gameObject);
        }
        else
        {
            SetStateBusy();
            float knockbackDistanse = 5f;
            transform.position += dirToAttacker * -1f * knockbackDistanse;
            enemyBase.PlayHitAnimation(dirToAttacker, SetStateNormal);
        }
    }

    public bool IsDead()
    {        
        return health <= 0;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
