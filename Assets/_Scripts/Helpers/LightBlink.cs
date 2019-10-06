using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    [SerializeField] private Animator animate;
    [SerializeField] private float speed;
    private float time;
    private bool isOn = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleLight(){
        isOn = !isOn;

        animate.SetBool("isOn", isOn);
    }
}
