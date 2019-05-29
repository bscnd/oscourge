using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputValues {
    public InputValues (float h, bool j) {
        this.horizontal = h;
        this.jump = j;
    }

    public InputValues() {
        horizontal = 0;
        jump = false;
    }

    public float horizontal { get; set; }
    public bool jump { get; set; }
}
