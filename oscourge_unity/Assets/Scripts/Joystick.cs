using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour{
	private int[] joysticks;
	private const int maxJoystick = 2;

	void Start(){
		joysticks = new int[2];

		joysticks[0] = -1;
		joysticks[1] = -1;

		string[] sticks = Input.GetJoystickNames();

		int index = 0;

		for(int i=0 ; !(i >= sticks.Length || index >= maxJoystick) ; i++){
			if(sticks[i] != null){
				joysticks[index] = i;
				Debug.Log("You are using the joystick number " + (i+1));
				Debug.Log("This joystick is named " + sticks[i]);
				index++;
			}

		}
		InputManager.Instance().SetJoystick(joysticks);
	}
}
