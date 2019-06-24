using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

public float speed;
public bool scroll;

	public Vector3 spawnLocation;

	void Start(){
		scroll=false;
		spawnLocation=transform.position;
	}

	void Update()
	{
		if(scroll){
			transform.position=transform.position+new Vector3(speed/100,0,0);
		}
	}


	public void Stop(){
        scroll=false;

	}

	public void Respawn(){
		transform.position=spawnLocation;
        scroll = false;
	}
}
