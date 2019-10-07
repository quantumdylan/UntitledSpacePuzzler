using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private float lightBrightness = 1;
    [SerializeField] private float timeOut = 100.0f;
    
    private bool menuCoolDown = false;
    private bool lightCoolDown = false;
    private bool lightOn = false;
    private float menuTime = 0;
    private float lightTime = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        flashlight = GameObject.FindGameObjectWithTag("Flashlight");

        flashlight.GetComponent<Light>().intensity = (lightOn) ? lightBrightness : 0;
    }

    // Update is called once per frame
    // TODO: Make an actual pause screen function, and make sure to take away the ability to look when the game is paused/mouse loses focus
    void Update()
    {
        if(Input.GetKey(KeyCode.F)){
            
        }

        if(Input.GetKey(KeyCode.Escape) && !menuCoolDown){
            pauseMenu.GetComponent<Canvas>().enabled = !pauseMenu.GetComponent<Canvas>().enabled; // toggle state
            
            if(!pauseMenu.GetComponent<Canvas>().enabled){
                cam.GetComponent<PlayerLook>().LockCursor();
                cam.GetComponent<PlayerLook>().giveControl();
            }
            else{
                Cursor.visible = true;
                cam.GetComponent<PlayerLook>().takeControl();
            }

            menuCoolDown = true;
        }
        if(Input.GetKey(KeyCode.F) && !lightCoolDown){
            lightOn = !lightOn; // toggle state

            flashlight.GetComponent<Light>().intensity = (lightOn) ? lightBrightness : 0; // assign intensity

            lightCoolDown = true;
        }

        if(menuCoolDown){
            if(menuTime < timeOut)
                menuTime += Time.deltaTime;
            else{
                menuCoolDown = false;
                menuTime = 0;
            }
        }
        if(lightCoolDown){
            if(lightTime < timeOut)
                lightTime += Time.deltaTime;
            else{
                lightCoolDown = false;
                lightTime = 0;
            }
        }
    }

    public void exitGame(){
        Application.Quit();
    }
}
