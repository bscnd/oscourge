using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update

	private GameObject spawn;

	public GameObject part1,part2,part3;



  void OnCollisionEnter2D(Collision2D col){

   spawn=Instantiate(part1, this.transform.position + new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), Quaternion.identity);
   spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-1000.0f, 1000.0f),Random.Range(-1000.0f, 1000.0f)));
   
   spawn=  Instantiate(part2, this.transform.position+ new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), Quaternion.identity);
   spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-1000.0f, 1000.0f),Random.Range(-1000.0f, 1000.0f)));
   spawn=  Instantiate(part3,this.transform.position+ new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), Quaternion.identity);
   spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-1000.0f, 1000.0f),Random.Range(-1000.0f, 1000.0f)));
   Destroy(this.gameObject);


 }
 

 
}
