using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public GameObject player1,player2;
    public GameObject newPlayer1, newPlayer2;
    public GameObject gameManager;
    public float timeBeforeScroll;
    void Start()
    {
        foreach (Behaviour childCompnent in newPlayer1.GetComponentsInChildren<Behaviour>())
            childCompnent.enabled = false;

        newPlayer1.GetComponent<SpriteRenderer>().enabled = false;

        foreach (Behaviour childCompnent in newPlayer2.GetComponentsInChildren<Behaviour>())
            childCompnent.enabled = false;

        newPlayer2.GetComponent<SpriteRenderer>().enabled = false;


    }
    private  bool hasEnded=false;

    private bool scrollEnabled = false;

    void Update()
    {

        timeBeforeScroll -= Time.deltaTime;
        if (!scrollEnabled && timeBeforeScroll < 0)
        {
            gameManager.GetComponent<GameManager>().setScrolling(true);
        }



        if (GetComponent<PlayableDirector>().state != PlayState.Playing)
        {
         
            if (!hasEnded)
            {
                hasEnded = true;
                Debug.Log("Cutscene end");
                foreach (Behaviour childCompnent in player1.GetComponentsInChildren<Behaviour>())
                    childCompnent.enabled = false;


                player1.GetComponent<SpriteRenderer>().enabled = false;

                foreach (Behaviour childCompnent in player2.GetComponentsInChildren<Behaviour>())
                    childCompnent.enabled = false;


                player2.GetComponent<SpriteRenderer>().enabled = false;

                foreach (Behaviour childCompnent in newPlayer1.GetComponentsInChildren<Behaviour>())
                    childCompnent.enabled = true;


                newPlayer1.GetComponent<SpriteRenderer>().enabled = true;

                foreach (Behaviour childCompnent in newPlayer2.GetComponentsInChildren<Behaviour>())
                    childCompnent.enabled = true;


                newPlayer2.GetComponent<SpriteRenderer>().enabled = true;


                newPlayer1.transform.position = player1.transform.position;
                newPlayer2.transform.position = player2.transform.position;

                gameManager.GetComponent<GameManager>().SetStart();
                gameManager.GetComponent<GameManager>().intro = false; 



            }
          }
        else
        {

            player1.GetComponent<PlayerController>().intro = true;
            player2.GetComponent<PlayerController>().intro = true;


        }

    }
}
