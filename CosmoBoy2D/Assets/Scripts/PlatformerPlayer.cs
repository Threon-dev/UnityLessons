using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField]
    string landingSoundName = "LandingFootsteps";
    private BoxCollider2D _box;
    public float JumpForce = 12.0f;
   
    private Rigidbody2D _body;
    private Animator anim;
    Transform playerGraphics;
    AudioManager audioManager;
    
   
   

    private void Awake()
    {
        playerGraphics = transform.Find("Graphics");
        if (playerGraphics == null)
        {
            Debug.LogError("Let's freak out! There is no 'Graphics' object as a child of a player!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in Weapon script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * PlayerStats.instance.speed *100* Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool grounded = false;
        if (hit != null)
        { 
            grounded = true;
        }
        _body.gravityScale = grounded && deltaX == 0 ? 0 : 1;
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

        }
       
       
        anim.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            playerGraphics.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

       
        if (Input.GetKeyDown(KeyCode.Space)&& grounded==true)
        {
            anim.SetBool("grounded", false);
        }
        else
        {
            anim.SetBool("grounded", true);
           
        }

      
    }
   
}
