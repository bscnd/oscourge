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

    public GameObject SFX;


    private GameObject destroyContainer;

    List<string> values = new List<string>();

	public Vector3 spawnPos;

	Vector3Int pos;

	Vector3 pos2;


	private bool isDead;

    List<Vector3> tiles = new List<Vector3>();

    void Start(){

		spawnPos=this.transform.position;

		values.Add(tilemap.name);
		values.Add("BlockTemp");
		values.Add("Player1");
		values.Add("Player2");
		values.Add("bigBoy");


		tilemap=gameManager.GetComponent<GameManager>().currentGrid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();	


		pos2=transform.position;


        destroyContainer = new GameObject
        {
            name = "destroyContainer"
        };



        for (int i=-15;i<50;i++){

			for(int j=-20;j<6;j++){
				pos.x= (int)pos2.x+j;
				pos.y= (int)pos2.y+i;
				pos.z= 0;


                if (tilemap.GetTile(pos) != null && !tiles.Contains(pos)) {

                    GameObject g=Instantiate(destroy, pos, Quaternion.identity);
                    g.transform.parent = destroyContainer.transform;


                    //  tilemap.SetTile(pos, null);
                    tiles.Add(pos);
                    SFX.gameObject.GetComponent<SFX>().destructionSound();
                }



            }
        }

	}

	void FixedUpdate(){
		bool scroll=cam.GetComponent<CameraController>().scroll;
		float speed=cam.GetComponent<CameraController>().speed;
        bool intro = gameManager.GetComponent<GameManager>().intro;


        if( intro || (scroll && !isDead))
        {
            StartCoroutine(WalkSound());
        }
  

        if (scroll&& !isDead){
			anim.SetBool("Run",true);


			transform.position=transform.position+new Vector3(speed/100,0,0);

        }




        tilemap =gameManager.GetComponent<GameManager>().currentGrid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
		pos2=transform.position;


		for(int i=-15;i<50;i++){
			pos.x= (int)pos2.x+5;
			pos.y= (int)pos2.y+i;
			pos.z= 0;



            if (tilemap.GetTile(pos) != null && !tiles.Contains(pos))
            {

                GameObject g = Instantiate(destroy, pos, Quaternion.identity);
                g.transform.parent = destroyContainer.transform;
                //   tilemap.SetTile(pos, null);
                tiles.Add(pos);
                SFX.gameObject.GetComponent<SFX>().destructionSound();
            }
        }


	}


    IEnumerator WalkSound()
    {

        yield return new WaitForSeconds(0.5f);
        SFX.GetComponent<SFX>().KnightRunSound();
    }


        public void Kill(){
		anim.SetBool("Run",false);
		isDead=true;
        SFX.gameObject.GetComponent<SFX>().KnightRunStop();

    }

	public void Respawn(){
		isDead=false;
		this.transform.position=spawnPos;
	pos2=transform.position;
        tiles.Clear();


		tilemap=gameManager.GetComponent<GameManager>().currentGrid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
		for(int i=-15;i<50;i++){

			for(int j=-20;j<6;j++){
				pos.x= (int)pos2.x+j;
				pos.y= (int)pos2.y+i;
				pos.z= 0;


                if (tilemap.GetTile(pos) != null && !tiles.Contains(pos)) {

                    GameObject g = Instantiate(destroy, pos, Quaternion.identity);
                    g.transform.parent = destroyContainer.transform;
                    //   tilemap.SetTile(pos, null);
                    tiles.Add(pos);
                    SFX.gameObject.GetComponent<SFX>().destructionSound();
                }
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
