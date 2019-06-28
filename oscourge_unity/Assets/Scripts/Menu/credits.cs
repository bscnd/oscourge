using Scripts.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{

    public float speed;

    public float timeBeforeMenu = 10;

    private bool sceneIsLoading = false;
    private AsyncOperation currentLoadingOperation = null;

    public GameObject loadingScene;
    public GameObject creditsPanel;


    private void Start()
    {


        Cursor.visible = false;
    }
    void Update()
    {

        timeBeforeMenu -= Time.deltaTime;
        if (timeBeforeMenu < 0)
        {
            Menu();
        }


        transform.position += new Vector3(0, speed, 0);


        if (sceneIsLoading)
        {
            creditsPanel.SetActive(false);
            loadingScene.SetActive(true);
            if (currentLoadingOperation.isDone)
            {
                loadingScene.SetActive(false);
                sceneIsLoading = false;
            }
        }
    }


    public void Menu()
    {

        currentLoadingOperation = SceneManager.LoadSceneAsync("Menu");
        sceneIsLoading = true;

    }

    void OnApplicationQuit()
    {
        if (ClientUDP.Instance.gameState != ClientUDP.OFFLINE)
            ClientUDP.Instance.sendTypedMessage(Message.ENDGAME);
    }

}