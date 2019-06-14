using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
	private Collider2D platformCollider;

	// Start is called before the first frame update
	void Start()
	{
		platformCollider = GetComponent<Collider2D>();

		if(platformCollider == null){
			Debug.Log("A Collision2D object must be attached to this script to make it work !");
			this.enabled = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		collision.collider.transform.SetParent(this.transform);
	}

	void OnCollisionExit2D(Collision2D collision){
		collision.collider.transform.SetParent(null);
	}
}
