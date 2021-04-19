using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;


public class GameHandler : MonoBehaviour {

    public CameraFollow cameraFollow;

    public Transform playerTransform;


    private void Start() {

        cameraFollow.Setup(() => playerTransform.position);
    }

}


