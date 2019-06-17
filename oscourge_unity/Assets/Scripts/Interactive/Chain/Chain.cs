using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : Mechanism
{
	private Animator myAnim;

	// The number of pressurePlates/levers/... that has to be activated to trigger this gameobject
	public int activatorNumber = 1;

	// Set of currently activated activator's id
	private SortedSet<int> activators;
	private SortedSet<int> deactivators;

	private Collider2D chainCollider;

	int hashIsTriggered = Animator.StringToHash("isTriggered");


	public Chain(){
		this.activators = new SortedSet<int>();
		this.deactivators = new SortedSet<int>();
	}
	
	void Start()
	{
		this.myAnim = GetComponent<Animator>();
		this.chainCollider = GetComponent<Collider2D>();
		updateChain();
	}

	public void updateChain(){
		int currentNumberActivator = activators.Count - deactivators.Count;
		myAnim.SetBool(hashIsTriggered, currentNumberActivator >= activatorNumber);
	}

	public void trigger(bool isActivate, int objectId){
		if(isActivate){
			activators.Add(objectId);
			deactivators.Remove(objectId);
		}
		else{
			deactivators.Add(objectId);
			activators.Remove(objectId);
		}

		if(myAnim != null){
			updateChain();
		}
	}

	public void disableCollider(){
		this.chainCollider.enabled = false;
	}
	
	public void enableCollider(){
		this.chainCollider.enabled = true;
	}

	public override GameObject initTriggerIndicator(){
		if(this.indicator == null){
			this.indicator = TriggerIndicator.spawn(this.transform.position, new Vector3(0, -0.25F, 0));
		}
		return this.indicator;
	}
}
