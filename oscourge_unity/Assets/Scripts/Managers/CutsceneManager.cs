using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public GameObject player1,player2;

    void Start()
    {
        
    }


    void Update()
    {
        if (GetComponent<PlayableDirector>().state != PlayState.Playing)
        {
            Debug.Log("Cutscene end");
            player1.GetComponent<PlayerController>().intro = false;
            player2.GetComponent<PlayerController>().intro = false;
            //this.gameObject.SetActive(false);

     

        }

    }
}
