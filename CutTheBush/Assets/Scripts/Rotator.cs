using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float turnSpeed = 10f;

    public float timeBeforeRotate = 2f;
    
    private bool canRotate;

    GameManager gm;
    Animator animator;
    private void Start()
    {
        gm = GameManager.instance;
        animator = GetComponent<Animator>();
        StartCoroutine(WaitBeforeStart());
    }
    private void FixedUpdate()
    {      
        if (canRotate)
        {
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        }

        if (gm == null)
        {
            return;
        }

        if (gm.levelComplete)
        {
            canRotate = false;
            animator.SetBool("levelComplete", true);
            return;
        }    
    }

    private IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(timeBeforeRotate);
        canRotate = true;
    }
}
