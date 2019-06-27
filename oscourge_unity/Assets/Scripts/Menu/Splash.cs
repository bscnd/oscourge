using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{

    public AudioSource sound;

	float timer = 3.5f;


    public GameObject loadingScene;

    private bool sceneIsLoading = false;
    private AsyncOperation currentLoadingOperation = null;
    void Start()

    {
        int width = 1280;
        int height = 720;
        bool isFullScreen = true; 
        int desiredFPS = 60; 

        Screen.SetResolution(width, height, isFullScreen, desiredFPS);

        sound.PlayDelayed(0.5f);
    }
    void Update()
	{

        if (sceneIsLoading)
        {
            loadingScene.SetActive(true);
            if (currentLoadingOperation.isDone)
            {
                loadingScene.SetActive(false);
                sceneIsLoading = false;
            }
        }


        timer -= Time.deltaTime;

		if(timer <= 0)
        {
            currentLoadingOperation = SceneManager.LoadSceneAsync("Menu");
            sceneIsLoading = true;
        }
	}



}
