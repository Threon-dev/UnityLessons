using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey.MonoBehaviours;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Spiderman spiderman;

    private void Awake()
    {
        WebFloorParticles.Init();
        cameraFollow.Setup(GetCameraFollowPosition, ()=> 60f );
    }

    private void Start()
    {
        FunctionPeriodic.Create(()=> Enemy.Create(spiderman.GetPosition() + UtilsClass.GetRandomDir() * 80f),2f);
        //Enemy enemy = Enemy.Create(new Vector3(30, 0));
        //Enemy.Create(new Vector3(10, 0));
        //FunctionPeriodic.Create(() => enemy.Damage(spiderman.GetPosition()),1f);
        /*
        FunctionPeriodic.Create(() => {
            WebProjectile.Create(spiderman.GetPosition(),UtilsClass.GetRandomDir());
        }, 0.5f);
        */
    }
    private Vector3 GetCameraFollowPosition()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 spidermanToMouseDir = mousePosition - spiderman.GetPosition();
        return spiderman.GetPosition() + spidermanToMouseDir * .3f;
    }
}
