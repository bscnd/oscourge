using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{

public AudioSource jump;
public AudioSource run;


public void JumpSound(){
	jump.Play();
}
public void RunSound(){
	if(!run.isPlaying){
	run.Play();
}
}

public void RunStop(){
	run.Stop();
}

 
}
