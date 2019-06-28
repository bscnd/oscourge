using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchapKeyMenu : MonoBehaviour {
    public GameObject Main;
    public GameObject Options;
    public GameObject Play;
    public GameObject Online;
    public GameObject Join;
    public GameObject Host;
    public GameObject Waiting;
    public GameObject Controls;

    // Update is called once per frame
    void Update() { } }
        //if(Input.GetKeyDown(KeyCode.Escape)){
        //    if (Play.activeInHierarchy || Options.activeInHierarchy)
        //    {
        //        if (Play.activeInHierarchy) 
        //            Play.SetActive(false);
        //        else
        //            Options.SetActive(false);
        //        Main.SetActive(true);
        //    }

        //    if (Online.activeInHierarchy)
        //    {
        //        Online.SetActive(false);
        //        Play.SetActive(true);
        //    }

        //    if (Join.activeInHierarchy || Host.activeInHierarchy || Waiting.activeInHierarchy)
        //    {
        //        if (Join.activeInHierarchy)
        //            Join.SetActive(false);
        //        if (Host.activeInHierarchy)
        //            Host.SetActive(false);
        //        if (Waiting.activeInHierarchy)
        //            Waiting.SetActive(false);
        //        Online.SetActive(true);
        //    }
            // Touche Echap assignable donc retour en même temps
            /*if (Controls.activeInHierarchy)
            {
                Controls.SetActive(false);
                Options.SetActive(true);
            }
            */
//        }
//    }
//}
