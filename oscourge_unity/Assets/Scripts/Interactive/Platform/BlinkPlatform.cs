using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkPlatform : Mechanism
{
	private SortedSet<int> activators;
	private SortedSet<int> deactivators;
	
	public int activatorNumber = 1;

	public BlinkPlatform(){
		this.activators = new SortedSet<int>();
		this.deactivators = new SortedSet<int>();
	}

	// TODO Unify all initTriggerIndicator in order not to copy past this method
	public override GameObject initTriggerIndicator(){
		if(this.indicator == null){
			this.indicator = TriggerIndicator.spawn(this.transform.position, new Vector3(0, -0.25F, 0));
		}
		return this.indicator;
	}

	public void trigger(bool isActivate, int objectId){
		if(isActivate){
			activators.Add(objectId);
			deactivators.Remove(objectId);
		}
		else{
			activators.Remove(objectId);
			deactivators.Add(objectId);
		}

		if(deactivators.Count == 0 && activators.Count >= 1){
			this.gameObject.SetActive(true);
		}
		else{
			this.gameObject.SetActive(false);
		}
	}
}
