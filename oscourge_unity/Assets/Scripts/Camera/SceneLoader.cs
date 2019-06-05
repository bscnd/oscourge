using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Networking;

public class SceneLoader : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (ClientUDP.Instance.gameState == ClientUDP.RESTART) {
            Replay();
        }
    }


    public void Quit() {
        Application.Quit();
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");

    }

    public void Replay() {
        if (ClientUDP.Instance.gameState != ClientUDP.RESTART) ClientUDP.Instance.sendTypedMessage(Message.RESTART);
        SceneManager.LoadScene("Level1");
    }
}
