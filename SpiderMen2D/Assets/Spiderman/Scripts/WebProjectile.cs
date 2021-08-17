using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class WebProjectile : MonoBehaviour
{
    public static event EventHandler onWebReachedMaxDistance;
    public static event EventHandler onWebHitObject;
    public static WebProjectile Create(Vector3 position, Vector3 direction)
    {
        Transform webProjectileTransform = Instantiate(MyGameAssets.instanse.webProjectile,position,Quaternion.identity);
        WebProjectile webProjectile =  webProjectileTransform.GetComponent<WebProjectile>();
        webProjectile.Setup(direction);

        return webProjectile;
    }

    private const float SPEED = 200f;
    private const float DISTANCE_TRABELLED_MAX = 100f;

    private Vector3 dir;
    private float distanceTravelled;

    private void Awake()
    {
        Setup(new Vector3(1,0));
    }
    private void Setup(Vector3 dir)
    {
        this.dir = dir;
        transform.eulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVectorFloat(dir  ));
    }

    private void Update()
    {
        transform.position += dir * SPEED * Time.deltaTime;

        distanceTravelled += SPEED * Time.deltaTime;
        if (distanceTravelled > DISTANCE_TRABELLED_MAX)
        {
            onWebReachedMaxDistance?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onWebHitObject?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);   
    }
}
