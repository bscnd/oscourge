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
        else {
            float r = Random.Range(0f, 3f);
            Debug.Log(r);
            animator.SetFloat("num_anim", r);
            i = 0;
        }
    }
}
