using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour{
	private int[] joysticks;
	private const int maxJoystick = 2;

	void Start(){
		Debug.Log("Start");
		joysticks = new int[2];

		joysticks[0] = -1;
		joysticks[1] = -1;

		string[] sticks = Input.GetJoystickNames();

		int index = 0;

		for(int i=0 ; !(i >= sticks.Length || index >= maxJoystick) ; i++){
			if(sticks[i] != null){
				joysticks[index] = i;
				index++;
			}

		}

		for(int i=0 ; i<index ; i++){
			Debug.Log("You are using the joystick number" + joysticks[index]);
		}

		if(index == 0){
			Debug.Log("You are not using any joystick !");
		}
	}
}
