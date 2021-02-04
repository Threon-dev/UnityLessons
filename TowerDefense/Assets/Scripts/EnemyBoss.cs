using System.Collections;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private Node turretTarget;

    [HideInInspector]
    public Transform target;

    Enemy enemy;

    public GameObject cam;

    [Header("Destroy")]
    public bool CanDestroy = false;
    public bool hasMeteor = true;
    private bool timeToDropMeteor = false;

    public GameObject meteorPrefab;
    public Transform spawnPosit;
    public GameObject targetSelectedParticle;
    public Transform particleSpawnPosit;

    public float range = 15f;
    public float destroyCountdown = 10f;
    public float nextTimeToDestroy = 10f;

    public float stopSpeedRate = 2f;

    public string turretTag = "turret";

    [Header("Regen Health Settings")]
    public bool HasRegenHealth = false;

    public float regenHealth = 1000f;
    public float timeToRegen = 15f;
    public float nextTimeToRegen = 20f;

    [Header("Shield Settings")]
    public bool HasShield = false;

    public Transform boss;
    public GameObject shieldGO;
    public float shieldTimer = 10f;

    private bool shieldIsBroken = false;
    [HideInInspector]
    public bool shieldOff = false;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void RegenHealth()
    {
        enemy.enemyHealth += regenHealth;
        Debug.Log("Boss regened 1000 health");
        Debug.Log("Current boss health" + enemy.enemyHealth);
    }

    IEnumerator GetShield()
    {
        InvokeRepeating("Destroy", shieldTimer + 2f, .2f);
        enemy.shieldIsOn = true;
        shieldIsBroken = true;
        GameObject shieldObj = (GameObject)Instantiate(shieldGO,boss);
        yield return new WaitForSeconds(shieldTimer);
        enemy.shieldIsOn = false;
        yield return new WaitForSeconds(1f);
        Destroy(shieldObj); 
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
                StartCoroutine(GetShield());
                shieldTimer -= Time.deltaTime;
            }            
        }

        if (CanDestroy == true)
        {
            destroyCountdown -= Time.deltaTime;

            if(hasMeteor == true)
            {
                FindTurret();           
            }

            if (destroyCountdown <= 0 && hasMeteor == true)
            {
                timeToDropMeteor = true;

                if (enemy.startSpeed <= 0)
                {
                    StartCoroutine(DropTheMeteor());               
                }
            }
        }

        if (timeToDropMeteor && enemy.startSpeed >= 0f)
        {
            enemy.startSpeed -= Time.deltaTime * stopSpeedRate;
        }
    }

    public void FindTurret()
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

        if (target != null)
        {
            Vector3 spawnPos = new Vector3(target.position.x, cam.transform.position.y + 5f, target.position.z);
            spawnPosit.transform.position = spawnPos;
            Vector3 targetSelected = new Vector3(target.position.x, target.position.y + 5f, target.position.z);
            particleSpawnPosit.transform.position = targetSelected;
        }
    }
    IEnumerator DropTheMeteor()
    {
        hasMeteor = false;
        GameObject meteor = (GameObject)Instantiate(meteorPrefab, spawnPosit.position,spawnPosit.rotation);
        GameObject targetSel = (GameObject)Instantiate(targetSelectedParticle, particleSpawnPosit.position,particleSpawnPosit.rotation);
        Destroy(targetSel,2f);
        Destroy(meteor, 10f);
        yield return new WaitForSeconds(3f);
        timeToDropMeteor = false;
        destroyCountdown = nextTimeToDestroy;
        enemy.startSpeed = 2.5f;
        hasMeteor = true;
    }
    private void Destroy()
    {
        GameObject destroy = GameObject.FindGameObjectWithTag("ShieldParticles");
        Destroy(destroy);
    }
    
}
