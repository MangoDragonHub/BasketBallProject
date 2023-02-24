using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepPlayback : MonoBehaviour
{
    public AudioSource[] FootstepAudioClips;
    private SphereCollider player_Lfoot;
    private SphereCollider player_Rfoot;
    private BoxCollider planeCollider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject L_foot_object = GameObject.Find("LeftFootCollider");
        GameObject R_foot_object = GameObject.Find("RightFootCollider");
        player_Lfoot = L_foot_object.GetComponent<SphereCollider>();
        player_Rfoot = R_foot_object.GetComponent<SphereCollider>();
        planeCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision == player_Lfoot)
        {
            var index = Random.Range(0, FootstepAudioClips.Length); //Selects a random sound effect in the array to play, and it will play that sound.
            FootstepAudioClips[index].Play(0);
        }

        if (collision == player_Rfoot)
        {
            var index = Random.Range(0, FootstepAudioClips.Length);
            FootstepAudioClips[index].Play(0);
        }
    }
}
