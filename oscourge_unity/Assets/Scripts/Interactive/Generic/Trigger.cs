using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	public Chain[] chains;

	protected void initIndicators(){
		GameObject prefabTriggerIndicator = TriggerIndicator.prefabTriggerIndicator;
		if(prefabTriggerIndicator != null && prefabTriggerIndicator.tag == "Indicator"){
			if(chains.Length > 0){
				float[] angle = new float[5];
				angle[0] = 90;
				angle[1] = 120;
				angle[2] = 60;
				angle[3] = 150;
				angle[4] = 30;

				int index = 0;
				foreach(Chain chain in chains){
					GameObject chainIndicator = chain.initTriggerIndicator();
					
					Vector3 position = new Vector3(Mathf.Cos(angle[index] * Mathf.Deg2Rad), Mathf.Sin(angle[index] * Mathf.Deg2Rad), 0);
					position += this.transform.position;

					GameObject obj = GameObject.Instantiate(chainIndicator, position, Quaternion.identity);
					obj.transform.SetParent(this.transform);
					index++;
				}
			}
		}
		else{
			Debug.Log("Can't find the prefab 'TriggerIndicator' witch must have the tag 'Indicator' !");
		}
	}
}
