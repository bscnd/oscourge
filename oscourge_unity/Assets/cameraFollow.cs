using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject cam;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = cam.transform.position;
        
    }
}
