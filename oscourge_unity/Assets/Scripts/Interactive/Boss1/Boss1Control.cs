using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Control : MonoBehaviour
{
	private Boss1Movement movementScript;
	private List<GameObject> platforms;
	private int activePlatformIndex;
	private PlatformPlate activePlatformScript;
	private System.Random rand;
	private float cumulatedTime;
	public float switchTime = 15;

	void Start(){
		movementScript = this.GetComponent<Boss1Movement>();

		if(movementScript == null){
			Debug.Log("No movement script attached to this Boss !");
			this.enabled = false;
		}

		platforms = new List<GameObject>();

		foreach(GameObject go in movementScript.gameObjects){
			if(go != null){
				platforms.Add(go);
			}
		}

		if(platforms.Count <= 1){
			Debug.Log("The boss must have at least two platforms !");
			this.enabled = false;
		}

		rand = new System.Random();
		cumulatedTime = switchTime;
	}

	public bool isActivated(){
		if(activePlatformScript != null){
			return activePlatformScript.isActivated();
		}
		else{
			return false;
		}
	}

	public void switchPlatform(){
		if(activePlatformScript != null){
			activePlatformScript.despawnPlate();
		}
		
		// Take a new index not equal to the previous one randomly
		int newIndex = rand.Next(0, platforms.Count - 1);
		activePlatformIndex = (newIndex >= activePlatformIndex) ? newIndex+1 : newIndex;
		if(platforms[activePlatformIndex] == null){
			Debug.Log("Wrong index !");
			this.enabled = false;
		}

		activePlatformScript = platforms[activePlatformIndex].GetComponent<PlatformPlate>();

		if(activePlatformScript == null){
			Debug.Log("No PlatformPlate script is present !");
			this.enabled = false;
		}
		
		activePlatformScript.spawnPlate();
	}

	void Update(){
		this.cumulatedTime += Time.deltaTime;

		if(this.cumulatedTime >= this.switchTime){
			cumulatedTime = 0;
			this.switchPlatform();
		}
	}
}
