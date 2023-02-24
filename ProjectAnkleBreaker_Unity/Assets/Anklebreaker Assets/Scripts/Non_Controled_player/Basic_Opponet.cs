using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Opponet : MonoBehaviour
{

    public float distance_from_player = 2;
    public float turn_speed = 60;
    public float distance_y_axis = 2.5f;

    private GameObject player;

    private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("basket_ball");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Check_if_player_has_ball() == true)
        {
            Harass_noun(player.transform);
            Look_at_area(player.transform);
        }
        else if(Check_if_player_has_ball() == false)
        {
            Harass_noun(ball.transform);
            Look_at_area(ball.transform);

        }
    }

    bool Check_if_player_has_ball()
    {
        //make a public get player in the basketballHandler

        return false;
    }

    void Harass_noun(Transform noun)
    {
        this.transform.position =new Vector3((noun.transform.position.x + distance_from_player), distance_y_axis, noun.transform.position.z);
    }

    void Look_at_area(Transform dirction_goto)
    {
        Vector3 director = new Vector3(dirction_goto.transform.position.x, distance_y_axis, dirction_goto.transform.position.z) - new Vector3(this.transform.position.x, distance_y_axis, this.transform.position.z);
        Quaternion rotator = Quaternion.LookRotation(director);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotator, turn_speed * Time.deltaTime);
    }
}
