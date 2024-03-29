﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

	public float backgroundSize;
	public float viewZone =10;
	public GameObject cam;
	public GameObject a1;
	public GameObject a2;
	public GameObject a3;
	public float Speed;


	private Transform cameraTransform;
	private Transform[] layers;
	private int leftIndex;
	private int rightIndex;
    Vector3 aa1, aa2, aa3;
    public Vector3 startPos;


	 void Start(){

		startPos = transform.position;

		cameraTransform=cam.transform;
		layers =new Transform[3];
		layers[0]=a1.transform;
		layers[1]=a2.transform;
		layers[2]=a3.transform;
		leftIndex=0;
		rightIndex=layers.Length-1;


		
		aa1=a1.transform.position;
		aa2=a2.transform.position;
		aa3=a3.transform.position;


		
	}

	private void FixedUpdate(){


		bool scroll2=cameraTransform.gameObject.GetComponent<CameraController>().scroll;
		if(scroll2){
			transform.position=transform.position-new Vector3(Speed/1000,0,0);
		}
		

		if(cameraTransform.position.x >  (layers[leftIndex].position.x+viewZone)){
			ScrollRight();
		}
	}

	private void ScrollRight(){
		int lastLeft=leftIndex;
		Vector3 truc=new Vector3(0,0,0);
		truc.x=layers[rightIndex].position.x+backgroundSize;
		truc.y=layers[rightIndex].position.y;
		layers[leftIndex].position=truc;
		rightIndex=leftIndex;
		leftIndex++;
		if(leftIndex==layers.Length){
			leftIndex=0;
		}
	}

	public void Reset(){

		a1.transform.position=aa1;
		a2.transform.position=aa2;
		a3.transform.position=aa3;
        transform.position = startPos;
	}

	

}
