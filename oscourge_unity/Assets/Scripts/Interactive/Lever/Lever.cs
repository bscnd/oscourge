using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
	private Animator myAnim;
	public Chain[] chains;
	private bool playerIsNear = false;
	public GameObject triggerIndicator;
	//private Transfrom transform;
	
	int hashIsTriggered = Animator.StringToHash("isTriggered");
	

	// Start is called before the first frame update
	void Start()
	{
		myAnim = GetComponent<Animator>();
		initIndicators();
	}

	public void updateInteractiveObjects(){
		foreach(Chain chain in chains){
			chain.trigger(myAnim.GetBool(hashIsTriggered), this.GetInstanceID());
		}
	}

	void Update(){
		if(playerIsNear && Input.GetButtonDown("ContextualAction")){
			myAnim.SetBool(hashIsTriggered, !myAnim.GetBool(hashIsTriggered));
			updateInteractiveObjects();
		}
	}

	private void OnTriggerExit2D(Collider2D collision){
		if(collision.tag == "Player"){
			playerIsNear = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag == "Player") {
			playerIsNear = true;
		}
	}

	private void initIndicators(){
		if(triggerIndicator != null && triggerIndicator.tag == "Indicator"){
			if(chains.Length > 0){
				float[] angle = new float[5];
				angle[0] = 90;
				angle[1] = 120;
				angle[2] = 60;
				angle[3] = 150;
				angle[4] = 30;

				int index = 0;
				foreach(Chain chain in chains){
					Vector3 position = new Vector3(Mathf.Cos(angle[index] * Mathf.Deg2Rad), Mathf.Sin(angle[index] * Mathf.Deg2Rad), 0);
					position += this.transform.position;
					GameObject indicator = Instantiate(triggerIndicator, position, Quaternion.identity);
					SpriteRenderer renderer = indicator.GetComponent<SpriteRenderer>();
					Material mat = new Material(Shader.Find("Sprites/Diffuse"));
					mat.color = Chain.getAColor();
					renderer.material = mat;
					index++;
				}
			}
		}
	}
}
