using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformPlate : MonoBehaviour
{
	private GameObject pressurePlate;
	private Pressure_plate script;

	void Start(){
		if(this.gameObject.transform.childCount == 1){
			Transform pressurePlateTransform = this.transform.GetChild(0);

			this.pressurePlate = pressurePlateTransform.gameObject;
			script = this.pressurePlate.GetComponent<Pressure_plate>();
		}
		
		if(this.pressurePlate == null){
			Debug.Log("The platform must have one and only one pressure plate as a child !");
			this.enabled = false;
		}
		if(script == null){
			Debug.Log("No Pressure_plate.cs script is attached to the pressure plate !");
			this.enabled = false;
		}

		this.despawnPlate();
	}

	public void spawnPlate(){
		this.pressurePlate.SetActive(true);
	}

	public void despawnPlate(){
		this.pressurePlate.SetActive(false);
	}

	public bool isActivated(){
		return script.isActivated();
	}
}
