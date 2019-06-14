using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch_control : MonoBehaviour {
    public Animator animator;
    int i = 0;

    void Update() {
        if (i < 200) {
            i++;
        }
        if(i==100) {   
            i = 0;
            changeAnim();
        }
    }


    void changeAnim(){

        float r = Random.Range(0f, 3f);
        animator.SetFloat("num_anim", r);

    }
}
