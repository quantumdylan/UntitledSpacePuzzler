using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = playerCamera.rotation;
    }
}
