using UnityEngine;
using System.Collections;

public class Leaves : MonoBehaviour
{
    AudioManager am;
    public string leavesSound = "LeavesSound";
    public string coinSound = "CoinSound";

    public GameObject leavesParticles;

    public GameObject scoreIncrease;
    public GameObject scoreIncreasePos;
    
    public int score = 10;

    public float destroyRate = 0.5f;

    public bool isSmall;

    [Header("For decreesing size of leaves")]
    public bool canBeSmaller;
    public GameObject smallerForm;
    public GameObject parentObject;

    private bool canBeDestroyed = false;

    private void Start()
    {
        am = AudioManager.instance;
    }
    void OnTriggerEnter(Collider knife)
    {
        if(knife.tag == "Knife")
        {
            if (isSmall)
            {
                StartCoroutine(WaitALittle());
            }
            
            if (!isSmall || canBeDestroyed)
            {
                LeavesDestroy();
         
                if (canBeSmaller)
                {
                    GameObject smallerLeaves = Instantiate(smallerForm, transform.position, transform.rotation);
                    smallerLeaves.transform.parent = parentObject.transform;
                }
            }
        }
    }
    public void LeavesDestroy()
    {
        am.PlaySound(leavesSound);
        GameObject leaves = Instantiate(leavesParticles, transform.position, transform.rotation);
        ScoreLabel._scorePoints += score;
        GameObject increaseScoreText = Instantiate(scoreIncrease, scoreIncreasePos.transform.position, scoreIncreasePos.transform.rotation);
        am.PlaySound(coinSound);
        Destroy(increaseScoreText, 0.5f);
        Destroy(this.gameObject);
        Destroy(leaves, 2f);      
    }

    private IEnumerator WaitALittle()
    {
        yield return new WaitForSeconds(destroyRate);
        canBeDestroyed = true;
    }
}
