using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{



	public GameObject player1;
	public GameObject player2;
	public GameObject camera1;
	public GameObject camera2;
	public GameObject wallBot;
	public GameObject wallTop;
	public int offset;

    void Update()
    {
        	if(DetectGO()){
			GameOver();
		}

		if(player1.transform.position.x>camera1.transform.position.x+19){
			player1.transform.position=new Vector3(camera1.transform.position.x+19,player1.transform.position.y,player1.transform.position.z);
		}

		if(player2.transform.position.x>camera2.transform.position.x+19){
			player2.transform.position=new Vector3(camera2.transform.position.x+19,player2.transform.position.y,player2.transform.position.z);
		}
    }

    bool DetectGO(){
		if(player1.transform.position.x+ offset<camera1.transform.position.x  || player2.transform.position.x+offset<camera2.transform.position.x){
			return true;
		}
		return false;
	}


	void GameOver(){
		player1.GetComponent<PlayerController>().Kill();
		player2.GetComponent<PlayerController>().Kill();	
		camera1.GetComponent<CameraController>().Respawn();
		camera2.GetComponent<CameraController>().Respawn();
		wallBot.GetComponent<Parallax>().Reset();
		wallTop.GetComponent<Parallax>().Reset();	
	}
}
