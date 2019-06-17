using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Movement : MonoBehaviour
{
	public GameObject[] gameObjects;
	public int distanceToCenter = 1;

	// Each seconds rotationSpeed/360 turns will be done
	public float rotationSpeed = 20;
	private float angleDelta = 0F;
	private float angle = 0F;

	public float getCurrentAngle(int index){
		return index * angleDelta + angle;
	}

	// Start is called before the first frame update
	void Start()
	{
		setAngleDelta();
	}

	// Update is called once per frame
	void Update()
	{		
		updatePosition();
		this.angle = this.angle + this.rotationSpeed * Time.deltaTime;
	}

	public void setAngleDelta(){
		this.angleDelta = 360/gameObjects.Length;
	}

	public void updatePosition(){
		int index = 0;
		foreach(GameObject obj in gameObjects){
			float currentAngle = (index * angleDelta + angle) % 360F;
			currentAngle *= Mathf.Deg2Rad;

			if(obj != null){
				float deltaX = Mathf.Cos(currentAngle) * distanceToCenter;
				float deltaY = Mathf.Sin(currentAngle) * distanceToCenter;
				Vector3 position = new Vector3(deltaX, deltaY, 0);
				obj.transform.position = position + this.transform.position;
			}
			index++;
		}
	}
}
