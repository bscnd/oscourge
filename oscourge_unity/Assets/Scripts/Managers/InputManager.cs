using UnityEngine;
using System.Collections.Generic;
using System.Collections;
	
public enum ButtonName { Left, Right, Jump, Action, Left2, Right2, Jump2, Action2, Pause, None };
public enum AxisName { Horizontal, Horizontal2, None };

// Singleton
public class InputManager
//public class InputManager
{
	private static SortedSet<ButtonName> axis;
	private static InputManager instance;
	private int[] joysticks;
	private long debugCount;

	public static InputManager Instance(){
		if(InputManager.instance == null){
			InputManager.instance = new InputManager();
		}

		return InputManager.instance;
	}

	private Dictionary<ButtonName, KeyCode> buttonKeys;
	private Dictionary<ButtonName, string> keysName;

	private InputManager(){
		debugCount = 0;
		buttonKeys = new Dictionary<ButtonName, KeyCode >();
		keysName = new Dictionary<ButtonName, string>();
		this.joysticks = new int[2];

		// TODO : Change menu with these strings
		// TODO : Change inputs in project settings
		buttonKeys[ButtonName.Left   ] = KeyCode.Q;
		buttonKeys[ButtonName.Right  ] = KeyCode.D;
		buttonKeys[ButtonName.Jump   ] = KeyCode.Space;
		buttonKeys[ButtonName.Action ] = KeyCode.E;
		buttonKeys[ButtonName.Left2  ] = KeyCode.LeftArrow;
		buttonKeys[ButtonName.Right2 ] = KeyCode.RightArrow;
		buttonKeys[ButtonName.Jump2  ] = KeyCode.UpArrow;
		buttonKeys[ButtonName.Action2] = KeyCode.Keypad0;
		buttonKeys[ButtonName.Pause  ] = KeyCode.Escape;
		buttonKeys[ButtonName.None  ] = KeyCode.None;

		ButtonName[] values = (ButtonName[])ButtonName.GetValues(typeof(ButtonName));
		foreach(ButtonName button in values){
			keysName.Add(button, KeyCodeToString.Instance().Convert(buttonKeys[button]));
		}

		this.joysticks[0] = -1;
		this.joysticks[1] = -1;
	}

	public bool GetButtonDown(ButtonName buttonName){
		debugCount++;
		if(buttonKeys.ContainsKey(buttonName) == false){
			Debug.LogError("InputManager::GetButtonDown -- no button named : " + buttonName);
			return false;
		}

		bool res = Input.GetKeyDown(buttonKeys[buttonName]);

		if(this.joysticks[0] > 0){
			int index = this.joysticks[0];
			switch(buttonName){
				case ButtonName.Jump :
					res = res || Input.GetButtonDown("Jump" + index);
					if(res){
						Debug.Log("Method have been called this number of time : " + debugCount);
						Debug.Log("(GetButtonDown) Player 1 : Jump using gamepad number " + index);
					}
					break;
				case ButtonName.Action :
					res = res || Input.GetButtonDown("Action" + index);
					if(res){
						Debug.Log("Method have been called this number of time : " + debugCount);
						Debug.Log("(GetButtonDown) Player 1 : Action using gamepad number " + index);
					}
					break;
				case ButtonName.Pause :
					res = res || Input.GetButtonDown("Pause" + index);
					if(res){
						Debug.Log("Method have been called this number of time : " + debugCount);
						Debug.Log("(GetButtonDown) Player 1 : Pause using gamepad number " + index);
					}
					break;
			}
		}

		if(this.joysticks[1] > 0){
			int index = this.joysticks[1];
			switch(buttonName){
				case ButtonName.Jump2 :
					res = res || Input.GetButtonDown("Jump" + index);
					if(res){
						Debug.Log("Method have been called this number of time : " + debugCount);
						Debug.Log("(GetButtonDown) Player 2 : Jump using gamepad number " + index);
					}
					break;
				case ButtonName.Action2 :
					res = res || Input.GetButtonDown("Action" + index);
					if(res){
						Debug.Log("Method have been called this number of time : " + debugCount);
						Debug.Log("(GetButtonDown) Player 2 : Action using gamepad number " + index);
					}
					break;
				case ButtonName.Pause :
					res = res || Input.GetButtonDown("Pause" + index);
					if(res){
						Debug.Log("Method have been called this number of time : " + debugCount);
						Debug.Log("(GetButtonDown) Player 2 : Pause using gamepad number " + index);
					}
					break;
			}
		}

		return res;

	}

	public bool GetButton(ButtonName buttonName){
		if(buttonKeys.ContainsKey(buttonName) == false){
			Debug.LogError("InputManager::GetButton -- no button named : " + buttonName);
			return false;
		}

		bool res = Input.GetKey(buttonKeys[buttonName]);

		if(this.joysticks[0] > 0){
			int index = this.joysticks[0];
			switch(buttonName){
				case ButtonName.Jump :
					res = res || Input.GetButton("Jump" + index);
					if(res){
						Debug.Log("Player 1 : Jump using gamepad number " + index);
					}
					break;
				case ButtonName.Action :
					res = res || Input.GetButton("Action" + index);
					if(res){
						Debug.Log("Player 1 : Action using gamepad number " + index);
					}
					break;
				case ButtonName.Pause :
					res = res || Input.GetButton("Pause" + index);
					if(res){
						Debug.Log("Player 1 : Pause using gamepad number " + index);
					}
					break;
			}
		}

		if(this.joysticks[1] > 0){
			int index = this.joysticks[1];
			switch(buttonName){
				case ButtonName.Jump2 :
					res = res || Input.GetButton("Jump" + index);
					Debug.Log("Player 2 : Jump using gamepad number " + index);
					break;
				case ButtonName.Action2 :
					res = res || Input.GetButton("Action" + index);
					Debug.Log("Player 2 : Action using gamepad number " + index);
					break;
				case ButtonName.Pause :
					res = res || Input.GetButton("Pause" + index);
					Debug.Log("Player 2 : Pause using gamepad number " + index);
					break;
			}
		}

		return res;
	}

	public float GetAxisRaw(AxisName axisName){
		float horizontal = 0;

		if(axisName == AxisName.None){
			Debug.LogError("InputManager::GetAxisRaw -- no axis named : " + axisName);
		}
		else if(axisName == AxisName.Horizontal){
			if(this.GetButton(ButtonName.Left)){
				horizontal -= 1F;
			}
			if(this.GetButton(ButtonName.Right)){
				horizontal += 1F;
			}
		
			if(this.joysticks[0] > 0){
				int index = this.joysticks[0];
				horizontal += Input.GetAxisRaw("Horizontal" + index);

				if(horizontal > 0){
					Debug.Log("Player 1 is going right using gamepad number " + index);
				}
				else if(horizontal < 0){
					Debug.Log("Player 1 is going left using gamepad number " + index);
				}

			}
		}
		else if(axisName == AxisName.Horizontal2){
			if(this.GetButton(ButtonName.Left2)){
				horizontal -= 1F;
			}
			if(this.GetButton(ButtonName.Right2)){
				horizontal += 1F;
			}
			
			if(this.joysticks[1] > 0){
				int index = this.joysticks[1];
				horizontal += Input.GetAxisRaw("Horizontal" + index);

				if(horizontal > 0){
					Debug.Log("Player 2 is going right using gamepad number " + index);
				}
				else if(horizontal < 0){
					Debug.Log("Player 2 is going left using gamepad number " + index);
				}
			}

		}

		return horizontal;
	}

	public string GetKeyName(ButtonName buttonName){
		return this.keysName[buttonName];
	}

	public void SetKeyName(ButtonName buttonName, KeyCode code, string newName){
		buttonKeys.Remove(buttonName);
		buttonKeys.Add(buttonName, code); 
		keysName.Remove(buttonName);
		keysName.Add(buttonName, newName);
	}

	public void SetJoystick(int[] sticks){
		this.joysticks[0] = sticks[0] + 1;
		this.joysticks[1] = sticks[1] + 1;
	}

}
