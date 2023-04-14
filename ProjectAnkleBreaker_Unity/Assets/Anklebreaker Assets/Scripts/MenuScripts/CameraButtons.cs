using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtons : MonoBehaviour
{



    public CamSpotInfo[] spots;
    [Range(0.1f, 3.0f)] public float animSpeed = 0.3f;
    protected uint idx = 0;
    protected float t = 0.0f;

    void Update()
    {
        Transform cam = Camera.main.transform;
        Vector3 dir_target = spots[idx].target.position - cam.position;
        Quaternion roti = Quaternion.LookRotation(dir_target);
        cam.position = Vector3.Lerp(cam.position, spots[idx].transform.position, t);
        cam.rotation = Quaternion.Slerp(cam.rotation, roti, t);
        t += Time.deltaTime * animSpeed;
    }
    void OnGUI()
    {
        Rect rect = new Rect(10, 70, 200, 30);
        for (uint i = 0; i < spots.Length; ++i)
        {
            if (spots[i].showGUI)
            {
                if (GUI.Button(rect, "switch to " + spots[i].name))
                {
                    idx = i;
                    t = 0.0f;
                }
                rect.y += rect.height + 5;
            }
        }
    }
}

[System.Serializable]
public class CamSpotInfo
{
    public Transform transform;
    public Transform target;
    public string name;
    public bool showGUI = true;


    
}
