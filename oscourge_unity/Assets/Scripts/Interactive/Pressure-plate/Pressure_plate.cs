using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_plate : Trigger
{
	private Animator myAnim;

	// Count how many players are on the pressure plate
	private int playerCounter;

	int hashIsTriggered = Animator.StringToHash("isTriggered");

	void Start()
	{
		myAnim = GetComponent<Animator>();
		playerCounter = 0;
		initIndicators();
	}

	// The pressure plate will remain activated if at least one player is on it
	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			myAnim.SetBool(hashIsTriggered, true);
			playerCounter++;
		}
		updateInteractiveObjects();
	}

	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag == "Player"){
			playerCounter--;
		}
		myAnim.SetBool(hashIsTriggered, playerCounter > 0);
		updateInteractiveObjects();
	}

	public void updateInteractiveObjects(){
		foreach(Chain chain in chains){
			chain.trigger(playerCounter > 0, this.GetInstanceID());
		}
	}
}
