using UnityEngine;

public class PlayerManager : MonoBehaviour,IGameManager
{
    public ManagerStatus status { get; private set; }
    public int health { get; private set; }
   public int maxHealth { get; private set; }
    private NetworkSerivce _network;
    public void Startup(NetworkSerivce serivce)
    {
        Debug.Log("Player manager starting...");
        _network = serivce;
        UpdateData(50, 100);
        status = ManagerStatus.Started;
    }
    public void UpdateData(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    } 
    public void ChangeHealth(int value)
    {
        health += value;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health<0)
        {
            health = 0;
        }

        if (health == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }

        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
        Debug.Log("Health:" + health + "/" + maxHealth);
    }
    public void Respawn()
    {
        UpdateData(50, 100);
    }
}