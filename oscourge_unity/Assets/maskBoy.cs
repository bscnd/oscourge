using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maskBoy : MonoBehaviour
{

    public GameObject bigBoy;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = bigBoy.transform.position+ new Vector3 (offset,0,0);
    }
}
