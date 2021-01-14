 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 0; 
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    Transform firePoint;
    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    public Transform hitPrefab;

    //Handle camera shaking
    public float camShakeAmt = 0.05f;
    public float camShakeLenght = 0.1f;
    CameraShake camShake;

    public string weaponShootSound = "DefaultShot";



    AudioManager audioManager;


    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("There is no FirePoint! Please check in Weapon.cs");
        }
    }
    private void Start()
    {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null)
        {
            Debug.LogError("No CameraShake script found on GM object ");
        }
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in Weapon script");
        }
    }

    void Update()
    {
       
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
             
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }
   
    void Shoot()
    {
       
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition,100,whatToHit);
        
       
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemy Enemy = hit.collider.GetComponent<Enemy>();
            if(Enemy != null)
            {
                Enemy.DamageEnemy(Damage);
                //Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(99999, 99999, 99999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos,hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

    }
    void Effect(Vector3 hitPos,Vector3 hitNormal)
    {
        Transform trail = Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos); 
        } 
        Destroy(trail.gameObject, 0.04f);
        if(hitNormal!=new Vector3(99999, 99999, 99999))
        {
            Transform HitParticles = Instantiate(hitPrefab,hitPos,Quaternion.FromToRotation(Vector3.right,hitNormal)) as Transform;
            Destroy(HitParticles.gameObject,1f);
        }


        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.5f, 0.9f);
        clone.localScale = new Vector3(size, size,1);
        
        Destroy(clone.gameObject,0.02f);


        camShake.Shake(camShakeAmt, camShakeLenght);

        //Play shoot sound
        audioManager.PlaySound(weaponShootSound);
    }
}
