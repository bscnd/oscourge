using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
	public Boss1Control[] boss;
	public Chain chain;

	// The number of pressure plate activation by the two players needed to trigger the chain
	private int activationNeeded = 3;
	private int activationNumber = 0;

	void Start()
	{
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
