using System.Collections;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private Node turretTarget;
    private Transform target;

    Enemy enemy;

    [Header("Destroy")]
    public bool CanDestroy = false;

    public float range = 15f;
    public float destroyCountdown = 10f;
    public float nextTimeToDestroy = 10f;

    public string turretTag = "turret";

    [Header("Regen Health Settings")]
    public bool HasRegenHealth = false;

    public float regenHealth = 1000f;
    public float timeToRegen = 15f;
    public float nextTimeToRegen = 20f;

    [Header("Shield Settings")]
    public bool HasShield = false;

    public GameObject shield;

    private bool shieldIsBroken = false;
    public float shieldTimer = 10f;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    IEnumerator DestroyTurret()
    {
        yield return new WaitForSeconds(2f);
    }

    void RegenHealth()
    {
        enemy.enemyHealth += regenHealth;
        Debug.Log("Boss regened 1000 health");
        Debug.Log("Current boss health" + enemy.enemyHealth);
    }

    void GetShield()
    { 
        enemy.shieldIsOn = true;
        shield.SetActive(true);
    }

    private void Update()
    {
        if(HasRegenHealth == true)
        {
            timeToRegen -= Time.deltaTime;

            if (timeToRegen <= 0)
            {
                if (enemy.enemyHealth <= enemy.enemyStartHealth - regenHealth)
                {
                    timeToRegen = nextTimeToRegen;
                    RegenHealth();
                }
            }
        }

        if(HasShield == true)
        {
            if (shieldIsBroken == false && enemy.enemyHealth <= enemy.enemyStartHealth / 2f)
            {
                GetShield();
                shieldTimer -= Time.deltaTime;
            }

            if (shieldTimer <= 0f)
            {
                enemy.shieldIsOn = false;
                shield.SetActive(false);
                shieldIsBroken = true;
            }
            
        }

        if (CanDestroy == true)
        {
            FindTurret();

            destroyCountdown -= Time.deltaTime;            
        }
    }

    void FindTurret()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTurret = null;

        foreach (GameObject turret in turrets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, turret.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestTurret = turret;
            }
            else
            {
                target = null;
            }
        }

        if (nearestTurret != null && shortestDistance <= range)
        {
            target = nearestTurret.transform;
            turretTarget = nearestTurret.GetComponent<Node>();
        }
        else
        {
            target = null;
        }

        if (destroyCountdown <= 0f)
        {
            if (target != null)
            {
                Destroy(nearestTurret);
                destroyCountdown = nextTimeToDestroy;
                StartCoroutine(Stopped());          
                return;
            }
            else
            {
                destroyCountdown = nextTimeToDestroy;
            }
        }       
    }
    IEnumerator Stopped()
    {
        enemy.startSpeed = 0f;
        yield return new WaitForSeconds(3);
        enemy.startSpeed = 2.5f;
    }
}
