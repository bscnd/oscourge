using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch_control : MonoBehaviour {
    public Animator animator;

    void Update() {
        int r = Random.Range(0, 3);
        animator.SetInteger("num_anim", r);
    }
}
