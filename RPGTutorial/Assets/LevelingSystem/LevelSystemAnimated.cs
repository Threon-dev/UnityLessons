using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
public class LevelSystemAnimated 
{
    private LevelSystem levelSystem;
    private bool isAnimating;
    private float updateTimer;
    private float updateTimerMax;

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int level;
    private int experience;

    public LevelSystemAnimated(LevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);
        updateTimerMax = .016f;
        FunctionUpdater.Create(() => Update());

        Application.targetFrameRate = 60;
    }
    public void SetLevelSystem(LevelSystem levelSystem) 
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperience(); 

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void Update()
    {

        if(isAnimating)
        {
            updateTimer += Time.deltaTime;
            while (updateTimer > updateTimerMax)
            {
                updateTimer -= updateTimerMax;
                UpdateAddExperience();  
            }
        }

        Debug.Log(level + " " + experience);
    }


    private void UpdateAddExperience()
    {
        if (level < levelSystem.GetLevelNumber())
        {
            AddExperience();
        }
        else
        {
            if (experience < levelSystem.GetExperience())
            {
                AddExperience();
            }
            else
            {
                isAnimating = false;
            }
        }
    }
    private void AddExperience()
    {
        experience++;
        if (experience > levelSystem.GetExperienceToNextLevel(level))
        {
            level++;
            experience = 0;
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
        }

        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        if (!levelSystem.isMaxLevel(level)) 
        { 
            return (float)experience / levelSystem.GetExperienceToNextLevel(level);
        }
        else
        {
            return 1f;
        }
    }
}
