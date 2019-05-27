using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scripts.Networking;
using Newtonsoft.Json;
using System.Text;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public UnityEvent OnLandEvent;
	public Animator animator;
	private bool isGrounded; 
	private Vector3 spawnLocation;
	private bool wasGrounded;
	private bool jump;
	private Rigidbody2D myRigidbody;

	public bool myCharacter;
    private float horizontal;
    private bool jumpPressed;


	void Start() {
		myRigidbody = GetComponent<Rigidbody2D>();
		spawnLocation=transform.position;
		isGrounded=true;	
	}

	void Update() {
		Debug.Log(gameObject.name);
		wasGrounded=isGrounded;
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		if(isGrounded&& !wasGrounded  && myRigidbody.velocity.y<0){
			OnLandEvent.Invoke();
		}

		if(myCharacter){

			horizontal = Input.GetAxisRaw("Horizontal");
			jumpPressed = Input.GetButtonDown("Jump");


			InputValues inputs = new InputValues(horizontal, jumpPressed);
			Vector3 pos = transform.position;

			Message data = new Message(gameObject.name, "look at these moves", Message.DATA, inputs, pos);
			string dataString = JsonConvert.SerializeObject(data);
			ClientUDP.Instance.SendData(Encoding.ASCII.GetBytes(dataString));
		}
		else{
			horizontal = ClientUDP.Instance.currentInputs.horizontal;
			jumpPressed = ClientUDP.Instance.currentInputs.jump;
			transform.position = ClientUDP.Instance.currentPos;
		}
		Move(horizontal,jumpPressed);
	}



	private void Move(float horizontal,bool jumpPressed){
		if (horizontal > 0f) {
			myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
			transform.localScale = new Vector3(4f, 4f, 1f);
			animator.SetFloat("Speed",moveSpeed);
		} 
		else if (horizontal < 0f) {
			myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
			transform.localScale = new Vector3(-4f, 4f, 1f);
			animator.SetFloat("Speed",moveSpeed);
		} 
		else {
			myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
			animator.SetFloat("Speed",0f);
		}
		if (jumpPressed && isGrounded && !jump) {
			jump=true;
			animator.SetBool("isJumping",true);
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
		}
	}


	public void onLanding(){
		animator.SetBool("isJumping",false);
		jump=false;
	}

	public void Kill(){
		transform.position=spawnLocation;
		jump=false;
	}



	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Checkpoint"))
		{
			spawnLocation=col.transform.position+new Vector3(0.2f,0,0);
		}
	}



}
