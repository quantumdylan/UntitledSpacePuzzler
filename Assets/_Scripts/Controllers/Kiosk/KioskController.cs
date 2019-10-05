using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KioskController : MonoBehaviour
{
    [SerializeField] private Transform target; // camera target
    [SerializeField] private Transform lookAt; // target for camera to lookat

    private Camera cam;
    private GameObject player;

    public bool kioskEngaged;


    // Start is called before the first frame update
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");

        kioskEngaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Debug.Log("AH FUCK DON'T TOUCH ME");
            cam.GetComponent<PlayerLook>().moveToTarget(target, lookAt);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Debug.Log("THAT'S RIGHT WALK AWAY");
            cam.GetComponent<PlayerLook>().returnToHome();
        }
    }
}
