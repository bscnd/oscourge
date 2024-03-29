﻿using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement;
using Scripts.Networking;

public class VideoManager : MonoBehaviour {

    private bool sceneIsLoading = false;
    public GameObject loadingScene, intro;
    private VideoPlayer m_VideoPlayer;
    private AsyncOperation currentLoadingOperation = null;

    void Update() {
        if (sceneIsLoading) {
            intro.SetActive(false);
            loadingScene.SetActive(true);
        }


        if (InputManager.Instance().GetButtonDown(ButtonName.Pause))
        {
            if (ClientUDP.Instance.gameState == ClientUDP.OFFLINE)
            {
                Debug.Log("Event for movie end called");
                GetComponent<VideoPlayer>().Stop();
                currentLoadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                sceneIsLoading = true;
            }
        }




    }

    void Awake() {
        m_VideoPlayer = GetComponent<VideoPlayer>();
        m_VideoPlayer.loopPointReached += OnMovieFinished; // loopPointReached is the event for the end of the video

        Cursor.visible = false;
    }

    void OnMovieFinished(VideoPlayer player) {
        Debug.Log("Event for movie end called");
        currentLoadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        sceneIsLoading = true;
        player.Stop();
    }


    IEnumerator NextLevel() {
        yield return new WaitForSeconds(0);

        currentLoadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        sceneIsLoading = true;
    }

    void OnApplicationQuit()
    {
        if (ClientUDP.Instance.gameState != ClientUDP.OFFLINE)
            ClientUDP.Instance.sendTypedMessage(Message.ENDGAME);
    }
}
