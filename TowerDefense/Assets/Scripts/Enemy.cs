using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
 
    [HideInInspector]
    public float speed;

    public int worth;

    public float enemyStartHealth = 100;
    private float enemyHealth;

    public GameObject enemyDeathParticle;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        enemyHealth = enemyStartHealth;
    } 

    public void HasDamaged(float amount)
    {
        enemyHealth -= amount;

        healthBar.fillAmount = enemyHealth/enemyStartHealth;

        if (enemyHealth <= 0)
        {       
            Die();          
        }
    }
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
    public void Die()
    {      
        PlayerStats.Money += worth;
        Destroy(gameObject);
        GameObject enemyParticles = (GameObject)Instantiate(enemyDeathParticle, transform.position, transform.rotation);
        Destroy(enemyParticles, 3f);
    }
   
    
}
