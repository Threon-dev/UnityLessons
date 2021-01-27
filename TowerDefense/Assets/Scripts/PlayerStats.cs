using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int minimumLives;
    private int minLives = 0;

    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        minimumLives = minLives;

        Rounds = 0;
    }
}
