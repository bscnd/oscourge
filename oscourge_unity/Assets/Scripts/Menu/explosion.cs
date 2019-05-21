using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update

	 private GameObject truc;

	public GameObject a1,a2,a3,a4;
    void Start()
    {
    	
      //StartCoroutine(Example());
        
  


    }


void OnCollisionEnter2D(Collision2D col){

           truc=Instantiate(a2, this.transform.position + new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), Quaternion.identity);
            truc.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-1000.0f, 1000.0f),Random.Range(-1000.0f, 1000.0f)));
    
         truc=  Instantiate(a3, this.transform.position+ new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), Quaternion.identity);
            truc.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-1000.0f, 1000.0f),Random.Range(-1000.0f, 1000.0f)));
         truc=  Instantiate(a4,this.transform.position+ new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)), Quaternion.identity);
            truc.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-1000.0f, 1000.0f),Random.Range(-1000.0f, 1000.0f)));
           Destroy(this.gameObject);


}
    

  
}
