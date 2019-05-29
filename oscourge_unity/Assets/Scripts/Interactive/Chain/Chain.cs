using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
	private Animator myAnim;

	// The number of pressurePlates/levers/... that has to be activated to trigger this gameobject
	public int activatorNumber = 1;

	// Set of currently activated activator's id
	private SortedSet<int> activators;

	// The visual indicator that shows witch lever/pressure plate... will trigger this chain
	private GameObject indicator;

	private Collider2D chainCollider;

	int hashIsTriggered = Animator.StringToHash("isTriggered");
	
	void Start()
	{
		this.myAnim = GetComponent<Animator>();
		this.activators = new SortedSet<int>();
		this.chainCollider = GetComponent<Collider2D>();
	}

	public void trigger(bool isActivate, int objectId){
		if(isActivate){
			activators.Add(objectId);
		}
		else{
			activators.Remove(objectId);
		}

		myAnim.SetBool(hashIsTriggered, activators.Count >= activatorNumber);
	}

	public void disableCollider(){
		this.chainCollider.enabled = false;
	}
	
	public void enableCollider(){
		this.chainCollider.enabled = true;
	}

	public GameObject initTriggerIndicator(){
		if(this.indicator == null){
			this.indicator = TriggerIndicator.spawn(this.transform.position, new Vector3(0, -0.25F, 0));
		}
		return this.indicator;
	}
}
