﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] LevelWindow levelWindow;
    [SerializeField] Player player;
    [SerializeField] EquipWindow equipWindow;
    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        levelWindow.SetLevelSystem(levelSystem);
        equipWindow.SetLevelSystem(levelSystem);

        LevelSystemAnimated levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        levelWindow.SetLevelSystemAnimated(levelSystemAnimated);
        player.SetLevelSystemAnimated(levelSystemAnimated);
    }
}