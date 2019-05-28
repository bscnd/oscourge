using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Scripts.Networking;

public class MainMenu : MonoBehaviour
{

    public TMP_InputField ipInput;
    public TMP_InputField portInput;

    public void PlayGame()
    {
        string ip = ipInput.text;
        int port = int.Parse(portInput.text);

        Debug.Log("ip : " + ip + " / port : " + port);
        ClientUDP.Instance.ConnectToServer(ip, port);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
