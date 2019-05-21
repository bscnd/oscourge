using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody2D myRigidbody;

	public float jumpSpeed;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded; 

	private Vector3 spawnLocation;

	void Start() {
		myRigidbody = GetComponent<Rigidbody2D>();
		spawnLocation=transform.position;
	}

	void Update() {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if (Input.GetAxisRaw("Horizontal2") > 0f) {
			myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
			transform.localScale = new Vector3(4f, 4f, 1f);
			} else if (Input.GetAxisRaw("Horizontal2") < 0f) {
				myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
				transform.localScale = new Vector3(-4f, 4f, 1f);
			} else {
				myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
			}

			if (Input.GetButtonDown("Jump2") && isGrounded) {
				myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
			}
		}


		public void Kill(){
			transform.position=spawnLocation;
		}



		void OnTriggerEnter2D(Collider2D col)
		{
			if(col.gameObject.name == "checkpoint")
			{
				spawnLocation=col.transform.position+new Vector3(0.2f,0,0);
			}
		}



}
