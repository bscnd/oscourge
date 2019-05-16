using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{


	public GameObject a1,a2,a3,a4;

	 private GameObject truc;
    // Start is called before the first frame update
    void Start()
    {
          truc= Instantiate(a1, this.transform.position, Quaternion.identity);
            truc.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-600.0f, 600.0f),Random.Range(-1000.0f, 0.0f))); 
               }

    // Update is called once per frame

        int i=0;
    void Update()
    {
        if(i<100){
        	i++;
        }
else{
	i=0;
        truc= Instantiate(a1, this.transform.position, Quaternion.identity);
            truc.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-600.0f, 600.0f),Random.Range(-1000.0f, 0.0f))); 

        }
    }
}
