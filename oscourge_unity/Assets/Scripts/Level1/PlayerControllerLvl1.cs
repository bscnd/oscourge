using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLvl1 : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody2D myRigidbody;

	public float jumpSpeed;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded; 

	// Start is called before the first frame update
	void Start() {
		myRigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update() {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if (Input.GetAxisRaw("Horizontal") > 0f) {
			myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
			transform.localScale = new Vector3(4f, 4f, 1f);
		} else if (Input.GetAxisRaw("Horizontal") < 0f) {
			myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
			transform.localScale = new Vector3(-4f, 4f, 1f);
		} else {
			myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
		}

		if (Input.GetButtonDown("Jump") && isGrounded) {
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
		}
	}
}
