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

	public static InputManager Instance(){
		if(InputManager.instance == null){
			InputManager.instance = new InputManager();
		}

		return InputManager.instance;
	}

	private Dictionary<ButtonName, KeyCode> buttonKeys;
	private Dictionary<ButtonName, string> keysName;

	private InputManager(){
		buttonKeys = new Dictionary<ButtonName, KeyCode >();
		keysName = new Dictionary<ButtonName, string>();

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

		/*
		keysName[ButtonName.Left   ] = "A";
		keysName[ButtonName.Right  ] = "D";
		keysName[ButtonName.Jump   ] = "Space";
		keysName[ButtonName.Action ] = "E";
		keysName[ButtonName.Left2  ] = "Left arrow";
		keysName[ButtonName.Right2 ] = "Right arrow";
		keysName[ButtonName.Jump2  ] = "Up arrow";
		keysName[ButtonName.Action2] = "Keypad 0";
		keysName[ButtonName.Pause  ] = "Escape";
		*/
	}

	public bool GetButtonDown(ButtonName buttonName){
		if(buttonKeys.ContainsKey(buttonName) == false){
			Debug.LogError("InputManager::GetButtonDown -- no button named : " + buttonName);
			return false;
		}

		bool res = Input.GetKeyDown(buttonKeys[buttonName]);

		switch(buttonName){
			case ButtonName.Jump :
				res = res || Input.GetButtonDown("Jump");
				break;
			case ButtonName.Jump2 :
				res = res || Input.GetButtonDown("Jump2");
				break;
			case ButtonName.Action :
				res = res || Input.GetButtonDown("Action");
				break;
			case ButtonName.Action2 :
				res = res || Input.GetButtonDown("Action2");
				break;
			case ButtonName.Pause :
				res = res || Input.GetButtonDown("Pause");
				res = res || Input.GetButtonDown("Pause2");
				break;
		}

		return res;
	}

	public bool GetButton(ButtonName buttonName){
		if(buttonKeys.ContainsKey(buttonName) == false){
			Debug.LogError("InputManager::GetButtonDown -- no button named : " + buttonName);
			return false;
		}

		bool res = Input.GetKey(buttonKeys[buttonName]);

		switch(buttonName){
			case ButtonName.Jump :
				res = res || Input.GetButton("Jump");
				break;
			case ButtonName.Jump2 :
				res = res || Input.GetButton("Jump2");
				break;
			case ButtonName.Action :
				res = res || Input.GetButton("Action");
				break;
			case ButtonName.Action2 :
				res = res || Input.GetButton("Action2");
				break;
			case ButtonName.Pause :
				res = res || Input.GetButton("Pause");
				res = res || Input.GetButton("Pause2");
				break;
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
		
			horizontal += Input.GetAxisRaw("Horizontal");
		}
		else if(axisName == AxisName.Horizontal2){
			if(this.GetButton(ButtonName.Left2)){
				horizontal -= 1F;
			}
			if(this.GetButton(ButtonName.Right2)){
				horizontal += 1F;
			}
			
			horizontal += Input.GetAxisRaw("Horizontal2");
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
}
