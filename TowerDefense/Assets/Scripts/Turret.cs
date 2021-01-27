using UnityEngine;
public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy enemyTarget;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets(default)")]
    public bool standartBullet = false;
    public string standartBulletSound = "StandartBulletSound";

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public bool onEnableLaser = false;
    
    public int damageOverTime = 30;
    public float slowPct = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    public string laserSound = "LaserSound";

    [Header("Use missile")]
    public bool missile = false;

    public string missileSound = "MissileSound";

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;
    
    public Transform firePoint;

    AudioManager audioManager;

    AudioSource audioSource;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        audioSource = GetComponent<AudioSource>();

        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in scene");
        }
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position,enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            else
            {
                target = null;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemyTarget = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                    audioSource.Stop();
                }                    
            }
            return;
        }
             
        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }
    
    void LockOnTarget()
    {
        //target Lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        enemyTarget.HasDamaged(damageOverTime * Time.deltaTime);
        enemyTarget.Slow(slowPct);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
            audioSource.Play();
        }
          
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position+dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
    void Shoot()
    {
        GameObject bulletGO=(GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }

        if (missile == true)
            audioManager.PlaySound(missileSound);

        if (standartBullet == true)
            audioManager.PlaySound(standartBulletSound);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
