using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    private Animator m_animator;
    private bool m_grounded = false;
    private Sensor_Bandit m_groundSensor;
    private SpriteRenderer mySpriteRenderer;

    private Rigidbody2D myBody;

    [SerializeField] float speed = 2f;
    [SerializeField] float jumpForce = 5f;

    private bool moveLeft; // Determine if we move left or right
    private bool dontMove; // Determine if we are moving or not
    private bool canJump; // Test the j8mp

    // Start is called before the first frame update
    void Start()
    {
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        m_animator = GetComponent<Animator>();
       
        myBody = GetComponent<Rigidbody2D>();

        dontMove = true;
        m_animator.SetInteger("AnimState", 0);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
        HandleMoving();
        GroundCheck();
    }
    void HandleMoving() // Movement Handler
    {
        if (dontMove)
        {
            StopMoving();
        } else
        {
            if (moveLeft)
            {
                MoveLeft();
            } else if (!moveLeft)
            {
                MoveRight();
            }
        }
    } 

    // I uesed event trigger components pointer up and pointer down events. If pointer down for right button is not checked it is false. Which means moveLEft will be false and MoveRight method will execute.
    void GroundCheck()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }
    }
    public void AllowMovement (bool movement)
    {
        dontMove = false;
        moveLeft = movement;
    }

    public void DontAllowMovement()
    {
        dontMove = true;
    }

    public void Jump ()
    {
        if(canJump)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
            m_groundSensor.Disable(0.2f);

        }

    }

    public void MoveLeft()
    {
        myBody.velocity = new Vector2( -speed , myBody.velocity.y);
        m_animator.SetInteger("AnimState", 2);
        mySpriteRenderer.flipX = false;
    }
    public void MoveRight()
    {
        myBody.velocity = new Vector2(speed , myBody.velocity.y);
        m_animator.SetInteger("AnimState", 2);
        mySpriteRenderer.flipX = true;
    }
    public void StopMoving()
    {
        myBody.velocity = new Vector2(0f, myBody.velocity.y);
        m_animator.SetInteger("AnimState", 1);

    }


    void DetectInput()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x > 0)
        {
          //  m_animator.SetFloat("AirSpeed", myBody.velocity.y);
            MoveRight();
           
        }
        else if (x < 0)
        {
            MoveLeft();
        }
        else
        {
            StopMoving();
        }
    }
        void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }
}
