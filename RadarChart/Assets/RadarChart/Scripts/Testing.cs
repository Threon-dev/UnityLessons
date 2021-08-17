using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class Testing : MonoBehaviour
{
    [SerializeField] private UI_StatsRadarChart statsRadarChart;
    private void Awake()
    {
        Stats stats = new Stats(10,2,5,15,20);
        statsRadarChart.SetStats(stats);


        CMDebug.ButtonUI(new Vector2(0, 20), "ATK++", () => stats.IncreaseStatAmount(Stats.Type.Attack));
        CMDebug.ButtonUI(new Vector2(0, -20), "ATK--", () => stats.DecreaseStatAmount(Stats.Type.Attack));

        CMDebug.ButtonUI(new Vector2(100, 20), "DEF++", () => stats.IncreaseStatAmount(Stats.Type.Defence));
        CMDebug.ButtonUI(new Vector2(100, -20), "DEF--", () => stats.DecreaseStatAmount(Stats.Type.Defence));

        CMDebug.ButtonUI(new Vector2(200, 20), "SPD++", () => stats.IncreaseStatAmount(Stats.Type.Speed));
        CMDebug.ButtonUI(new Vector2(200, -20), "SPD--", () => stats.DecreaseStatAmount(Stats.Type.Speed));

        CMDebug.ButtonUI(new Vector2(300, 20), "HP++", () => stats.IncreaseStatAmount(Stats.Type.Health));
        CMDebug.ButtonUI(new Vector2(300, -20), "HP--", () => stats.DecreaseStatAmount(Stats.Type.Health));

        CMDebug.ButtonUI(new Vector2(400, 20), "MP++", () => stats.IncreaseStatAmount(Stats.Type.Mana));
        CMDebug.ButtonUI(new Vector2(400, -20), "MP--", () => stats.DecreaseStatAmount(Stats.Type.Mana));
    }
}
