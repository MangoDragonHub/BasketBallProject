using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class buttonSetting_forTitle : MonoBehaviour
{
    private PlayerInput pl_input;
    private List<string> konamiCodeInputs;
    private List<string> userButtonInputs;
    [SerializeField] private AudioSource success_sound;

    // Start is called before the first frame update
    void Start()
    {
        pl_input = GetComponent<PlayerInput>();
        konamiCodeInputs = new List<string>(){"u", "u", "d", "d", "l", "r", "l", "r", "b", "a"};
        userButtonInputs = new List<string>();
    }

    void Update()
    {
        //Debug.Log($"kc: {konamiCodeInputs.Count}, user: {userButtonInputs.Count}");
    }

    private void checkListMatches()
    {
        if(userButtonInputs.SequenceEqual(konamiCodeInputs))
        {
            //play the success sound, turn on granny mode.
            success_sound.Play();
            Debug.Log("K O N A M I   C O D E   A C T I V A T E D .");
        }
    }

    void OnKc_up()
    {
        userButtonInputs.Add("u");
    }

    void OnKc_down()
    {
        userButtonInputs.Add("d");
    }

    void OnKc_left()
    {
        userButtonInputs.Add("l");
    }

    void OnKc_right()
    {
        userButtonInputs.Add("r");
    }

    void OnKc_a()
    {
        userButtonInputs.Add("a");
        checkListMatches();
    }

    void OnKc_b()
    {
        userButtonInputs.Add("b");
    }

    void OnRedo_kc()
    {
        userButtonInputs.Clear();
    }
}
