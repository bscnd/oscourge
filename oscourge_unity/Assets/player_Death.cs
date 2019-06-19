using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Death : MonoBehaviour
{


	private GameObject spawn;

	public GameObject part1,part2,part3,part4,part5;


	void Start()
	{

		spawn=Instantiate(part1, this.transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), Quaternion.identity);
		spawn.transform.parent =  this.gameObject.transform;
		spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-5f, 5f),Random.Range(-5f, 5f)),ForceMode2D.Impulse);

		spawn=  Instantiate(part2, this.transform.position+ new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), Quaternion.identity);
		spawn.transform.parent =  this.gameObject.transform;
		spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-5f, 5f),Random.Range(-5f, 5f)),ForceMode2D.Impulse);


		spawn=  Instantiate(part3,this.transform.position+ new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), Quaternion.identity);
		spawn.transform.parent =  this.gameObject.transform;
		spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-5f, 5f),Random.Range(-5f, 5f)),ForceMode2D.Impulse);


		spawn=  Instantiate(part4, this.transform.position+ new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), Quaternion.identity);
		spawn.transform.parent =  this.gameObject.transform;
		spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-5f, 5f),Random.Range(-5f, 5f)),ForceMode2D.Impulse);


		spawn=  Instantiate(part5,this.transform.position+ new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), Quaternion.identity);
		spawn.transform.parent =  this.gameObject.transform;
		spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-5f, 5f),Random.Range(-5f, 5f)),ForceMode2D.Impulse);



	}

	void Update(){

     Destroy(this.gameObject, 3);
	}

}
