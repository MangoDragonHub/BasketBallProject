using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Made_up_person : Stat_Dict
{

    public string name = "no name";
    public int[] values = { 0,0,0,0,0,0,0,0};


    // Start is called before the first frame update
    void Start()
    {
        player[0] = name;
        player["name"] = values[0];
        player["dunking"] = values[1];
        player["three point"] = values[2];
        player["free throw"] = values[3];
        player["offensive rebound"] = values[4];
        player["deffensive rebound"] = values[5];
        player["assist"] = values[6];
        player["turnover"] = values[7];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
