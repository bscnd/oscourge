using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Networking;

public class WaitingScript : MonoBehaviour
{


    public GameObject loadingScene;

    private bool sceneIsLoading=false;
    private AsyncOperation currentLoadingOperation=null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ClientUDP.Instance.gameState == ClientUDP.PLAYING) {
            Debug.Log("Loading Scene ..."); 
             currentLoadingOperation=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            sceneIsLoading=true;
        }
    }

   void Update() {
        if (sceneIsLoading){
            loadingScene.SetActive(true);
            if (currentLoadingOperation.isDone){
                loadingScene.SetActive(false);
                sceneIsLoading = false;
            }
        }

    }
    public void GiveUp() {
        // You said you were never gonna give me up, meh
        if(ClientUDP.Instance.gameState != ClientUDP.OFFLINE)
            ClientUDP.Instance.GiveUp();
    }
}
