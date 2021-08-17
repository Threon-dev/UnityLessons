using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;


    private static readonly int[] experiencePerLevel = new[] { 120, 140, 160, 180, 200, 220, 250, 280, 300, 400 };

    private int level;
    private int experience;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
    }

    public void AddExperience(int amount)
    {
        if (!isMaxLevel())
        {
            experience += amount;
            while (!isMaxLevel() && experience >= GetExperienceToNextLevel(level))
            {
                experience -= GetExperienceToNextLevel(level);
                level++;
                OnLevelChanged?.Invoke(this, EventArgs.Empty);
            }

            OnExperienceChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public int GetExperience()
    {
        return experience;
    }

    public int GetExperienceToNextLevel(int level)
    {
        if(level < experiencePerLevel.Length)
        {
            return experiencePerLevel[level];
        }
        else
        {
            Debug.LogError("Invalid level");
            return 100;
        }
    }
    public float GetExperienceNormalized()
    {
        if (!isMaxLevel())
        {
            return 1f;
        }
        else
        {
            return (float)experience / GetExperienceToNextLevel(level);
        } 
    }

    public bool isMaxLevel()
    {
        return isMaxLevel(level);
    }

    public bool isMaxLevel(int level)
    {
        return level == experiencePerLevel.Length - 1;
    }
}
