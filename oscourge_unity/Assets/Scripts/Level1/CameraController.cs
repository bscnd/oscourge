using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


	private Vector3 spawnLocation;

public float speed;
	void Start(){

		spawnLocation=transform.position;
	}

	void Update()
	{

		transform.position=transform.position+new Vector3(speed/100,0,0);


	}

	public void Respawn(){

			transform.position=spawnLocation;
	}



	
}
