using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
	public GameObject tallFull;
	public GameObject smallFull;
	public GameObject container;

	private GameObject spawn;
	int i=0;

	public float colThickness = 4f;
	public float zPosition = 0f;
	private Vector2 screenSize;

	void Start ()
	{
		//Create a Dictionary to contain all our Objects/Transforms
		System.Collections.Generic.Dictionary<string,Transform> colliders = new System.Collections.Generic.Dictionary<string,Transform>();
		//Create our GameObjects and add their Transform components to the Dictionary we created above
		colliders.Add("Top",new GameObject().transform);
		colliders.Add("Bottom",new GameObject().transform);
		colliders.Add("Right",new GameObject().transform);
		colliders.Add("Left",new GameObject().transform);
		//Generate world space point information for position and scale calculations
		Vector3 cameraPos = Camera.main.transform.position;
		screenSize.x = Vector2.Distance (Camera.main.ScreenToWorldPoint(new Vector2(0,0)),Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f; //Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half, since we only need half that value for distance away from the camera to the edge
		screenSize.y = Vector2.Distance (Camera.main.ScreenToWorldPoint(new Vector2(0,0)),Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
		//For each Transform/Object in our Dictionary
		foreach(KeyValuePair<string,Transform> valPair in colliders)
		{
			valPair.Value.gameObject.AddComponent<BoxCollider2D>(); //Add our colliders. Remove the "2D", if you would like 3D colliders.
			valPair.Value.name = valPair.Key + "Collider"; //Set the object's name to it's "Key" name, and take on "Collider".  i.e: TopCollider
			valPair.Value.parent = transform; //Make the object a child of whatever object this script is on (preferably the camera)

			if(valPair.Key == "Left" || valPair.Key == "Right") //Scale the object to the width and height of the screen, using the world-space values calculated earlier
				valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
			else
				valPair.Value.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
		}  
		//Change positions to align perfectly with outter-edge of screen, adding the world-space values of the screen we generated earlier, and adding/subtracting them with the current camera position, as well as add/subtracting half out objects size so it's not just half way off-screen
		colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zPosition);
		colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zPosition);
		colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f), zPosition);
		colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), zPosition);
	}

	void Update()
	{
		if(i<100){
			i++;
		}
		else{
			i=0;

			float r=Random.Range(0f,1f);
			if(r<0.5){
				spawn= Instantiate(tallFull, this.transform.position, Quaternion.identity);
				spawn.transform.parent = container.transform;
				spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-600.0f, 600.0f),Random.Range(-1000.0f, 0.0f))); 
			}
			else{
				spawn= Instantiate(smallFull, this.transform.position, Quaternion.identity);
				spawn.transform.parent = container.transform;
				spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-600.0f, 600.0f),Random.Range(-1000.0f, 0.0f))); 
			}
		}
	}
}
