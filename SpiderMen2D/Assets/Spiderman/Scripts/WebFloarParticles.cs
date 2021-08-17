using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WebFloorParticles
{
    public static void Init()
    {
        WebProjectile.onWebReachedMaxDistance += WebProjectile_onWebReachedMaxDistance;
        WebProjectile.onWebHitObject += WebProjectile_onWebHitObject;
    }

    private static void WebProjectile_onWebHitObject(object sender, System.EventArgs e)
    {
        WebProjectile webProjectile = sender as WebProjectile;
        Object.Instantiate(MyGameAssets.instanse.pfWebFloor, webProjectile.transform.position, Quaternion.identity);
    }

    private static void WebProjectile_onWebReachedMaxDistance(object sender, System.EventArgs e)
    {
        WebProjectile webProjectile = sender as WebProjectile;
        Object.Instantiate(MyGameAssets.instanse.pfWebFloor, webProjectile.transform.position, Quaternion.identity);
    }
}
