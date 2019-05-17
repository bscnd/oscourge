using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	private bool active = false;

	public void activate(){
		this.active = true;
	}

	public void deactivate(){
		this.active = false;
	}

	public bool getState(){
		return this.active;
	}
}
