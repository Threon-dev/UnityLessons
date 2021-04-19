using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V_AnimationSystem;

namespace CodeMonkey_CameraFollowPlayer_Final {

    /*
     * Character moves between waypoints
     * */
    public class CharacterWaypoints : MonoBehaviour {
        
        private const float speed = 30f;

        [SerializeField]
        private List<Vector3> waypointList;
        private int waypointIndex;

        [SerializeField] private string idleAnimation;
        [SerializeField] private string walkAnimation;

        [SerializeField] private float idleFrameRate;
        [SerializeField] private float walkFrameRate;

        private V_UnitSkeleton unitSkeleton;
        private V_UnitAnimation unitAnimation;
        private AnimatedWalker animatedWalker;


        private void Start() {
            Transform bodyTransform = transform.Find("Body");
            unitSkeleton = new V_UnitSkeleton(1f, bodyTransform.TransformPoint, (Mesh mesh) => bodyTransform.GetComponent<MeshFilter>().mesh = mesh);
            unitAnimation = new V_UnitAnimation(unitSkeleton);
            animatedWalker = new AnimatedWalker(unitAnimation, UnitAnimType.GetUnitAnimType(idleAnimation), UnitAnimType.GetUnitAnimType(walkAnimation), idleFrameRate, walkFrameRate);
        }
        private void Update() {
            HandleMovement();
            unitSkeleton.Update(Time.deltaTime);
        }
        private void HandleMovement() {
            Vector3 waypoint = waypointList[waypointIndex];

            Vector3 waypointDir = (waypoint - transform.position).normalized;

            float distanceBefore = Vector3.Distance(transform.position, waypoint);
            animatedWalker.SetMoveVector(waypointDir);
            transform.position = transform.position + waypointDir * speed * Time.deltaTime;
            float distanceAfter = Vector3.Distance(transform.position, waypoint);

            if (distanceBefore <= distanceAfter) {
                // Go to next waypoint
                waypointIndex = (waypointIndex + 1) % waypointList.Count;
            }
        }
    }

}