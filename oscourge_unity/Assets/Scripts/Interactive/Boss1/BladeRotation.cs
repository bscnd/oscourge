using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeRotation : MonoBehaviour
{
	// Each seconds rotationSpeed/360 turns will be done
	public float rotationSpeed = 20;

	void Update(){
		float amountToTurnRotate = this.rotationSpeed * Time.deltaTime;
		this.transform.Rotate(0, 0, amountToTurnRotate, Space.Self);

	}
}
