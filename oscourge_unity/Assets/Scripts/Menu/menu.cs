using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
	public GameObject tallFull;
	public GameObject smallFull;

	private GameObject spawn;
	int i=0;

    // Update is called once per frame

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
				spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-600.0f, 600.0f),Random.Range(-1000.0f, 0.0f))); 
			}
			else{
				spawn= Instantiate(smallFull, this.transform.position, Quaternion.identity);
				spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-600.0f, 600.0f),Random.Range(-1000.0f, 0.0f))); 
			}
		}
	}
}
