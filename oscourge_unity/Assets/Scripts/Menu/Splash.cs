using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{

	float timer = 3.5f;

	void Update()
	{
		timer -= Time.deltaTime;

		if(timer <= 0)
		{
			SceneManager.LoadScene("Menu");
		}
	}



}
