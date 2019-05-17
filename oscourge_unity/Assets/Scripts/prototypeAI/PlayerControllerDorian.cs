using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDorian : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidbody;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded; 


public Vector3 respawn;

    // Start is called before the first frame update
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();

        respawn=this.transform.position;
    }

    // Update is called once per frame
    void Update() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetAxisRaw("Horizontal") > 0f) {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(2f, 2f, 1f);
        } else if (Input.GetAxisRaw("Horizontal") < 0f) {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(-2f, 2f, 1f);
        } else {
            myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
        }

        if (Input.GetButtonDown("Jump") ) {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
        }
    }

    public void kill(){
        transform.position=respawn;
    }



void OnTriggerEnter2D(Collider2D col)
{
    if(col.gameObject.name == "checkpoint")
    {
        Debug.Log("alooooo");
      respawn=col.transform.position+new Vector3(0.2f,0,0);
    }
}

   }
