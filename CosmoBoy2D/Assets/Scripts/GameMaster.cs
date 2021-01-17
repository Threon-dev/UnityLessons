using System.Collections;
using UnityEngine;


public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    [SerializeField]
    private int startingMoney;
    public static int Money;

    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }
   
   
    
    private void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        
    }
    public Transform playerPrefab;
    public Transform spawnPoint;
    public Transform spawnPrefab;
    public float spawnDelay=2;
    public string respawnCountdownSoundName="RespawnCountdown";
    public string spawnSoundName="Spawn";
    public string gameOverSoundName = "GameOver";
    public CameraShake cameraShake;
    public UpgradeMenuCallback onToggleUpgradeMenu;
    public delegate void UpgradeMenuCallback(bool active);
    private AudioManager audioManager;

    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject upgradeMenu;
    [SerializeField]
    private WaveSpawner waveSpawner;

    private void Start()
    {
        if (cameraShake == null)
        {
            Debug.LogError("No cameraShake on the GM");
        }

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No audio manager found in Scene");
        }
        _remainingLives = maxLives;
        Money = startingMoney;       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleTheUpgradeMenu();
        }
    }
    private void ToggleTheUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpawner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf); 
    }
    public void EndGame()
    {
        audioManager.PlaySound(gameOverSoundName);
        Debug.Log("Game over");
        gameOverUI.SetActive(true);
    }
    public IEnumerator RespawnPlayer()
    {
        audioManager.PlaySound(respawnCountdownSoundName);
        yield return new WaitForSeconds(spawnDelay);
        audioManager.PlaySound(spawnSoundName);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);              
    }
    public static void KillPlayer(Player Player)
    {
        Destroy(Player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer());
        }       
    }
    public static void KillEnemy(Enemy Enemy)
    {
        gm._KillEnemy(Enemy);     
    }
    public void _KillEnemy(Enemy _enemy)
    {     
        audioManager.PlaySound(_enemy.deathSoundName);
        //GainMoney
        Money += _enemy.moneyDrop;
        audioManager.PlaySound("Money");

        //Add particles
      Transform _clone= Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_clone.gameObject, 5f);

        //Go camera shake
        cameraShake.Shake(_enemy.shakeAmt,_enemy.shakeLenght);
        Destroy(_enemy.gameObject);
    }
}
