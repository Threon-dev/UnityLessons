using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public int worth;

    public float enemyHealth = 100;

    public GameObject enemyDeathParticle;

    private void Start()
    {
        speed = startSpeed;
    }

    public void HasDamaged(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            GameObject enemyParticles=(GameObject)Instantiate(enemyDeathParticle, transform.position, transform.rotation);
            Die();
            Destroy(enemyParticles, 3f);
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
    }
   
    
}
