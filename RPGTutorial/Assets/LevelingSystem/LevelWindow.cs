using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class LevelWindow : MonoBehaviour
{
    private Text levelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;
    private void Awake()
    {
        levelText = transform.Find("levelText").GetComponent<Text>();
        experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();

        transform.Find("experienceButton5XP").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(5);
        transform.Find("experienceButton50XP").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(50);
        transform.Find("experienceButton500XP").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(500);
    }

    private void SetExpetienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL\n" + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;

        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExpetienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExpetienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }
}
