using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Networking;

public class GameManager : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject wallBot;
    public GameObject wallTop;

    public GameObject PausePanel;
    public GameObject OptionsPanel;
    public GameObject BlackBar;
    public GameObject DisconnectedPanel;
    public int offset;

    private bool isPaused = false;
    private bool isDisconnected = false;

    // used to force the camera to stay still after respawn
    private bool playerMoved = false;

    private int currentGameState = ClientUDP.Instance.gameState;

    void Update() {
        if (DetectGO()) {
            GameOver();
        }

        if (player1.transform.position.x > camera1.transform.position.x + 19) {
            player1.transform.position = new Vector3(camera1.transform.position.x + 19, player1.transform.position.y, player1.transform.position.z);
        }

        if (player2.transform.position.x > camera2.transform.position.x + 19) {
            player2.transform.position = new Vector3(camera2.transform.position.x + 19, player2.transform.position.y, player2.transform.position.z);
        }

        if (!isDisconnected && Input.GetKeyDown("escape")) {
            if (OptionsPanel.activeSelf) {
                OptionsPanel.SetActive(false);
                PausePanel.SetActive(true);
            }
            else
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
            }
        }

    }

    void FixedUpdate() { // stops during the pause (depends on the timescale ?)
        if (playersHaveMoved() && !isPaused) {
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
        }
        else return false;
    }

    bool DetectGO() {
        if (player1.transform.position.x + offset < camera1.transform.position.x || player2.transform.position.x + offset < camera2.transform.position.x) {
            return true;
        }
        return false;
    }


    void GameOver() {
        player1.GetComponent<PlayerController>().Kill();
        player2.GetComponent<PlayerController>().Kill();
        camera1.GetComponent<CameraController>().Respawn();
        camera2.GetComponent<CameraController>().Respawn();
        wallBot.GetComponent<Parallax>().Reset();
        wallTop.GetComponent<Parallax>().Reset();
        playerMoved = false;

        Lever[] levers = (Lever[])Object.FindObjectsOfType<Lever>();
        foreach (Lever lever in levers) {
            lever.OnGameOver();
        }
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
        if (BlackBar != null)
            BlackBar.SetActive(false);
        else Debug.LogError("BlackBar is null");
        setScrolling(false);
    }

    private void setOffPause() {
        isPaused = false;
        Time.timeScale = 1.0f;
        if (PausePanel != null)
            PausePanel.SetActive(false);
        if (OptionsPanel != null)
            OptionsPanel.SetActive(false);
        else Debug.LogError("PausePanel is null");
        if (BlackBar != null)
            BlackBar.SetActive(true);
        else Debug.LogError("BlackBar is null");
        if (playerMoved)
            setScrolling(true);
    }

    private void setScrolling(bool isScroll) {
        camera1.gameObject.GetComponent<CameraController>().scroll = isScroll;
        camera2.gameObject.GetComponent<CameraController>().scroll = isScroll;
    }
}
