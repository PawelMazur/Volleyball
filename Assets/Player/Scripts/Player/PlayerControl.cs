using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float maxSpeed = 20f;
    public float jumpForce = 200f;
    public float jumpDoubleForce = 100f;

    public float nextJump = 0.0f;
    public float timeRespJump = 0.4f;

    public float nextDoubleJump = 0.0f;
    public float timeRespDoubleJump = 1.5f;

    public LayerMask whatIsGround;
    public LayerMask whatIsBorder;

    public Transform groundCheck;
    public bool ground = false;

    public Transform borderLeftCheck;
    public bool borderLeft = false;

    public Transform borderRightCheck;
    public bool borderRight = false;

    Rigidbody2D rigidbody2D;
    public bool doublejumpIsActive = false;

    // Use this for initialization

    public AudioClip audioClipJump;
    private AudioSource audioSource;

    private Animator animator;

    public float horizontal;

    bool turn = false;

    bool facingRight = true;

    void Start()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space) && ground)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
         */
        //Debug.Log("Horizontal : " + horizontal);
        

        #if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (Time.time > nextJump)
            {
                Jump();
            }
            if (!ground && (borderLeft || borderRight))
            {
                if (Time.time > nextDoubleJump)
                {
                    DoubleJump();
                }

            }
        }

        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if (JumpButton.isJump)
        {
            animator.SetBool("Jump", true);
            if (Time.time > nextJump)
            {
                Jump();
                //Debug.Log("JumpButton ");
            }
            if (!ground && (borderLeft || borderRight))
            {
                if (Time.time > nextDoubleJump)
                {
                    
                    DoubleJump();
                    //Debug.Log("JumpDouble ");
                }

            }
        }
        //Debug.Log("TimeJump : " + nextJump);
        //Debug.Log("TimeDoubleJump : " + nextDoubleJump);
        
        #endif
        
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (Time.time > nextJump)
            {
                Jump();
            }
            if (!ground && (borderLeft || borderRight))
            {
                if (Time.time > nextDoubleJump)
                {
                    DoubleJump();
                }

            }
        }

        if (JumpButton.isJump)
        {

            if (Time.time > nextJump)
            {
                Jump();
                Debug.Log("JumpButton ");
            }
            if (!ground && (borderLeft || borderRight))
            {
                if (Time.time > nextDoubleJump)
                {
                    DoubleJump();
                    Debug.Log("JumpDouble ");
                }

            }
        }
        */
        
        

    }

    void FixedUpdate()
    {

        float horizontal;
        #if UNITY_STANDALONE || UNITY_WEBPLAYER

            horizontal = Input.GetAxis("Horizontal");
            animator.SetFloat("Speed", horizontal);
        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
            
            horizontal = VirtualJoystick.Horizontal();
            animator.SetFloat("Speed", horizontal);
        #endif

            if (horizontal > 0)
            {
                turnRight();
            } if (horizontal < 0)
            {
                turnLeft();
            }

        rigidbody2D.velocity = new Vector2(horizontal * maxSpeed, rigidbody2D.velocity.y);

        //if (ground || borderLeft || borderRight)
        //    return;

        ground = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

        borderLeft = Physics2D.OverlapCircle(borderLeftCheck.position, 0.15f, whatIsBorder);
        borderRight = Physics2D.OverlapCircle(borderRightCheck.position, 0.15f, whatIsBorder);

        animator.SetBool("Ground", ground);
        animator.SetFloat("Jump", rigidbody2D.velocity.y);

        //Debug.Log("BorderLeft : " + borderLeft);

        if (ground)
        {
            doublejumpIsActive = false;
        }

        //Debug.Log("transform.localScale : " + transform.localScale);
    }

    void turnRight()
    {
        //flip(); 
        rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void turnLeft()
    {
        //flip();
        rigidbody2D.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    /*
    //
    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        //theScale *= -1;
        float x = transform.localScale.x * -1;
        theScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        transform.localScale = theScale;
    }*/

    public void AudioJump()
    {
        audioSource.clip = audioClipJump;
        audioSource.Play();
    }
    
    public void Jump()
    {
        
        if (ground)
        {
            nextJump = Time.time + timeRespJump;
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
            AudioJump();
        }
    }

    public void DoubleJump()
    {
        if (borderLeft || borderRight)
        {
            nextDoubleJump = Time.time + timeRespDoubleJump;
            Debug.Log("DoubleJump");
            rigidbody2D.AddForce(new Vector2(0, jumpForce * 0.5f));
            AudioJump();
        }
    }
    
}
