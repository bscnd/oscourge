using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Net;
using Scripts.Networking;
using System;
using UnityEngine.Audio;
using System.Threading;

public class MainMenu : MonoBehaviour {

    public TMP_InputField ipInput;
    public TMP_InputField portInput;
    public TMP_InputField portInputHost;
    public GameObject ErrorBox;
    public TextMeshProUGUI ErrorTxt;
    public GameObject SFX;
    public GameObject waitingPanel;
    public GameObject onlinePanel;
    public GameObject playPanel;
    public GameObject hostPanel;
    public AudioMixer mixer;
    public GameObject loadingScene;
    public GameObject server;

    private bool sceneIsLoading=false;
    private AsyncOperation currentLoadingOperation=null;



    public void HostGame()
    {
        try
        {

            if (string.IsNullOrEmpty(portInputHost.text)) { EmptyInputFieldError(); }
            else
            {
                changePanel(1);
                string ip = "127.0.0.1";
                IPAddress adresse = IPAddress.Parse(ip);
                int port = int.Parse(portInputHost.text);

                server.GetComponent<serverLauncher>().Launch(port);

                Thread.Sleep(1000); // Waiting for the server to actually start before connecting

                Debug.Log("ip : " + ip + " / port : " + port);
                ClientUDP.Instance.ConnectToServer(ip, port);
                //ClientUDP.Instance.ConnectToServer("127.0.0.1", 1331);
                //Debug.LogError("RESET THESES LIGNES !");
            }
        }
        catch (FormatException e)
        {
            Debug.LogError(e);
            ErrorTxt.SetText("This is not a correct IP adress");
            ErrorBox.SetActive(true);
        }
    }

    public void PlayGame(bool modeOnline) {
        if (modeOnline) {
            try {

                if (string.IsNullOrEmpty(ipInput.text) || string.IsNullOrEmpty(portInput.text)) { EmptyInputFieldError(); }
                else {
                    changePanel(0);

                    string ip = ipInput.text;
                    IPAddress adresse = IPAddress.Parse(ip);
                    int port = int.Parse(portInput.text);

                    Debug.Log("ip : " + ip + " / port : " + port);
                    ClientUDP.Instance.ConnectToServer(ip, port);
                    //ClientUDP.Instance.ConnectToServer("127.0.0.1", 1331);
                    //Debug.LogError("RESET THESES LIGNES !");
                }
            }
            catch (FormatException e) {
                Debug.LogError(e);
                ErrorTxt.SetText("This is not a correct IP adress");
                ErrorBox.SetActive(true);
            }
        }
        else {
            // Starting the game in local (offline mode)
            Debug.Log("Loading Scene ...");
            currentLoadingOperation=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            sceneIsLoading=true;
        }
    }

    public void resetColorIpInput() {
        ipInput.image.color = Color.white;
    }
    public void resetColorPortInput() {
        portInput.image.color = Color.white;
    }

    public void resetInputs() {
        resetColorIpInput();
        resetColorPortInput();
        ipInput.text = "";
        portInput.text = "";
    }

    private void EmptyInputFieldError() {
        if (string.IsNullOrEmpty(ipInput.text))
        ipInput.image.color = Color.red;
        if (string.IsNullOrEmpty(portInput.text))
        portInput.image.color = Color.red;
        if (string.IsNullOrEmpty(portInputHost.text))
            portInputHost.image.color = Color.red;
    }

    private void changePanel(int i) {
        if (i == 0)
        {
            waitingPanel.SetActive(true);
            onlinePanel.SetActive(false);
        }
        if (i == 1)
        {
            waitingPanel.SetActive(true);
            hostPanel.SetActive(false);

        }
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start() {
        float   sliderValue = PlayerPrefs.GetFloat("Master", 0.75f);
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        sliderValue = PlayerPrefs.GetFloat("Music", 0.75f);
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        sliderValue = PlayerPrefs.GetFloat("SFX", 0.75f);
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);

        SFX.gameObject.GetComponent<SFX>().MenuMusic();

    }

    // Update is called once per frame
    void Update() {
        if (sceneIsLoading){
            playPanel.SetActive(false);
            loadingScene.SetActive(true);
            if (currentLoadingOperation.isDone){
                loadingScene.SetActive(false);
                sceneIsLoading = false;
            }
        }

    }


    public void SwitchFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
