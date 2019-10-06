using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ClipLauncher : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter ev;
    public bool launch = false;
    public bool lastLaunch = false;
    // Start is called before the first frame update
    void Awake()
    {
        ev = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(launch){
            ev.Play();
            launch = false;
        }
    }

    public void launchClip(){
        ev.Play();
    }

    public void stopClip(){
        ev.Stop();
    }
}
