using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slamDunkAreaBehavior : MonoBehaviour
{
    private GameObject player;
    private PlayerStateManager currentPlayer_psm;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
        {
            player = other.gameObject;
            currentPlayer_psm = player.GetComponent<PlayerStateManager>();
            currentPlayer_psm.isInSDarea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
        {
            player = other.gameObject;
            currentPlayer_psm = player.GetComponent<PlayerStateManager>();
            currentPlayer_psm.isInSDarea = false;
        }
    }
}
