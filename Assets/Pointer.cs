using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    RaycastHit hit;
    LineRenderer lr;

    InputManager input;

    bool gripPressed;
    private void Awake() {
        input = new InputManager();

        input.Flying.Grip.performed += ctx => gripPressed = true;
        input.Flying.Grip.canceled += ctx => gripPressed = false;

    }
    private void Start() {
        lr = GetComponent<LineRenderer>();
    }
    private void Update() {
    if(lr.enabled){

        
        if(Physics.Raycast(transform.position,transform.forward,out hit, 25f)&& gripPressed){
            lr.positionCount = 2;
            lr.SetPosition(0,transform.position);
            lr.SetPosition(1,hit.point);

        }else if(gripPressed){
            lr.positionCount = 2;
            Vector3 endPos = transform.forward * 10f;
            lr.SetPosition(0,transform.position);
            lr.SetPosition(1,endPos);
        }else{
            lr.positionCount = 0;
        }
    }
        
    }
    public void OnEnable(){input.Enable();}
    public void OnDisable(){input.Disable();}
}
