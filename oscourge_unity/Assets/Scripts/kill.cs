using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill : MonoBehaviour
{
 public void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.CompareTag("Player")){
			GameManager parentScript = transform.parent.parent.GetComponent<GameManager>();
			parentScript.GameOver();
		}

	}
}
