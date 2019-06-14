using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch_control : MonoBehaviour {
    public Animator animator;
    int i = 0;

    void Update() {
        int r = Random.Range(0, 3);
        Debug.Log(r);
        animator.SetInteger("num_anim", r);
        i = 0;
    }
}
