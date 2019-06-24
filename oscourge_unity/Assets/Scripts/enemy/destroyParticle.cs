using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyParticle : MonoBehaviour
{
    private float timeLeft = 3f;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            Destroy(this.gameObject);


    }

    }

}