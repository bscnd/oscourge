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
			PauseToggle();
		}


		if(playerMoved() && !isPaused){
			camera1.gameObject.GetComponent<CameraController>().scroll=true;
			camera2.gameObject.GetComponent<CameraController>().scroll=true;

		}
	}


	bool playerMoved(){
		if(player1.gameObject.GetComponent<Rigidbody2D>().velocity.x!=0 || 
			player1.gameObject.GetComponent<Rigidbody2D>().velocity.y!=0 ||
			player2.gameObject.GetComponent<Rigidbody2D>().velocity.x!=0 ||
			player2.gameObject.GetComponent<Rigidbody2D>().velocity.y!=0){
			return true;
		}
		return false;
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

		Lever[] levers = (Lever[]) Object.FindObjectsOfType<Lever>();
		foreach(Lever lever in levers){
			lever.OnGameOver();
		}
	}
	
	
	
	public void PauseToggle()
	{
		
		if (!isPaused)
		{
			Time.timeScale = 0.0f;
			PausePanel.SetActive(true);
			isPaused = true;
			camera1.gameObject.GetComponent<CameraController>().scroll=false;
			camera2.gameObject.GetComponent<CameraController>().scroll=false;
			//Debug.Log("Pause");
		}

		else
		{
			Time.timeScale = 1.0f;
			PausePanel.SetActive(false);
			isPaused = false;
			camera1.gameObject.GetComponent<CameraController>().scroll=true;
			camera2.gameObject.GetComponent<CameraController>().scroll=true;
		}
	}
}
