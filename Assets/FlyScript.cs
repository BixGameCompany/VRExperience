using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FlyScript : MonoBehaviour
{
    InputManager input;
    public Transform RTrackedController;
    public Transform LTrackedController;
    //public bool isFlying;
    public float flyForce = 100f;
    public float maxVelcoity = 25f;

    public GameObject[] Rockets;

    Rigidbody player;
    
    double RtriggerValue;
    double LtriggerValue;

    private void Awake() {
        input = new InputManager();
        //input.Flying.Fly.performed += ctx => isFlying = true;
        //input.Flying.Fly.canceled += ctx => isFlying = false;


        input.Flying.RFly.performed += ctx => RtriggerValue = Math.Round((double)ctx.ReadValue<float>(),2);
        input.Flying.LFly.performed += ctx => LtriggerValue = Math.Round((double)ctx.ReadValue<float>(),2);

    }
    private void Start() {
        player = GetComponent<Rigidbody>();
    }


    private void Update() {
        Rockets[0].transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = (float)LtriggerValue;
        Rockets[1].transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = (float)RtriggerValue;

    }

    private void FixedUpdate() {



        if(RtriggerValue >= 0.5){
            //Debug.Log("Flying Right");
            player.AddForce(RTrackedController.forward * flyForce * Time.deltaTime,ForceMode.Acceleration);
            Rockets[1].transform.GetChild(1).gameObject.SetActive(true);
        }else if(RtriggerValue <= 0.5){
            Rockets[1].transform.GetChild(1).gameObject.SetActive(false);

        }
        if(LtriggerValue >= 0.5){
            //Debug.Log("Flying Left");
            player.AddForce(LTrackedController.forward * flyForce * Time.deltaTime,ForceMode.Acceleration);
            Rockets[0].transform.GetChild(1).gameObject.SetActive(true);

        }else if(LtriggerValue <= 0.5){
            Rockets[0].transform.GetChild(1).gameObject.SetActive(false);

        }
        
        player.velocity = Vector3.ClampMagnitude(player.velocity, maxVelcoity);
    }
    public void OnEnable(){input.Enable();}
    public void OnDisable(){input.Disable();}
}
