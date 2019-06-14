using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
	// TODO Move update boss control here

	private Boss1Control[] boss;
	public Chain chain;

	// The number of pressure plate activation by the two players needed to trigger the chain
	private int activationNeeded = 3;
	private int activationNumber = 0;

	void Start()
	{
		boss = new Boss1Control[2];

		if(this.gameObject.transform.childCount == 2){
			GameObject control0 = this.transform.GetChild(0).gameObject;
			GameObject control1 = this.transform.GetChild(1).gameObject;
			boss[0] = control0.GetComponent<Boss1Control>();
			boss[1] = control1.GetComponent<Boss1Control>();

		}
		else{
			Debug.Log("This object must have 2 childs with Boss1Control scripts attached to it !");
			this.enabled = false;
		}

		if(boss[0] == null || boss[1] == null){
			Debug.Log("One or both Boss1Control scripts is null");
			this.enabled = false;
		}

		if(chain == null){
			Debug.Log("No chain is attached !");
			this.enabled = false;
		}
	}

	void Update()
	{
		if(boss[0].isActivated() && boss[1].isActivated()){
			activationNumber++;
			boss[0].switchPlatform();
			boss[1].switchPlatform();
		}
		
		if(activationNumber >= activationNeeded){
			chain.trigger(true, this.GetInstanceID());
		}
	}
}
