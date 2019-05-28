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

	public GameObject PausePanel;
	public int offset;
	
	public bool isPaused = false;

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
		
		if(Input.GetKeyDown("escape"))
		{
			if (isPaused == false)
			{
				PausePanel.SetActive(true);
				isPaused = true;
			}

			else
			{
				PausePanel.SetActive(false);
				isPaused = false;
			}
		}
    }

    bool DetectGO(){
		if(player1.transform.position.x+ offset<camera1.transform.position.x  || player2.transform.position.x+offset<camera2.transform.position.x){
			return true;
		}
		return false;
	}


	void GameOver(){
		player1.GetComponent<PlayerController1>().Kill();
		player2.GetComponent<PlayerController2>().Kill();	
		camera1.GetComponent<CameraController>().Respawn();
		camera2.GetComponent<CameraController>().Respawn();
		wallBot.GetComponent<Parallax>().Reset();
		wallTop.GetComponent<Parallax>().Reset();	

		Lever[] levers = (Lever[]) Object.FindObjectsOfType<Lever>();
		foreach(Lever lever in levers){
			lever.OnGameOver();
		}
	}
}
