using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        
    }

    public void Replay()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PauseToggle()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.0f;
            Debug.Log("allo");
        }
            
        else
            Time.timeScale = 1.0f;
    }
}
