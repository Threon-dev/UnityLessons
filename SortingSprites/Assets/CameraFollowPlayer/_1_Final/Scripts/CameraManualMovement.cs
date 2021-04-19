using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeMonkey_CameraFollowPlayer_Final {

    public class CameraManualMovement : MonoBehaviour {

        // Update is called once per frame
        void Update() {
            Vector3 moveDir = new Vector3(0, 0);
            if (Input.GetKey(KeyCode.W)) {
                moveDir.y = +1;
            }
            if (Input.GetKey(KeyCode.S)) {
                moveDir.y = -1;
            }
            if (Input.GetKey(KeyCode.A)) {
                moveDir.x = -1;
            }
            if (Input.GetKey(KeyCode.D)) {
                moveDir.x = +1;
            }
            float manualCameraSpeed = 80f;
            transform.position += moveDir * manualCameraSpeed * Time.deltaTime;
        }
    }

}