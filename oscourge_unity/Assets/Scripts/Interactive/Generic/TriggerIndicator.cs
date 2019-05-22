using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIndicator
{
	public static GameObject prefabTriggerIndicator = Resources.Load<GameObject>("TriggerIndicator");

	private static Color[] colors =
	{
		 new Color(0.0F, 5.0F, 5.0F, 0.5F) // Cyan
		,new Color(1.0F, 5.0F, 1.0F, 0.5F) // Light green
		,new Color(5.0F, 1.0F, 1.0F, 0.5F) // Light red
		,new Color(1.0F, 1.0F, 5.0F, 0.5F) // Light blue
		,new Color(5.0F, 0.0F, 0.0F, 0.5F) // Red 
		,new Color(0.0F, 5.0F, 0.0F, 0.5F) // Green
		,new Color(0.0F, 0.0F, 5.0F, 0.5F) // Blue
		,new Color(5.0F, 5.0F, 0.0F, 0.5F) // Yellow
		,new Color(5.0F, 0.0F, 1.0F, 0.5F) // Magenta
		,new Color(5.0F, 5.0F, 5.0F, 0.5F) // Grey 
	};

	private static int colorIndex = 0;

	private static Color nextColor(){
		if(colorIndex >= colors.Length){
			Debug.Log("Your scene is using too much mecanisms, like levers or pressure plates, for the number of colors present in the script TriggerIndicator.cs, please add more colors !");
		}
		return colors[colorIndex++];
	}

	public static GameObject spawn(Vector3 position, Vector3 delta){
		Vector3 newPosition = position + delta;

		GameObject indicator = GameObject.Instantiate(prefabTriggerIndicator, newPosition, Quaternion.identity);
		SpriteRenderer renderer = indicator.GetComponent<SpriteRenderer>();
		Material mat = new Material(Shader.Find("Sprites/Diffuse"));
		mat.color = TriggerIndicator.nextColor();
		renderer.material = mat;

		return indicator;
	}

	public static GameObject spawn(Vector3 position){
		return spawn(position, new Vector3(0, 0, 0));
	}
}
