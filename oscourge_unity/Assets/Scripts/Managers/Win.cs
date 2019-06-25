using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    public float scrollingOffset = 0;
    GameManager parentScript;

    private void Start()
    {
         parentScript = transform.parent.GetComponent<GameManager>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.CompareTag("Player"))
        {
           
            parentScript.Win();
        }

        if (col.gameObject.CompareTag("MainCamera"))
        {
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(scrollingOffset);
        parentScript.setScrolling(false);
        parentScript.boy1.GetComponent<bigBoy>().Kill();

    }

}
