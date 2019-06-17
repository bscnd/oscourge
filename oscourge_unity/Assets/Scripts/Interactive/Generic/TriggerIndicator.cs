using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIndicator
{
	public static GameObject prefabTriggerIndicator = Resources.Load<GameObject>("TriggerIndicator");

	private static List<Color> colors;
	/*
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
		,new Color(0.5F, 5.0F, 1.0F, 0.5F) // Light green
		,new Color(2.5F, 2.0F, 1.0F, 0.5F) // Light red
		,new Color(0.5F, 2.0F, 5.0F, 0.5F) // Light blue
		,new Color(2.5F, 0.4F, 0.0F, 0.5F) // Red 
		,new Color(2.5F, 5.0F, 0.0F, 0.5F) // Yellow
		,new Color(2.5F, 0.4F, 1.0F, 0.5F) // Magenta
		,new Color(2.5F, 5.0F, 5.0F, 0.5F) // Grey 

	};
	*/
	
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

			/*
			colors.Add(getNewColor("#B0171F")); // Indian red
			colors.Add(getNewColor("#FF1493")); // Deeppink
			colors.Add(getNewColor("#8B008B")); // Magenta
			colors.Add(getNewColor("#4B0082")); // Indigo
			colors.Add(getNewColor("#6A5ACD")); // Slateblue
			colors.Add(getNewColor("#0000FF")); // Blue
			colors.Add(getNewColor("#436EEE")); // Royal blue
			colors.Add(getNewColor("#87CEEB")); // Deep sky blue
			colors.Add(getNewColor("#00E5EE")); // Turquoise
			colors.Add(getNewColor("#008B8B")); // Dark cyan
			colors.Add(getNewColor("#008B45")); // Springgreen
			colors.Add(getNewColor("#006400")); // Dark green
			colors.Add(getNewColor("#FFFF00")); // Yellow
			colors.Add(getNewColor("#FFA500")); // Orange
			colors.Add(getNewColor("#8B5A2B")); // Tan
			colors.Add(getNewColor("#FF0000")); // Red
			colors.Add(getNewColor("#515151")); // Grey
			*/
			
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

		/*
		float red = Random.Range(0.1F, 1F);
		float blue = Random.Range(0.1F, 1F);
		float green = Random.Range(0.1F, 1F);

		Color color = new Color(red, blue, green, 0.5F);
		colors.Add(color);

		return color;
		*/
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
		//Material mat = new Material(Shader.Find("Sprites/Diffuse"));
		Material mat = new Material(Shader.Find("Sprites/Default"));
		mat.color = TriggerIndicator.nextColor();
		renderer.material = mat;

		return indicator;
	}

	public static GameObject spawn(Vector3 position){
		return spawn(position, new Vector3(0, 0, 0));
	}
}
