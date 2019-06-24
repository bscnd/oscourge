using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Trigger
{
	private Animator myAnim;

	// Up player is 0 in the array
	// Down player is 1 in the array
	private bool[] playerIsNear;
	
	int hashIsTriggered = Animator.StringToHash("isTriggered");

	public Lever(){
		this.playerIsNear = new bool[2];
		this.playerIsNear[0] = false;
		this.playerIsNear[1] = false;
	}

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
			chain.detrigger(isLeverActivated, this.GetInstanceID());
		}

		foreach(BlinkPlatform platform in activatePlatform){
			platform.trigger(isLeverActivated, this.GetInstanceID());
		}

		foreach(BlinkPlatform platform in deactivatePlatform){
			platform.trigger(!isLeverActivated, this.GetInstanceID());
		}

		foreach (Spikes spikes in activateSpikes) {
			spikes.trigger(!isLeverActivated, this.GetInstanceID());
		}

		foreach (Spikes spikes in deactivateSpikes) {
			spikes.trigger(isLeverActivated, this.GetInstanceID());
		}
	}

	void Update(){
		//if(playerIsNear[0] && Input.GetButtonDown("ContextualAction")){
		if(playerIsNear[0] && InputManager.Instance().GetButtonDown(ButtonName.Action)){
			myAnim.SetBool(hashIsTriggered, !myAnim.GetBool(hashIsTriggered));
		}

		//if(playerIsNear[1] && Input.GetButtonDown("ContextualAction2")){
		if(playerIsNear[1] && InputManager.Instance().GetButtonDown(ButtonName.Action2)){

			myAnim.SetBool(hashIsTriggered, !myAnim.GetBool(hashIsTriggered));
		}
		updateInteractiveObjects();
	}

	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag == "Player"){
			PlayerController script = collision.gameObject.GetComponent<PlayerController>();

			int playerIndex = script.isTallSquash ? 1 : 0;
			this.playerIsNear[playerIndex] = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			PlayerController script = collision.gameObject.GetComponent<PlayerController>();

			int playerIndex = script.isTallSquash ? 1 : 0;
			this.playerIsNear[playerIndex] = true;
		}
	}
}
