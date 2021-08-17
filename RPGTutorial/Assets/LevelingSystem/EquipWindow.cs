using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class EquipWindow : MonoBehaviour
{
    [SerializeField] Player player;
    private LevelSystem levelSystem;
    private void Awake()
    {
        transform.Find("equipNoneBtn").GetComponent<Button_UI>().ClickFunc = () => player.SetEquip(Player.Equip.None);
        transform.Find("equipHelmet1Btn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            if (levelSystem.GetLevelNumber() >= 4)
                player.SetEquip(Player.Equip.Helmet_1);
        };
        transform.Find("equipHelmet2Btn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            if (levelSystem.GetLevelNumber() >= 9)
                player.SetEquip(Player.Equip.Helmet_2);
        };
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }
}
