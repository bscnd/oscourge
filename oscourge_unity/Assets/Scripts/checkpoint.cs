using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject player1;
    public GameObject player2;
    public GameObject cam;
    public GameObject boy;
    public float boyFloat;

    GameManager gm;

    private bool done = false;

    private void Start()
    {
        gm = gameManager.GetComponent<GameManager>();
    }



    public void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.CompareTag("Player"))
        {

            Vector3 p1Offset = player1.transform.position;
            p1Offset.x = -player1.transform.position.x + this.transform.position.x;
            p1Offset.y = -player1.transform.position.y + this.gameObject.transform.GetChild(0).transform.position.y ;
            Vector3 p2Offset = player2.transform.position;
            p2Offset.x = -player2.transform.position.x + this.transform.position.x;
            p2Offset.y = -player2.transform.position.y + this.gameObject.transform.GetChild(1).transform.position.y;
            Vector3 camOffset = cam.transform.position;
            camOffset.x = -cam.transform.position.x + this.transform.position.x;
            camOffset.y = 0;
            camOffset.z = 0;
            Vector3 p0 = new Vector3(0, 0, 0);
            Vector3 boyOffset = new Vector3(0, 0, 0);
            //boyOffset.x = this.transform.position.x - boy.transform.position.x+ boyFloat;
             boyOffset.x =- boy.transform.position.x + this.transform.position.x +boyFloat;



            if (!done)
            {
                done = true;
                gm.SetCheckpoint(p1Offset, p2Offset, camOffset, boyOffset, p0);
            }
        }
    }
}
