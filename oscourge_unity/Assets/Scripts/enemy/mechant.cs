using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechant : MonoBehaviour
{

  public GameObject player;
  public float moveSpeed;
  private Rigidbody2D myRigidbody;

  public float jumpSpeed;

  private bool seen;
  private int z;

  private Vector3 spawn;

  void Start() {
    myRigidbody = GetComponent<Rigidbody2D>();
    seen=false;
    z=0;
    spawn=transform.position;
  }

  void Update()
  {
    float i=player.transform.position.x-transform.position.x;
    if((i>0f&&i<4f)||(i<0f && i>-4f) ){
      seen=true;
    }

    if(seen){
    	if(player.transform.position.x>transform.position.x){
        myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
        transform.localScale = new Vector3(2f, 2f, 1f);
      }
      else if(player.transform.position.x<transform.position.x){

        myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
        transform.localScale = new Vector3(-2f, 2f, 1f);
      }

      if(player.transform.position.y>transform.position.y+3){
       myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
     }
   } else{
    if(z<30){
     myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
     transform.localScale = new Vector3(2f, 2f, 1f);
     z++;
   }
   else if (z<60){

    myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
    transform.localScale = new Vector3(-2f, 2f, 1f);
    z++;
  }
  else if (z==60){
    z=0;
  }


}


}
void OnCollisionEnter2D (Collision2D col)
{
  if(col.gameObject.name == "Player1")
  {
      transform.position=spawn;
    //col.gameObject.GetComponent<PlayerController1>().kill();
    seen=false;
  }
}
}
