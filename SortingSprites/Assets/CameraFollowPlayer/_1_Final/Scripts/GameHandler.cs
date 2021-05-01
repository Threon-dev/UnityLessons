using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

namespace CodeMonkey_CameraFollowPlayer_Final {

    public class GameHandler : MonoBehaviour {

        public Transform playerTransform;
        public Transform character1Transform;
        public Transform character2Transform;
        public Transform cameraManualMovementTransform;
        
        public CameraFollow cameraFollow;
        
        private float zoom = 60f;

        private void Start() {
            cameraFollow.Setup(() => playerTransform.position, () => zoom);
            

            CMDebug.ButtonUI(new Vector3(520, 330), "Player", () => {
                FunctionPeriodic.StopAllFunc("RandomCameraMovement");
                cameraFollow.SetGetCameraFollowPositionFunc(() => playerTransform.position);
                cameraFollow.SetGetCameraZoomFunc(() => zoom);
            });
            CMDebug.ButtonUI(new Vector3(520, 270), "Char 1", () => {
                FunctionPeriodic.StopAllFunc("RandomCameraMovement");
                cameraFollow.SetGetCameraFollowPositionFunc(() => character1Transform.position);
                cameraFollow.SetGetCameraZoomFunc(() => zoom);
            });
            CMDebug.ButtonUI(new Vector3(520, 210), "Char 2", () => {
                FunctionPeriodic.StopAllFunc("RandomCameraMovement");
                cameraFollow.SetGetCameraFollowPositionFunc(() => character2Transform.position);
                cameraFollow.SetGetCameraZoomFunc(() => zoom);
            });
            CMDebug.ButtonUI(new Vector3(520, 150), "Manual", () => {
                FunctionPeriodic.StopAllFunc("RandomCameraMovement");
                cameraFollow.SetGetCameraFollowPositionFunc(() => cameraManualMovementTransform.position);
                cameraFollow.SetGetCameraZoomFunc(() => zoom);
            });

            CMDebug.ButtonUI(new Vector2(520, 70), "Zoom In", ZoomIn);
            CMDebug.ButtonUI(new Vector2(520, 10), "Zoom Out", ZoomOut);

            CMDebug.ButtonUI(new Vector3(520, -70), "Random", () => {
                // Executes Action every 0.5s
                FunctionPeriodic.StopAllFunc("RandomCameraMovement");
                FunctionPeriodic.Create(() => { 
                    cameraFollow.SetCameraFollowPosition(new Vector3(Random.Range(-100, 100), Random.Range(-100, 100)));
                    cameraFollow.SetCameraZoom(Random.Range(40f, 150f));
                }, 0.5f, "RandomCameraMovement");
            });
        }

        private void Update() {
            HandleZoomButtons();
        }


        private void HandleZoomButtons() {
            if (Input.GetKeyDown(KeyCode.Z)) {
                ZoomOut();
            }
            if (Input.GetKeyDown(KeyCode.X)) {
                ZoomIn();
            }
        }

        private void ZoomIn() {
            zoom -= 40f;
            if (zoom < 40f) zoom = 40f;
        }
        private void ZoomOut() {
            zoom += 40f;
            if (zoom > 200f) zoom = 200f;
        }

    }

}