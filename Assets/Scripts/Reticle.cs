using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Transform PlayerCamera;
    public Transform Controller;
    
    public float MaxDist = 50f;

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        RaycastHit hit;

        if(Physics.Raycast(Controller.position,Controller.forward,out hit, MaxDist))
        {
            transform.position = hit.point;
        }else{
            GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

        transform.LookAt(PlayerCamera.position);

    }
}
