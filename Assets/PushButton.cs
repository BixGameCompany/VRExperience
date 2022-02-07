using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public Collider[] ObjectsToIgnoreCol;
    void Start()
    {
        foreach(Collider Col in ObjectsToIgnoreCol){
            Physics.IgnoreCollision(GetComponent<Collider>(),Col);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
