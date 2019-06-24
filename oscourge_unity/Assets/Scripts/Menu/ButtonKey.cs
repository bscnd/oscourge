using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonKey : MonoBehaviour
{
	private static bool buttonAlreadySelected;
	public ButtonName buttonName = ButtonName.None;
	public Text textField;
	private Button button;
	private bool keyAsToChange;
	private ColorBlock selectedColorBlock;
	private ColorBlock normalColorBlock;

	// Start is called before the first frame update
	void Start()
	{
		keyAsToChange = false;
		
		button = this.GetComponent<Button>();
		if(button == null){
			Debug.LogError("ButtonKey::Start -- There is no Button component attached to this game object  " + this.gameObject.name);
		}
		
		if(buttonName == ButtonName.None){
			Debug.LogError("ButtonKey::Start -- The field buttonName off this ButtonKey is null -- " + this.gameObject.name);
			this.enabled = false;
		}
		else{
			this.textField.text = InputManager.Instance().GetKeyName(buttonName);
		}
		
		normalColorBlock = button.colors;
		selectedColorBlock = ColorBlock.defaultColorBlock;
		selectedColorBlock.normalColor = normalColorBlock.selectedColor;
		selectedColorBlock.highlightedColor = normalColorBlock.selectedColor;
	}

	public void OnClick(){
		if(!buttonAlreadySelected){
			keyAsToChange = true;
			buttonAlreadySelected = true;

			button.colors = selectedColorBlock;
		}
	}

	void OnGUI(){
		if(keyAsToChange){
			Event e = Event.current;
			if(e != null && e.isKey){
				string keycodeString = KeyCodeToString.Instance().Convert(e.keyCode);

				if(keycodeString != null){
					InputManager.Instance().SetKeyName(buttonName, e.keyCode, keycodeString);
					this.textField.text = InputManager.Instance().GetKeyName(buttonName);

					keyAsToChange = false;
					buttonAlreadySelected = false;
			
					button.colors = normalColorBlock;
				}

			}
		}
	}
}
