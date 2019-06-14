#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Boss1Movement))]
public class Boss1Editor : Editor
{
	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		Boss1Movement boss1 = (Boss1Movement) target;
		
		if(GUILayout.Button("Update position")){
			boss1.setAngleDelta();
			boss1.updatePosition();
		}
	}
}

#endif
