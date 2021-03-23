using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlatform : MonoBehaviour
{

    private void OnTriggerEnter(Collider platform)
    {
        if(platform.tag == "RedPlatforms")
        {
            GameObject[] redPlatforms =  GameObject.FindGameObjectsWithTag("RedPlatforms");

            Destroy(redPlatforms[0], 1f);
            Destroy(redPlatforms[1], 1f);
            Destroy(redPlatforms[2], 1f);
            Destroy(redPlatforms[3], 1f);
        }

        if (platform.tag == "BluePlatforms")
        {
            GameObject[] redPlatforms = GameObject.FindGameObjectsWithTag("BluePlatforms");

            Destroy(redPlatforms[0], 1f);
            Destroy(redPlatforms[1], 1f);
            Destroy(redPlatforms[2], 1f);
            Destroy(redPlatforms[3], 1f);
        }

        if (platform.tag == "YellowPlatforms")
        {
            GameObject[] redPlatforms = GameObject.FindGameObjectsWithTag("YellowPlatforms");

            Destroy(redPlatforms[0], 1f);
            Destroy(redPlatforms[1], 1f);
            Destroy(redPlatforms[2], 1f);
            Destroy(redPlatforms[3], 1f);
        }

        if (platform.tag == "OrangePlatforms")
        {
            GameObject[] redPlatforms = GameObject.FindGameObjectsWithTag("OrangePlatforms");

            Destroy(redPlatforms[0], 1f);
            Destroy(redPlatforms[1], 1f);
            Destroy(redPlatforms[2], 1f);
            Destroy(redPlatforms[3], 1f);
        }
    }
}
