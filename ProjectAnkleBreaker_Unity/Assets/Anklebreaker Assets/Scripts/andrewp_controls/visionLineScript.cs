using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionLineScript : MonoBehaviour
{
    private Transform hoop_tr;
    public bool startCalculating = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tmp_go_hoop = GameObject.Find("Basket_Target");
        hoop_tr = tmp_go_hoop.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(startCalculating == true)
        {
            //Calculate distance here
            float dist = Vector3.Distance(hoop_tr.position, this.transform.position);
            print("Distance to hoop: " + dist);
        }
    }
}
