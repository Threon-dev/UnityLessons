using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V_AnimationSystem;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour {

    public Transform pfHealthBar;

    void Start() {
        HealthSystem healthSystem = new HealthSystem(100);

        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(0, 10), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("Health:" + healthSystem.GetHealthPercent());
        healthSystem.Damage(10);
        Debug.Log("Health:" + healthSystem.GetHealthPercent());

        CMDebug.ButtonUI(new Vector2(100,100),"damage",() => {
            healthSystem.Damage(10);
            Debug.Log("Damaged:" + healthSystem.GetHealth());
        });

        CMDebug.ButtonUI(new Vector2(-100, 100), "heal", () => {
            healthSystem.Heal(10);
            Debug.Log("Healed:" + healthSystem.GetHealth());
        });
    }
}

