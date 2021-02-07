using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
 
    [HideInInspector]
    public float speed;

    public int worth;

    public float enemyStartHealth = 100;
    public float enemyHealth;

    public GameObject enemyDeathParticle;

    [Header("Unity Stuff")]
    public Image healthBar;

    [HideInInspector]
    public bool isDead = false;

    [HideInInspector]
    public bool shieldIsOn = false;
    private void Start()
    {
        speed = startSpeed;
        enemyHealth = enemyStartHealth;
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
