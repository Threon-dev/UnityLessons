using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameAssets : MonoBehaviour
{
    public static MyGameAssets instanse;

    private void Awake()
    {
        instanse = this;
    }

    public Transform pfEnemy;
    public Transform pfEnemyFlyingBody;
    public Transform pfSpidermanWeb;

    public Transform webProjectile;
    public Transform pfWebFloor ;

}
