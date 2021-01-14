using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth=100;
        public int damage = 40;

        private int _curHealth;
        public int curHealth 
        {
         get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }
    public EnemyStats _enemyStats = new EnemyStats();

    public Transform deathParticles;
    public float shakeAmt = 0.1f;
    public float shakeLenght = 0.1f;
    public int moneyDrop = 10;

    public string deathSoundName = "Explosion";

    [Header("Optional:  ")]
    [SerializeField]
    private StatusIndicator statusIndicator;
    

    public void Start()
    {
        
        _enemyStats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(_enemyStats.curHealth, _enemyStats.maxHealth);
        }
        if (deathParticles == null)
        {
            Debug.LogError("No particles references on Enemy");
        }
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;
    }


    void OnUpgradeMenuToggle(bool active)
    {
        GetComponent<EnemyAI>().enabled = !active;
        
    }
    public void DamageEnemy(int damage)
    {
        _enemyStats.curHealth -= damage;
        if (_enemyStats.curHealth <= 0)
        {
            
            GameMaster.KillEnemy(this);
        }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(_enemyStats.curHealth, _enemyStats.maxHealth);
        }

    }
    private void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(_enemyStats.damage);
            DamageEnemy(999999);
        }
    }
    private void OnDestroy()
    {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }
}
