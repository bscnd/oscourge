using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Trigger
{
	private Animator myAnim;
	private bool playerIsNear = false;
	
	int hashIsTriggered = Animator.StringToHash("isTriggered");
	

	// Start is called before the first frame update
	void Start()
	{
		myAnim = GetComponent<Animator>();
		initIndicators();
		updateInteractiveObjects();
	}

	// This method must be called in the game manager game over method !
	public void OnGameOver(){
		myAnim.SetBool(hashIsTriggered, false);
	}

	public void updateInteractiveObjects(){
		bool isLeverActivated = myAnim.GetBool(hashIsTriggered);

		foreach(Chain chain in chains){
			chain.trigger(isLeverActivated, this.GetInstanceID());
		}

		foreach(Chain chain in deactivateChains){
			chain.trigger(!isLeverActivated, this.GetInstanceID());
		}

		foreach(BlinkPlatform platform in activatePlatform){
			platform.trigger(isLeverActivated, this.GetInstanceID());
		}

		foreach(BlinkPlatform platform in deactivatePlatform){
			platform.trigger(!isLeverActivated, this.GetInstanceID());
		}
	}

	void Update(){
		if(playerIsNear && Input.GetButtonDown("ContextualAction")){
			myAnim.SetBool(hashIsTriggered, !myAnim.GetBool(hashIsTriggered));
		}
		updateInteractiveObjects();
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
