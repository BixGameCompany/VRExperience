using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ODMScript : MonoBehaviour
{
    InputManager input;
    public Transform RTrackedController;
    public Transform LTrackedController;
    public float maxVelcoity = 25f;

    public GameObject[] Grapples;
    public SpringJoint[] Joints;

    Vector3[] LastPoints = new Vector3[2];
    public Transform[] LRPoints = new Transform[2];

    Rigidbody player;
    public LineRenderer[] GrappleLines = new LineRenderer[2];

    double RPull;
    double LPull;

    public float[] pullPower = new float[2];
    private void Awake() {
        input = new InputManager();

        input.ODM.RGrapple.performed += ctx => fireRightGrapple();
        input.ODM.LGrapple.performed += ctx => fireLeftGrapple();


        input.ODM.RPull.performed += ctx => RPull = Math.Round((double)ctx.ReadValue<float>(),2);
        input.ODM.LPull.performed += ctx => LPull = Math.Round((double)ctx.ReadValue<float>(),2);

        input.ODM.TestMove.performed += ctx => Joints[0].spring = 1;
        input.ODM.TestMove.canceled += ctx => Joints[0].spring = 0;

    }
    private void Start() {
        player = GetComponent<Rigidbody>();
        //GrappleLines[0] = GetComponent<LineRenderer>();
    }
    private void Update() {
        //pullPower[0] = Mathf.Lerp(pullPower[0],map((float)RPull,0,1,0,5),Time.deltaTime);
        //pullPower[1] = Mathf.Lerp(pullPower[1],map((float)LPull,0,1,0,5),Time.deltaTime);

        //pullPower[0] = Mathf.Round(pullPower[0]);
        //pullPower[1] = Mathf.Round(pullPower[1]);



    if(Joints[0] != null){
        pullPower[0] = Mathf.Lerp(pullPower[0],map((float)RPull,0,1,0,5),Time.deltaTime);

        if(LastPoints[0] != Vector3.zero){
            Joints[0].spring = pullPower[0];


            //Line Renderer
            GrappleLines[0].positionCount = 2;
            GrappleLines[0].SetPosition(0,LRPoints[0].position);
            GrappleLines[0].SetPosition(1,LastPoints[0]);

        }else {
            Joints[0].spring = 0;
            
            GrappleLines[0].positionCount = 0;
        }
    }else{
        //pullPower[0] = 0;
    }

    if(Joints[1] != null){
        pullPower[1] = Mathf.Lerp(pullPower[1],map((float)LPull,0,1,0,5),Time.deltaTime);

        if(LastPoints[1] != Vector3.zero){
            Joints[1].spring = pullPower[1];
            
            //Line Renderer
            GrappleLines[1].positionCount = 2;
            GrappleLines[1].SetPosition(0,LRPoints[1].position);
            GrappleLines[1].SetPosition(1,LastPoints[1]);

        }else {
            Joints[1].spring = 0;
            

           GrappleLines[1].positionCount = 0;
        }
    }else{
        //pullPower[1] = 0;
    }
    
    }
    
    private void FixedUpdate() {
        player.velocity = Vector3.ClampMagnitude(player.velocity, maxVelcoity);
    }
    void fireLeftGrapple()
    {
        RaycastHit hit;
        //Creates Grapple
        if(Physics.Raycast(Grapples[1].transform.position,Grapples[1].transform.forward,out hit,100f) && Joints[1] == null){
            Joints[1] = gameObject.AddComponent<SpringJoint>();
            Joints[1].autoConfigureConnectedAnchor = false;
            
            //Creates Grapple Point
            Joints[1].connectedAnchor = hit.point;
            LastPoints[1] = hit.point;
            Joints[1].damper = 2f;
        }
        //Destorys Grapple
        else if(Joints[1] != null){
            Destroy(Joints[1]);
            LastPoints[1] = Vector3.zero;
            Joints[1] = null;
        }

    }
    void fireRightGrapple()
    {
        
        RaycastHit hit;
        //Creates Grapple
        if(Physics.Raycast(Grapples[0].transform.position,Grapples[0].transform.forward,out hit,100f) && Joints[0] == null){
            Joints[0] = gameObject.AddComponent<SpringJoint>();

            Joints[0].autoConfigureConnectedAnchor = false;

            //Creates Grapple Point
            Joints[0].connectedAnchor = hit.point;
            LastPoints[0] = hit.point;

            Joints[0].damper = 2f;

        }
        //Destorys Grapple
        else if(Joints[0] != null){
            Destroy(Joints[0]);
            LastPoints[0] = Vector3.zero;
            Joints[0] = null;

        }

    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }
    public void OnEnable(){input.Enable();}
    public void OnDisable(){input.Disable();}
}
