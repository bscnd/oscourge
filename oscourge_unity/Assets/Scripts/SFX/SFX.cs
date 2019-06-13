using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{

public AudioSource jump1,jump2;
public AudioSource run;
public AudioSource explosion;
public AudioSource hurt;
public AudioSource teleport;



public void JumpSound1(){
	jump1.Play();
}

public void JumpSound2(){
	jump2.Play();
}


public void RunSound(){
	if(!run.isPlaying){
	run.Play();
}
}

public void RunStop(){
	run.Stop();
}

public void ExplosionSound(){
	explosion.Play();
}

public void HurtSound(){
	hurt.Play();
}

public void TeleportSound(){
	teleport.Play();
}

 
}
