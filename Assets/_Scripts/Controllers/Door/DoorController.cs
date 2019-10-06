using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animate;
    [SerializeField] private string opener = "doorOpen";
    private bool isOpen = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        animate = GetComponent<Animator>();
        animate.SetBool(opener, isOpen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        isOpen = true;
        
        if(other.gameObject.tag == "Player"){
            animate.SetBool(opener, isOpen);
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        isOpen = false;
        
        if(other.gameObject.tag == "Player"){
            animate.SetBool(opener, isOpen);
        }
    }
}
