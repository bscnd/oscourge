using UnityEngine;
using System.Collections.Generic;
	
public enum ButtonName { Left, Right, Jump, Action, Left2, Right2, Jump2, Action2, Pause, None };

// Singleton
public class InputManager
{
	private static InputManager instance;

	public static InputManager Instance(){
		if(InputManager.instance == null){
			InputManager.instance = new InputManager();
		}

		return InputManager.instance;
	}

	private Dictionary<ButtonName, KeyCode> buttonKeys;
	private Dictionary<ButtonName, string> keysName;
	private Dictionary<ButtonName, string> unityKeyName;

	private InputManager(){
		buttonKeys = new Dictionary<ButtonName, KeyCode >();
		keysName = new Dictionary<ButtonName, string>();
		unityKeyName = new Dictionary<ButtonName, string>();

		// TODO : Change menu with these strings
		// TODO : Change inputs in project settings
		buttonKeys[ButtonName.Left   ] = KeyCode.None;
		buttonKeys[ButtonName.Right  ] = KeyCode.None;
		buttonKeys[ButtonName.Jump   ] = KeyCode.None;
		buttonKeys[ButtonName.Action ] = KeyCode.None;
		buttonKeys[ButtonName.Left2  ] = KeyCode.None;
		buttonKeys[ButtonName.Right2 ] = KeyCode.None;
		buttonKeys[ButtonName.Jump2  ] = KeyCode.None;
		buttonKeys[ButtonName.Action2] = KeyCode.None;
		buttonKeys[ButtonName.Pause  ] = KeyCode.None;

		keysName[ButtonName.Left   ] = "A";
		keysName[ButtonName.Right  ] = "D";
		keysName[ButtonName.Jump   ] = "Space";
		keysName[ButtonName.Action ] = "E";
		keysName[ButtonName.Left2  ] = "Left arrow";
		keysName[ButtonName.Right2 ] = "Right arrow";
		keysName[ButtonName.Jump2  ] = "Up arrow";
		keysName[ButtonName.Action2] = "Keypad 0";
		keysName[ButtonName.Pause  ] = "Escape";

		unityKeyName[ButtonName.Left   ] = "";
		unityKeyName[ButtonName.Right  ] = "";
		unityKeyName[ButtonName.Jump   ] = "";
		unityKeyName[ButtonName.Action ] = "";
		unityKeyName[ButtonName.Left2  ] = "";
		unityKeyName[ButtonName.Right2 ] = "";
		unityKeyName[ButtonName.Jump2  ] = "";
		unityKeyName[ButtonName.Action2] = "";
		unityKeyName[ButtonName.Pause  ] = "";
	}

	public bool GetButtonDown(ButtonName buttonName){
		if(buttonKeys.ContainsKey(buttonName) == false){
			Debug.LogError("InputManager::GetButtonDown -- no button named : " + buttonName);
			return false;
		}

		if(buttonKeys[buttonName] == KeyCode.None){
			return Input.GetButton(this.GetKeyName(buttonName));
		}
		else{
			return Input.GetKeyDown(buttonKeys[buttonName]);
		}
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
