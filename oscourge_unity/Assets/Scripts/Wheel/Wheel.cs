using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
	public float moveSpeed = 1;
	public Transform endPoint;

	private Vector3 initPosition;
	private Vector3 endPosition;
	private Vector3 targetPosition;

	// Start is called before the first frame update
	void Start()
	{
		initPosition = this.transform.position;
		endPosition = endPoint.transform.position;
		targetPosition = endPosition;
	}

	// Update is called once per frame
	void Update()
	{
		if(this.transform.position.x == initPosition.x){
			targetPosition = endPosition;
		}
		else if(this.transform.position.x == endPosition.x){
			targetPosition = initPosition;
		}
	}

	void FixedUpdate(){
		// TODO Vector3 -> Vector2
		this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
	}
}
