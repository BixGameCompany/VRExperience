using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugControls : MonoBehaviour
{
    InputManager input;

    public FlyScript Player;
    public GameObject[] allCars;
    
    public GameObject ScreenText;
    public Text SliderText;

    bool carsEnabled = true;


    bool IncreasingVel;
    bool DecreasingVel;


    private void Awake() {
        input = new InputManager();

        input.Debug.ToggleCars.performed += ctx => ToggleAllCars();
        input.Debug.ToggleText.performed += ctx => ToggleText();

        input.Debug.IncreaseVelocity.performed += ctx => IncreasingVel = true;
        input.Debug.IncreaseVelocity.canceled += ctx => IncreasingVel = false; 
        
        input.Debug.DecreaseVelocity.performed += ctx => DecreasingVel = true;
        input.Debug.DecreaseVelocity.canceled += ctx => DecreasingVel = false; 

    }
    private void Update() {
            if(IncreasingVel && Player.maxVelcoity <= 50){
                Player.maxVelcoity = Mathf.Lerp(Player.maxVelcoity,Player.maxVelcoity+1,5 * Time.deltaTime);
            }
            else if(DecreasingVel){
                Player.maxVelcoity = Mathf.Lerp(Player.maxVelcoity,Player.maxVelcoity-1,5 * Time.deltaTime);
            }
            SliderText.text = $"Max Velocity: {Mathf.Round(Player.maxVelcoity)}MPH";
    }
    void ToggleAllCars(){
        if(carsEnabled){
            foreach(GameObject g in allCars){
                g.SetActive(false);
            }
            carsEnabled = false;
        }else{
            foreach(GameObject g in allCars){
                g.SetActive(true);
            }
            carsEnabled = true;

        }
    }
    void ToggleText()
    {
        ScreenText.SetActive(!ScreenText.active);
    }

    

    public void OnEnable(){input.Enable();}
    public void OnDisable(){input.Disable();}
}
