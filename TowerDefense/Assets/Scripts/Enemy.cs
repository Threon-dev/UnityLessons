using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [Header("Main")]
    public Renderer rend;
    public Material material;
    public float spawnEffectTime = 1f;
    public float effect = 1f;
    public float effectRate = 0.5f;

    [HideInInspector]
    public float speed;

    [Header("Stats")]
    public int worth;
    public float enemyStartHealth = 100;
    public float enemyHealth;
    public float startSpeed = 10f;


    [Header("Particles")]
    public GameObject enemyDeathParticle;

    [Header("Unity Stuff")]
    public Image healthBar;
    public GameObject healthBarObject;

    [HideInInspector]
    public bool isDead = false;

    [HideInInspector]
    public bool shieldIsOn = false;
    private void Start()
    {
        speed = startSpeed;
        enemyHealth = enemyStartHealth;
        material = new Material(material);
        rend.material = material;
        healthBarObject.SetActive(false);
    }

    private void Update()
    {
        material.SetFloat("EffectVector", effect);

        if (enemyHealth > 0f)
        {
            while (effect >= 0f)
            {
                effect -= Time.deltaTime * effectRate;
                return;
            }
            healthBarObject.SetActive(true);
        }

        if (enemyHealth <= 0f)
        {
            while (effect <= 1f)
            {
                effect += Time.deltaTime * effectRate * 2f;
                return;
            }
        }
    }
    public void HasDamaged(float amount)
    {
        if(shieldIsOn == false)
        {
            enemyHealth -= amount;

            healthBar.fillAmount = enemyHealth / enemyStartHealth;

            if (enemyHealth <= 0 && !isDead)
            {
                Die();
            }
        }
    }
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
    public void Die()
    {
        isDead = true;

        PlayerStats.Money += worth;

        GameObject enemyParticles = (GameObject)Instantiate(enemyDeathParticle, transform.position, transform.rotation);
        Destroy(enemyParticles, 3f);

        WaveSpawner.EnemiesAlive--;

        Debug.Log("Enemies alive: " + WaveSpawner.EnemiesAlive);

        Destroy(gameObject,1f);
    }
   
    
}
