using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanism : MonoBehaviour
{
	protected GameObject indicator;
	
	public abstract GameObject initTriggerIndicator();
}
