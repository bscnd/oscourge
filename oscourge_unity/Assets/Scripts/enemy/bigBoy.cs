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

	  public GameObject tilemapPrefab;



	List<string> values = new List<string>();

	Vector3Int pos;

	Vector3 pos2;

	void Start(){

		values.Add(tilemap.name);
		values.Add("BlockTemp");
		values.Add("Player1");
		values.Add("Player2");
	}

	void Update(){
		tilemap=gameManager.GetComponent<GameManager>().currentGrid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();	
		pos2=transform.position;


		for(int i=-15;i<15;i++){
			pos.x= (int)pos2.x+1;
			pos.y= (int)pos2.y-i;
			pos.z= (int)pos2.z;

			tilemap.SetTile(pos, null);
		}
	}


	void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.CompareTag("Player")){
			gameManager.GetComponent<GameManager>().GameOver();
		}

		if(!values.Contains(col.transform.name)){
			Renderer r=	col.GetComponent<Renderer>();
			if(r!= null){
				r.enabled = false;
				gameManager.GetComponent<GameManager>().addRenderer(r);
			}
		}
	}
	


	


}
