using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerPlayer))]
public class Player : MonoBehaviour
{
   
   
    AudioManager audioManager;
    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";
    public PlayerStats stats;

    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start()
    {
        stats = PlayerStats.instance;

        stats.curHealth = stats.maxHealth;
       
        if (statusIndicator == null)
        {
           
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in MENU");
        }
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        InvokeRepeating("RegenHealth", 1f/stats.regenRate,1f/stats.regenRate);
    }
    void RegenHealth()
    {
        stats.curHealth += 1;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
    private void Update()
    {
        if(transform.position.y <= fallBoundary)
        {
            DamagePlayer(99999);
        }
    }

    void OnUpgradeMenuToggle(bool active)
    {
        GetComponent<PlatformerPlayer>().enabled = !active;
        Weapon _weapon = GetComponentInChildren<Weapon>();
        if (_weapon != null)
            _weapon.enabled = !active;
    }
    private void OnDestroy()
    {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }
    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            audioManager.PlaySound(deathSoundName);
            GameMaster.KillPlayer(this);
        }
        else
        {
            audioManager.PlaySound(damageSoundName);
        }
       
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);    
    }
}
