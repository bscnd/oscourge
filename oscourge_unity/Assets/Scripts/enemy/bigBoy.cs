using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class bigBoy : MonoBehaviour
{

	private bool scroll;
	private float speed;

	public Tilemap tilemap;
	public GameObject gameManager;
	public GameObject cam;

	public Animator anim;

    public GameObject destroy;


    private GameObject destroyContainer;

    List<string> values = new List<string>();

	Vector3 spawnPos;

	Vector3Int pos;

	Vector3 pos2;


	private bool isDead;

	void Start(){

		spawnPos=this.transform.position;

		values.Add(tilemap.name);
		values.Add("BlockTemp");
		values.Add("Player1");
		values.Add("Player2");
		values.Add("bigBoy");


		tilemap=gameManager.GetComponent<GameManager>().currentGrid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();	


		pos2=transform.position;


        destroyContainer = new GameObject();
        destroyContainer.name = "destroyContainer";



        for (int i=-50;i<50;i++){

			for(int j=-20;j<6;j++){
				pos.x= (int)pos2.x+j;
				pos.y= (int)pos2.y+i;
				pos.z= 0;

                if (tilemap.GetTile(pos) != null)
                {

                    GameObject g=Instantiate(destroy, pos, Quaternion.identity);
                    g.transform.parent = destroyContainer.transform;


                    tilemap.SetTile(pos, null);
                }



			}
		}

	}

	void Update(){
		bool scroll=cam.GetComponent<CameraController>().scroll;
		float speed=cam.GetComponent<CameraController>().speed;

		if(scroll&& !isDead){
			anim.SetBool("Run",true);

			transform.position=transform.position+new Vector3(speed/100,0,0);
		}


		tilemap=gameManager.GetComponent<GameManager>().currentGrid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
		pos2=transform.position;


		for(int i=-50;i<50;i++){
			pos.x= (int)pos2.x+5;
			pos.y= (int)pos2.y+i;
			pos.z= 0;

            if (tilemap.GetTile(pos) != null)
            {

                GameObject g = Instantiate(destroy, pos, Quaternion.identity);
                g.transform.parent = destroyContainer.transform;
                tilemap.SetTile(pos, null);
            }
        }


	}


	public void Kill(){
		anim.SetBool("Run",false);
		isDead=true;

	}

	public void Respawn(){
		isDead=false;
		this.transform.position=spawnPos;
	pos2=transform.position;


		tilemap=gameManager.GetComponent<GameManager>().currentGrid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
		for(int i=-50;i<50;i++){

			for(int j=-20;j<6;j++){
				pos.x= (int)pos2.x+j;
				pos.y= (int)pos2.y+i;
				pos.z= 0;

				tilemap.SetTile(pos, null);
			}
		}

	}


	void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.CompareTag("Player")){
			gameManager.GetComponent<GameManager>().GameOver();
		}

		else if(!values.Contains(col.transform.name)){
			Renderer r=	col.GetComponent<Renderer>();
			if(r!= null){
                GameObject g = Instantiate(destroy, col.transform.position, Quaternion.identity);
                g.transform.parent = destroyContainer.transform;
                r.enabled = false;
				gameManager.GetComponent<GameManager>().addRenderer(r);
			}
		}
	}
	


	


}
