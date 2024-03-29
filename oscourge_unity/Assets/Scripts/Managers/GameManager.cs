﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public GameObject camera1;
    public GameObject scrollMountains1;
    public GameObject fade;
    public GameObject SFX;
    public GameObject winPanel;
    public GameObject loadingScene;

    public GameObject PausePanel;
    public GameObject OptionsPanel; 
    public GameObject DisconnectedPanel;
    public GameObject ControlsPanel;

    public bool isPaused = false;
    private bool isDisconnected = false;

    // used to force the camera to stay still after respawn
    private bool playerMoved = false;

    private int currentGameState = ClientUDP.Instance.gameState;

    private bool sceneIsLoading = false;
    private AsyncOperation currentLoadingOperation = null;



    public GameObject boy1;


    public List<Renderer> renderers = new List<Renderer>();


    public bool intro ;

    void Start() {
        fade.SetActive(false);
        SFX.gameObject.GetComponent<SFX>().MenuMusicStop();
        SFX.gameObject.GetComponent<SFX>().Music();
        intro = true;

        Time.timeScale = 1.0f;
        isPaused = false;


        Cursor.visible = false;

    }

    void Update() {


        if (sceneIsLoading) {
            winPanel.SetActive(false);
            loadingScene.SetActive(true);
            if (currentLoadingOperation.isDone) {
                loadingScene.SetActive(false);
                sceneIsLoading = false;
            }
        }



        if (player1.transform.position.x > camera1.transform.position.x + 20) {
            player1.transform.position = new Vector3(camera1.transform.position.x + 20, player1.transform.position.y, player1.transform.position.z);
        }

        if (player1.transform.position.x < camera1.transform.position.x - 20)
        {
            player1.transform.position = new Vector3(camera1.transform.position.x - 20, player1.transform.position.y, player1.transform.position.z);
        }

        if (player2.transform.position.x > camera1.transform.position.x + 20) {
            player2.transform.position = new Vector3(camera1.transform.position.x + 20, player2.transform.position.y, player2.transform.position.z);
        }

        if (player2.transform.position.x < camera1.transform.position.x - 20)
        {
            player2.transform.position = new Vector3(camera1.transform.position.x - 20, player2.transform.position.y, player2.transform.position.z);
        }

        //if ((!isDisconnected && Input.GetKeyDown("escape")) || (ClientUDP.Instance.gameState == ClientUDP.OFFLINE && Input.GetKeyDown("escape"))) {
        if ((!isDisconnected && InputManager.Instance().GetButtonDown(ButtonName.Pause)) || (ClientUDP.Instance.gameState == ClientUDP.OFFLINE && InputManager.Instance().GetButtonDown(ButtonName.Pause))) {
            if (OptionsPanel.activeSelf)
            {
                OptionsPanel.SetActive(false);
                PausePanel.SetActive(true);
            }
            else if (ControlsPanel.activeSelf)
            {
                ControlsPanel.SetActive(false);
                OptionsPanel.SetActive(true);
            }

            else if(!intro && !gameIsOver)
                PauseToggle();
        }

        if (ClientUDP.Instance.gameState != currentGameState) {
            currentGameState = ClientUDP.Instance.gameState;

            switch (currentGameState) {
                case ClientUDP.CONNECTION_ERROR:
                    if (isPaused) Debug.LogError("game manager fixedupdate : this should not happen pause means no data");
                    setOnDisconnected();
                    break;
                case ClientUDP.PAUSE:
                    if (!isPaused)
                        setOnPause();
                    break;
                case ClientUDP.PLAYING:
                    Debug.Log("Lets play again !");
                    setOffDisconnected();
                    setOffPause();
                    break;
                case ClientUDP.RESTART:
                    ReplayOnline();
                    break;
            }
        }

    }

    void FixedUpdate() { // stops during the pause (depends on the timescale ?)
        if (!intro && playersHaveMoved() && !isPaused) {
            setScrolling(true);
        }
    }

    bool playersHaveMoved() {
        if (!playerMoved) {
            if (player1.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0 ||
                player1.gameObject.GetComponent<Rigidbody2D>().velocity.y != 0 ||
                player2.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0 ||
                player2.gameObject.GetComponent<Rigidbody2D>().velocity.y != 0) {
                playerMoved = true;
                return true;
            }
            return false;
        } else return false;
    }

    public void Replay() {
        if (ClientUDP.Instance.gameState != ClientUDP.OFFLINE) {
            if (ClientUDP.Instance.gameState != ClientUDP.RESTART) ClientUDP.Instance.sendTypedMessage(Message.RESTART);
        } else {
            currentLoadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            sceneIsLoading = true;
        }
    }

    public void ReplayOnline() {
        currentLoadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        sceneIsLoading = true;
        ClientUDP.Instance.changeGameState(ClientUDP.PLAYING);
    }

    private bool gameIsWon = false;

    public void Win() {
        if (!gameIsWon) {
            StartCoroutine(Won());
        }
    }



    IEnumerator Won() {

        gameIsWon = true;
        camera1.GetComponent<CameraController>().Stop();
        player1.GetComponent<PlayerController>().Win();
        player2.GetComponent<PlayerController>().Win();
        boy1.GetComponent<bigBoy>().Kill();
        yield return new WaitForSeconds(0.3f);
        winPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void nextLevel() {
        gameIsWon = false;
     
            currentLoadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            sceneIsLoading = true;
        
    }


    public bool gameIsOver = false;

    public void GameOver() {
        if (!gameIsOver) {
            StartCoroutine(GO());
        }
    }

    IEnumerator GO() {
        gameIsOver = true;
        camera1.GetComponent<CameraController>().Stop();
        boy1.GetComponent<bigBoy>().Kill();
        fade.SetActive(true);
        player1.GetComponent<PlayerController>().Kill();
        player2.GetComponent<PlayerController>().Kill();
        yield return new WaitForSeconds(4);
        if(scrollMountains1!=null)
        scrollMountains1.GetComponent<Parallax>().Reset();

        playerMoved = false;

        Lever[] levers = (Lever[])Object.FindObjectsOfType<Lever>();
        foreach (Lever lever in levers) {
            lever.OnGameOver();
        }

        player1.GetComponent<PlayerController>().Respawn();
        player2.GetComponent<PlayerController>().Respawn();
        resetRenderers();
        boy1.GetComponent<bigBoy>().Respawn();
        fade.SetActive(false);
        gameIsOver = false;
        camera1.GetComponent<CameraController>().Respawn();
    }


    private void setOnDisconnected() {
        isDisconnected = true;
        Time.timeScale = 0.0f;
        if (DisconnectedPanel != null)
            DisconnectedPanel.SetActive(true);
        else Debug.LogError("Disconnected panel is null");
        setScrolling(false);
        Debug.Log("Disconnected");
    }

    private void setOffDisconnected() {
        isDisconnected = false;
        Time.timeScale = 1.0f;
        if (DisconnectedPanel != null)
            DisconnectedPanel.SetActive(false);
        else Debug.LogError("Disconnected panel is null");
        if (playerMoved)
            setScrolling(true);
    }

    public void PauseToggle() {
        if (ClientUDP.Instance.gameState != ClientUDP.OFFLINE) {
            if (!isPaused)
                ClientUDP.Instance.sendTypedMessage(Message.PAUSE);
            else
                ClientUDP.Instance.sendTypedMessage(Message.RESUME);
        }
        if (!isPaused)
            setOnPause();
        else
            setOffPause();


    }

    private void setOnPause() {
        isPaused = true;
        Time.timeScale = 0.0f;
        if (PausePanel != null)
            PausePanel.SetActive(true);
        else Debug.LogError("PausePanel is null");
        setScrolling(false);
        Cursor.visible = true;
    }

    private void setOffPause() {
        isPaused = false;
        Time.timeScale = 1.0f;
        if (PausePanel != null)
            PausePanel.SetActive(false);
        if (OptionsPanel != null)
            OptionsPanel.SetActive(false);
        if (ControlsPanel != null)
            ControlsPanel.SetActive(false);

            setScrolling(true);

        Cursor.visible = false;
    }

    public void setScrolling(bool isScroll) {
        camera1.gameObject.GetComponent<CameraController>().scroll = isScroll;
    }

    public void addRenderer(Renderer r) {
        renderers.Add(r);
    }

    public void resetRenderers() {
        foreach (Renderer r in renderers) {
            r.enabled = true;
        }
    }



    public void SetCheckpoint(Vector3 p1Offset,Vector3 p2Offset,Vector3 camOffset, Vector3 boyOffset, Vector3 mountainsOffset)
    {
        player1.GetComponent<PlayerController>().spawnLocation = player1.transform.position+p1Offset;
        player2.GetComponent<PlayerController>().spawnLocation = player2.transform.position+p2Offset;
        camera1.GetComponent<CameraController>().spawnLocation = camera1.transform.position+camOffset;
        boy1.GetComponent<bigBoy>().spawnPos = boy1.transform.position+ boyOffset;
        scrollMountains1.GetComponent<Parallax>().startPos = scrollMountains1.transform.position+mountainsOffset;
    }




    public void LoadMenu()
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
