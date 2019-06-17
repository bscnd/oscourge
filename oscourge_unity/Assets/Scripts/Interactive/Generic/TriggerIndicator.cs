using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIndicator
{
	public static GameObject prefabTriggerIndicator = Resources.Load<GameObject>("TriggerIndicator");

	private static List<Color> colors;
	
	private static int colorIndex = 0;

	private static Color getNewColor(string htmlString){
		Color res = new Color(0F, 0F, 0F, 0F);

		ColorUtility.TryParseHtmlString(htmlString, out res);
		res = res * new Color(1F, 1F, 1F, 1F);
		res.a = 0.50F;
		res.r += res.r; 
		res.g += res.g;
		res.b += res.b;

		return res;
	}
	
	private static Color nextColor(){
		if(colors == null){
			colors = new List<Color>();

			colors.Add(getNewColor("#0000FF")); // Blue
			colors.Add(getNewColor("#00FF00")); // Green
			colors.Add(getNewColor("#FF0000")); // Red
			colors.Add(getNewColor("#FF00FF")); // Magenta
			colors.Add(getNewColor("#FFFF00")); // Yellow
			colors.Add(getNewColor("#00FFFF")); // Cyan
			colors.Add(getNewColor("#FFFAF0")); // White
			colors.Add(getNewColor("#808080")); // White
			colors.Add(getNewColor("#8B4513")); // Brown
			colors.Add(getNewColor("#800080")); // Purple
			colors.Add(getNewColor("#FFA500")); // Orange
			colors.Add(getNewColor("#87CEFA")); // Light blue
			colors.Add(getNewColor("#90EE90")); // Light green
			colors.Add(getNewColor("#FFB6C1")); // Light pink
			colors.Add(getNewColor("#B22222")); // Light red
			colors.Add(getNewColor("#008080")); // Teal
			colors.Add(getNewColor("#800000")); // Maroon
			colors.Add(getNewColor("#2F4F4F")); // Gray
		}

		if(colorIndex == colors.Count){
			Debug.Log("Your scene is using too much mecanisms, like levers or pressure plates, for the number of colors present in the script TriggerIndicator.cs, please add more colors !");
			colorIndex = 0;
		}
		
		int resIndex = colorIndex;
		colorIndex++;

		return colors[resIndex];
	}

	public static GameObject spawn(Vector3 position, Vector3 delta){
		Vector3 newPosition = position + delta;

		GameObject indicator = GameObject.Instantiate(prefabTriggerIndicator, newPosition, Quaternion.identity);
		SpriteRenderer renderer = indicator.GetComponent<SpriteRenderer>();
		Material mat = new Material(Shader.Find("Sprites/Default"));
		mat.color = TriggerIndicator.nextColor();
		renderer.material = mat;

		return indicator;
	}

	public static GameObject spawn(Vector3 position){
		return spawn(position, new Vector3(0, 0, 0));
	}
}
