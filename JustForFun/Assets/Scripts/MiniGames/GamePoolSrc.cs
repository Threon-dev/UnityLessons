using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePoolSrc : MonoBehaviour
{
    public enum GameState
    {
        IDLE = 0,
        START = 1,
        PLAY = 2,
        WIN = 3,
        STOP = 4,
    }

    public GameObject MainScreen;
    public GameObject ProgBar;
    public GameObject WinBar;
    public Sprite[] WinPool;
    public Material ProgBarMt;
    public GameObject[] GamesObj;
    public Transform StageBar;
    public GameObject StagePref;

    IRoomGame[] Games;
    GameObject[] StageMark;
    int ID = 0;

    IEnumerator Start()
    {
        Physics.gravity = -Vector3.up * 90f;
        MainScreen.SetActive(true);
        ProgBar.SetActive(false);

        if (PlayerPrefs.HasKey("MiniGameStage"))
        {
            ID = PlayerPrefs.GetInt("MiniGameStage");
        } else
        {
            PlayerPrefs.SetInt("MiniGameStage", ID);
        }

        yield return new WaitForSeconds(0.1f);

        Games = new IRoomGame[GamesObj.Length];
        StageMark = new GameObject[GamesObj.Length];
        for (int _id = 0; _id < GamesObj.Length; _id++) {
            Games[_id] = GamesObj[_id].GetComponent<IRoomGame>();

            Transform _obj = Instantiate(StagePref, StageBar).transform;
            StageMark[_id] = _obj.GetChild(1).gameObject;
            StageMark[_id].SetActive(ID > _id);
            if (StageMark[_id].activeSelf)
            {
                Games[_id].SetState(GameState.STOP);
            }
            _obj.GetChild(2).GetComponent<Text>().text = (_id + 1).ToString();
        }
    }

    private void FixedUpdate()
    {
        if (Games == null) return;
        if (Games[ID].State == GameState.PLAY)
        {
            ProgBarMt.SetFloat("_Progress", (Games[ID].ProgressValue) * 0.5f);
            if (Games[ID].ProgressValue > 0.9f && !Input.GetMouseButton(0)) StopGame();
        }
    }

    public void StartGame()
    {
        MainScreen.SetActive(false);
        ProgBar.SetActive(true);
        Games[ID].SetState(GameState.START);
    }

    public void StopGame()
    {
        //MainScreen.SetActive(true);
        MainScreen.transform.GetChild(2).gameObject.SetActive(false);
        //ProgBar.SetActive(false);
        Games[ID].SetState(GameState.WIN);
        StageMark[ID].SetActive(true);
        ID++;
        if (ID > Games.Length - 1) ID = 0;
        PlayerPrefs.SetInt("MiniGameStage", ID);
        //SceneManager.LoadScene(PlayerPrefs.GetInt("LevelID"));
        StartCoroutine(FinishGame());
    }

    IEnumerator FinishGame()
    {
        WinBar.SetActive(true);
        WinBar.transform.GetChild(0).GetComponent<Image>().sprite = WinPool[Random.Range(0, WinPool.Length)];
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelID"));
    }
}
