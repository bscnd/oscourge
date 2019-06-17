using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update

	private GameObject spawn;

	public GameObject part1,part2,part3;

  private bool hasExploded=false;

  void OnCollisionEnter2D(Collision2D col){

    if(!hasExploded){
      hasExploded=true;

      spawn=Instantiate(part1, this.transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), Quaternion.identity);
      spawn.transform.parent =  this.gameObject.transform.parent.transform;  

      Transform par=this.transform.parent;

       

    spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-10f, 10f),Random.Range(-10f, 10f)),ForceMode2D.Impulse);

     spawn=  Instantiate(part2, this.transform.position+ new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), Quaternion.identity);
     spawn.transform.parent =  this.gameObject.transform.parent.transform;  
     par=this.transform.parent;

    

    spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-10f, 10f),Random.Range(-10f, 10f)),ForceMode2D.Impulse);


   spawn=  Instantiate(part3,this.transform.position+ new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), Quaternion.identity);
   spawn.transform.parent =  this.gameObject.transform.parent.transform;  

   par=this.transform.parent;

  

    spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-10f, 10f),Random.Range(-10f, 10f)),ForceMode2D.Impulse);

 Destroy(this.gameObject);
}

}



/*

IGNORE THIS

Vector3 RecalculatedPosition()
{
 float x,y;
 x = UnityEngine.Random.Range(transform.position.x, transform.position.x + 1);
 y = UnityEngine.Random.Range(transform.position.y, transform.position.y + 1);
 return new Vector3(x,y,1);
}


 foreach(Transform child in par)
   {

    if(spawn.gameObject.GetComponent<PolygonCollider2D>().bounds.Intersects(child.transform.GetComponent<PolygonCollider2D>().bounds)){
     transform.position = RecalculatedPosition();
   }
 }

 */

}
