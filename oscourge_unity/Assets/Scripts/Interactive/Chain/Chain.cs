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

	private Collider2D collider;

	int hashIsTriggered = Animator.StringToHash("isTriggered");

	void Start()
	{
		myAnim = GetComponent<Animator>();
		activators = new SortedSet<int>();
		collider = GetComponent<Collider2D>();
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
		this.collider.enabled = false;
	}
	
	public void enableCollider(){
		this.collider.enabled = true;
	}
}
