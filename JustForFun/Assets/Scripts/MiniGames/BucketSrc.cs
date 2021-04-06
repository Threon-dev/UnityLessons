using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketSrc : MonoBehaviour
{
    public DoubleGameSrc GameSrc;

    SocksSrc[] Socks = new SocksSrc[2];
    int ID = 0;    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Socks")) {
            Socks[ID] = collision.transform.parent.gameObject.GetComponent<SocksSrc>();
            if (!Socks[ID].onDrag)
            {
                Socks[ID].onDrag = true;
                ID++;
            }

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);

            if (ID > 1)
            {
                ID = 0;
                if (Socks[0].ID == Socks[1].ID)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    Destroy(Socks[0].gameObject);
                    Destroy(Socks[1].gameObject);
                    if (GameSrc.isDemo)
                    {
                        GameSrc.isDemo = false;
                    }
                    else
                    {
                        GameSrc.UpdateProg();
                    }
                }
                else
                {
                    transform.GetChild(1).gameObject.SetActive(true);

                    float _angle = Random.Range(0, 360) * Mathf.Deg2Rad;
                    
                    Socks[0].onDrag = false;
                    Socks[1].onDrag = false;

                    Socks[0].MainBone.velocity = new Vector3(Mathf.Sin(_angle), 2f, Mathf.Cos(_angle)) * Random.Range(100, 150);
                    Socks[1].MainBone.velocity = new Vector3(Mathf.Sin(_angle), 2f, Mathf.Cos(_angle)) * Random.Range(100, 150);
                }
            }
        }
    }
}
