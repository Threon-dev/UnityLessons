using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject completeLevelUI;
    [HideInInspector]
    public bool levelComplete;

    AudioManager am;

    public string mainLevelSound = "MainLevelSound";
    public string victorySound = "VictorySound";

    private void Awake()
    {
        instance = GetComponent<GameManager>();
        levelComplete = false;
    }

    private void Start()
    {
        am = AudioManager.instance;
        am.PlaySound(mainLevelSound);
    }

    private void Update()
    {
        if (levelComplete == false)
        {
            GameObject count = GameObject.FindGameObjectWithTag("Leaves");

            if (count == null)
            {
                Debug.Log("Level Complete");
            }
            else
            {
                return;
            }

            levelComplete = true;
            completeLevelUI.SetActive(true);
            am.PlaySound(victorySound);
        }       
    }
}
