using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scripts.Networking;
using Newtonsoft.Json;
using System.Text;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpSpeed;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public UnityEvent OnLandEvent;
    public Animator animator;
    public GameObject SFX;
    public bool localMode;
    public GameObject death;
    private bool isGrounded;
    public Vector3 spawnLocation;
    private bool wasGrounded;
    private bool jump;
    private Rigidbody2D myRigidbody;

    public bool isTallSquash;
    private float horizontal;
    private bool jumpPressed;
    private bool isDead;

    public bool intro;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        spawnLocation = transform.position;
        isGrounded = true;
        isDead = false;
        intro = false;

    }

    void Update()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded && !wasGrounded)
        {
            OnLandEvent.Invoke();
        }

        if (!intro) { 

        if ((isTallSquash && ClientUDP.Instance.playerMode == 2) || (!isTallSquash && ClientUDP.Instance.playerMode == 1))
        {

            horizontal = Input.GetAxisRaw("Horizontal");
            jumpPressed = Input.GetButtonDown("Jump");

            InputValues inputs = new InputValues(horizontal, jumpPressed);
            Vector3 pos = transform.position;

            Message data = new Message(gameObject.name, "look at these moves", Message.DATA, inputs, pos);
            string dataString = JsonConvert.SerializeObject(data);
            if (ClientUDP.Instance.gameState != ClientUDP.OFFLINE)
                ClientUDP.Instance.SendData(Encoding.ASCII.GetBytes(dataString));
            if (!isDead)
            {
                Move(horizontal, jumpPressed);
            }
        }
        else if (ClientUDP.Instance.gameState == ClientUDP.OFFLINE)
        {
            horizontal = Input.GetAxisRaw("Horizontal2");
            jumpPressed = Input.GetButtonDown("Jump2");

            InputValues inputs = new InputValues(horizontal, jumpPressed);
            Vector3 pos = transform.position;
            if (!isDead)
            {
                Move(horizontal, jumpPressed);
            }
        }
        else
        {
            horizontal = ClientUDP.Instance.currentInputs.horizontal;
            jumpPressed = ClientUDP.Instance.currentInputs.jump;
            transform.position = ClientUDP.Instance.currentPos;

            Move(horizontal, jumpPressed);
        }
    }

    }

    private void Move(float horizontal, bool jumpPressed)
    {
        if (horizontal > 0f)
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(4f, 4f, 1f);
            animator.SetFloat("Speed", moveSpeed);
            SFX.gameObject.GetComponent<SFX>().RunSound();
        }
        else if (horizontal < 0f)
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(-4f, 4f, 1f);
            animator.SetFloat("Speed", moveSpeed);
            SFX.gameObject.GetComponent<SFX>().RunSound();
        }
        else
        {
            myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            animator.SetFloat("Speed", 0f);
            SFX.gameObject.GetComponent<SFX>().RunStop();
        }
        if (jumpPressed && isGrounded && !jump)
        {
            SFX.gameObject.GetComponent<SFX>().RunStop();
            if (gameObject.name == "Player1")
            {
                SFX.gameObject.GetComponent<SFX>().JumpSound2();
            }
            else
            {
                SFX.gameObject.GetComponent<SFX>().JumpSound1();
            }
            jump = true;
            animator.SetBool("isJumping", true);
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
        }
    }


    public void onLanding()
    {
        animator.SetBool("isJumping", false);
        jump = false;
    }


    public void Kill()
    {
        SFX.gameObject.GetComponent<SFX>().HurtSound();
        isDead = true;
        animator.SetBool("isDead", true);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        GameObject d = Instantiate(death, this.transform.position, Quaternion.identity);
        d.transform.parent = this.transform;

    }

    public void Respawn()
    {
        isDead = false;
        animator.SetBool("isDead", false);
        animator.SetBool("isJumping", false);
        transform.position = spawnLocation;
        jump = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        myRigidbody.velocity = new Vector3(0, 0, 0);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Checkpoint"))
        {
            spawnLocation = col.transform.position + new Vector3(0.2f, 0, 0);
        }
    }



}
