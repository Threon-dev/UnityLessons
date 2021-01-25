using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float JumpForce = 12.0f;
    public float checkRadius = 0.1f;

    private BoxCollider2D _box;
    private Rigidbody2D _body;
    private Animator anim;

    public LayerMask Ground;
    public bool onGround;
    public Transform GroundCheck;
    Transform playerGraphics;

    AudioManager audioManager;
    public string landingSound = "LandingFootsteps";
    private void Awake()
    {
        playerGraphics = transform.Find("Graphics");

        if (playerGraphics == null)
        {
            Debug.LogError("Let's freak out! There is no 'Graphics' object as a child of a player!");
        }
    }
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();

        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in PlatformerPlayer script");
        }
    }
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
        CheckingGround();
        bool wasGrouned = false;
        
        if (onGround==true && Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }  
        
        anim.SetFloat("speed", Mathf.Abs(deltaX));
        
        if (!Mathf.Approximately(deltaX, 0))
        {
            playerGraphics.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        if (onGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isGrounded", false);
            wasGrouned = false;
        }
        else
        {
            if (onGround == true)
            {
                anim.SetBool("isGrounded", true);
                wasGrouned = true;
            }              
        }

        if (onGround == true)
        {
            if (onGround != wasGrouned && onGround!=true)
            {
                audioManager.PlaySound(landingSound);
                Debug.Log("Grounded");
                wasGrouned = false;
            }
      
        }
    }
   void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }
}
