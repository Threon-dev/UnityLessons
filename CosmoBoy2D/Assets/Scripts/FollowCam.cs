using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float smoothTime = 0.2f;
    public float yPosRestriction = -1;
    public Transform target;
    private Vector3 _velocity = Vector3.zero;
    public float nextTimeToSearch = 0;
    public void LateUpdate()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }
            
        
            
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
       targetPosition = new Vector3(targetPosition.x, Mathf.Clamp(targetPosition.y,yPosRestriction,Mathf.Infinity), targetPosition.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);

    }
    public void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
            
            if (searchResult != null)
            {
                target = searchResult.transform;
                nextTimeToSearch = Time.time + 0.5f;
            }
            
        }
    }
    // Start is called before the first frame update
    
}
