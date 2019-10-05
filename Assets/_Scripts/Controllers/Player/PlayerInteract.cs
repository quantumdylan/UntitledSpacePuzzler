using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject cam;
    [SerializeField] private float timeOut = 100.0f;
    private bool countDown = false;
    private float time = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && !countDown){
            pauseMenu.GetComponent<Canvas>().enabled = !pauseMenu.GetComponent<Canvas>().enabled; // toggle state
            
            if(!pauseMenu.GetComponent<Canvas>().enabled){
                cam.GetComponent<PlayerLook>().LockCursor();
            }
            else{
                Cursor.visible = true;
            }

            countDown = true;
        }

        if(countDown){
            if(time < timeOut)
                time += Time.deltaTime;
            else{
                countDown = false;
                time = 0;
            }
        }
    }

    public void exitGame(){
        Application.Quit();
    }
}
