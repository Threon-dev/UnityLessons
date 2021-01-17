using System.Collections;
using UnityEngine;
using Pathfinding;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    //What to chase?
    public Transform target;
    //How many time each second we will update our path
    public float updateRate = 2f;


    private Seeker seeker;
    private Rigidbody2D rb;
    //The waypoint we currently moving towards
    private int currentWaypoint=0;
    

    //The calculated path
    public Path path;
    //The AI's speed per second
    public float speed = 300f;
    public ForceMode2D fMode;
    //The max distance from AI to waypoint for it to continue to the next waypoint
    public float nextWayPointDistance = 3;
    public float timeForSearchPlayer = 0.5f;

    [HideInInspector]
    public bool pathIsEnded = false;
    private bool searchForPlayer = false;
    
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
             if (!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }
        //Start a new path to the target position,return the result to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }
    IEnumerator SearchForPlayer()
    {
        GameObject sResult= GameObject.FindGameObjectWithTag("Player");
        if (sResult == null)
        {
            yield return new WaitForSeconds(timeForSearchPlayer);
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            target = sResult.transform;
            searchForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }
    IEnumerator UpdatePath()
    {
        if (target == null)
        { 
            if (!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1 / updateRate);
        StartCoroutine(UpdatePath());
    }
    public void OnPathComplete(Path p)
    {
        //Debug.Log("We got a path. Did it have an error?" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            if (!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        } 
        
        if (path == null)      
          return;
        
        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;
            //Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //Direction to the next point
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        
        //Move the AI
        rb.AddForce(dir, fMode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWayPointDistance)
        {
            currentWaypoint++;
            return;
        }
    } 
}
