using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetDefaultSelected : MonoBehaviour
{
	public EventSystem system;
	public GameObject selectable;

	void OnEnable(){
		updateSelectable();
	}

	void Awake(){
		updateSelectable();
	}

	void updateSelectable(){
		if(system != null){
			if(selectable != null){
				system.SetSelectedGameObject(selectable);
			}
			else{
				Debug.Log("SetDefaultSelected.cs::SetDefaultSelected -- selectable field is null !");
			}
		}
		else{
			Debug.Log("SetDefaultSelected.cs::SetDefaultSelected -- system field is null !");
		}
	}
}
