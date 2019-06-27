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
    void FixedUpdate()
    {

            Vector3 temp = bigBoy.transform.position;
            temp.y = this.transform.position.y;
            temp.z = this.transform.position.z;
            temp.x = Mathf.RoundToInt(temp.x);
            this.transform.position = temp + new Vector3(offset, 0, 0);
       
    }
}
