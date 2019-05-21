using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
	// TODO : Put the color code in the script indicators
	private static Color[] colors =
	{
		new Color(0F, 0F, 0F) // Black
		,new Color(255F, 72F, 226F) // Pink
		,new Color(13F, 113F, 148F) // Light blue
		,new Color(32F, 123F, 32F) // Light green
		,new Color(247F, 4F, 4F) // Red
		,new Color(242F, 255F, 0F) // Yellow
		,new Color(123F, 123F, 108F) // Grey
		,new Color(36F, 0F, 173F) // True blue
		,new Color(173F, 0F, 91F) // Magenta
		,new Color(115F, 64F, 40F) // Brown
	};

	private static int colorIndex = 0;

	private Animator myAnim;

	// The number of pressurePlates/levers/... that has to be activated to trigger this gameobject
	public int activatorNumber = 1;

	// Set of currently activated activator's id
	private SortedSet<int> activators;

	private Collider2D chainCollider;

	int hashIsTriggered = Animator.StringToHash("isTriggered");

	void Start()
	{
		myAnim = GetComponent<Animator>();
		activators = new SortedSet<int>();
		chainCollider = GetComponent<Collider2D>();

	}

	public void trigger(bool isActivate, int objectId){
		if(isActivate){
			activators.Add(objectId);
		}
		else{
			activators.Remove(objectId);
		}

		myAnim.SetBool(hashIsTriggered, activators.Count >= activatorNumber);
	}

	public void disableCollider(){
		this.chainCollider.enabled = false;
	}
	
	public void enableCollider(){
		this.chainCollider.enabled = true;
	}

	public static Color getAColor(){
		return colors[colorIndex++];
	}
}
