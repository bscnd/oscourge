using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class littleBoy : MonoBehaviour
{

	public GameObject player;
	public float moveSpeed;
	public Animator animator;
	private Rigidbody2D myRigidbody;
	public GameObject gameManager;

	public float jumpSpeed;
	public float detectRadius;
	public float patrolRadius;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public UnityEvent OnLandEvent;
	private bool seen;
	private int z;

	private Vector3 spawn;

	void Start() {
		myRigidbody = GetComponent<Rigidbody2D>();
		seen=false;
		z=0;
		spawn=transform.position;
	}

	void Update()
	{

		if(!isDead){

			float i=player.transform.position.x-transform.position.x;
			if((i>0f&&i<detectRadius)||(i<0f && i>-detectRadius) ){
				seen=true;
			}

			if(seen){
				if(player.transform.position.x>transform.position.x){
					myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
					transform.localScale = new Vector3(2f, 2f, 1f);
					animator.SetFloat("Speed",moveSpeed);
				}
				else if(player.transform.position.x<transform.position.x){

					myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
					transform.localScale = new Vector3(-2f, 2f, 1f);
					animator.SetFloat("Speed",moveSpeed);
				}

				if(player.transform.position.y>transform.position.y+3){
					myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
					animator.SetBool("isJumping",true);
				}
			} else{
				if(z<patrolRadius*100){
					myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
					transform.localScale = new Vector3(2f, 2f, 1f);
					animator.SetFloat("Speed",moveSpeed);
					z++;
				}
				else if (z<2*patrolRadius*100){

					myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
					transform.localScale = new Vector3(-2f, 2f, 1f);
					animator.SetFloat("Speed",moveSpeed);
					z++;
				}
				else if (z==2*patrolRadius*100){
					z=0;
				}


			}
		}


	}

	public void onLanding(){
		animator.SetBool("isJumping",false);
	}


	private bool attack=false;

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			if(!attack){
				attack=true;
				StartCoroutine(Attack());
			}
		}
	}


	IEnumerator Attack() {
		animator.SetBool("isAttacking",true);
		yield return new WaitForSeconds(0.5f);
		animator.SetBool("isAttacking",false);
		attack=false;
		Kill();
		gameManager.GetComponent<GameManager>().GameOver();
		yield return new WaitForSeconds(4);
		Respawn();
	}

	private bool isDead=false;

	public void Kill(){
		isDead=true;
		myRigidbody.isKinematic = true;
		myRigidbody.velocity = new Vector3(0f, 0f, 0f);
		animator.SetBool("isDead",true);

	}


	public void Respawn(){
		isDead=false;
		seen=false;
		myRigidbody.isKinematic = false;
		transform.position=spawn;
		animator.SetBool("isDead",false);


	}
}
