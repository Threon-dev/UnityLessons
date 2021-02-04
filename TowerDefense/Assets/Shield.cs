using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    EnemyBoss boss;
    public GameObject shieldEffect;
    public Light lightEffect;

    AudioManager am;
    Animator anim;

    float timer = 10f;

    private void Start()
    {
        am = AudioManager.instance;
        anim = gameObject.GetComponent<Animator>();
        boss = GetComponent<EnemyBoss>();
        StartCoroutine(ShieldOff());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Shield hit"+other.name);

        if(other.tag == "Bullet")
        {
            StartCoroutine(ShieldBulletBlock(other));
        }
    }

    IEnumerator ShieldBulletBlock(Collider other)
    {
        GameObject effect = (GameObject)Instantiate(shieldEffect, other.transform.position, other.transform.rotation);
        lightEffect.enabled = true;
        am.PlaySound("ShieldBlock");
        Destroy(other.gameObject);
        yield return new WaitForSeconds(0.1f);
        lightEffect.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }
    
    IEnumerator ShieldOff()
    {
        yield return new WaitForSeconds(timer - 1f);
        anim.SetBool("ShieldOff", true);
    }
}