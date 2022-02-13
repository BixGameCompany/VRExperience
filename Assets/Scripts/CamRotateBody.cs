using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotateBody : MonoBehaviour
{
    public Transform Camera;

    private void Update() {
        //Debug.Log(Camera.localRotation);
        transform.eulerAngles = new Vector3(0,Camera.eulerAngles.y,0);
        transform.position = new Vector3(Camera.position.x,transform.position.y,Camera.position.z);
    }
}
