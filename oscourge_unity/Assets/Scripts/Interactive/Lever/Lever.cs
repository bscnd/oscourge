﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
	private Animator myAnim;
	public Chain[] chains;
	private bool playerIsNear = false;
	
	int hashIsTriggered = Animator.StringToHash("isTriggered");

	// Start is called before the first frame update
	void Start()
	{
		myAnim = GetComponent<Animator>();
	}

	public void updateInteractiveObjects(){
		foreach(Chain chain in chains){
			chain.trigger(myAnim.GetBool(hashIsTriggered), this.GetInstanceID());
		}
	}

	void Update(){
		if(playerIsNear && Input.GetButtonDown("ContextualAction")){
			myAnim.SetBool(hashIsTriggered, !myAnim.GetBool(hashIsTriggered));
			updateInteractiveObjects();
		}
	}

	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag == "Player"){
			playerIsNear = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			playerIsNear = true;
		}
	}
}