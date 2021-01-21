using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavePointIndex = 0;

    public int enemyDeathCost;

    public int enemyHealth = 100;

    public GameObject enemyDeathParticle;

    public void HasDamaged(int amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            GameObject enemyParticles=(GameObject)Instantiate(enemyDeathParticle, transform.position, transform.rotation);
            Die();
            Destroy(enemyParticles, 3f);
        }
    }
    public void Die()
    {
        PlayerStats.Money += enemyDeathCost;
        Destroy(gameObject);        
    }
    private void Start()
    {
        target = Waypoints.points[0];
       
    }
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }
    void GetNextWaypoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        
        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];        
    }
    void EndPath()
    {
        Destroy(gameObject);
        PlayerStats.Lives--;
    }
}
