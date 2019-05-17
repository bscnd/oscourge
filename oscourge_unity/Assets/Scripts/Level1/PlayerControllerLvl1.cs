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

	private void updateInteractiveObjects(){
		GameObject[] interactiveObjects = GameObject.FindGameObjectsWithTag("Interactive");
		Lever nearestLever = null;
		float distanceLever = float.PositiveInfinity;
		float maximumDistance = 1.2F;

		foreach(GameObject obj in interactiveObjects){
			float temp = Vector2.Distance(this.transform.position, obj.transform.position);
			if(Input.GetButtonDown("ContextualAction")){
				Lever aLever = obj.GetComponent(typeof(Lever)) as Lever;
				if(aLever != null){
					if(temp < distanceLever && temp < maximumDistance){
						distanceLever = temp;
						nearestLever = aLever;
					}
				}

			}
		}

		if(nearestLever != null){
			nearestLever.trigger();
		}
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

		if (Input.GetButtonDown("Jump") && isGrounded) {
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
		}

		updateInteractiveObjects();
	}
}
