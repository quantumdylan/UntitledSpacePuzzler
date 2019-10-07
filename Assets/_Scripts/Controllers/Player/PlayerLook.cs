using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity = 150;
    [SerializeField] private float kioskZoom = 5;

    [SerializeField] private Transform playerBody;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;

    [SerializeField] private Vector2 xClampMinMax = new Vector2(-90f, 90f);

    private float xAxisClamp;

    private bool isControl = true;
    private bool movingIn = false;
    private bool inScreen = false;
    public Transform idle;

    private void Awake(){
        LockCursor();
        xAxisClamp = 0;

        playerBody = GameObject.FindWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
        idle = GetComponent<Transform>();
    }

    private void Update(){
        if(isControl)
            CameraRotation();
    }

    public void LockCursor(){
        if(isControl){
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void CameraRotation(){
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;
        if(xAxisClamp > xClampMinMax.y){
            xAxisClamp = 90.0f;
            mouseY = 0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if(xAxisClamp < xClampMinMax.x){
            xAxisClamp = -90.0f;
            mouseY = 0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value){
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    public void moveToTarget(Transform target, Transform look){
        isControl = false;
        movingIn = true;
        transform.parent = null;
        
        StartCoroutine(cameraIn(target, look));
    }

    public void returnToHome(){
        StartCoroutine(cameraOut());
    }

    public void takeControl(){
        isControl = false;
    }

    public void giveControl(){
        if(!inScreen)
            isControl = true;
    }

    private IEnumerator cameraIn(Transform target, Transform look){
        player.GetComponent<PlayerMove>().takeControl();

        Cursor.lockState = CursorLockMode.None;

        while(((Vector3.Distance(transform.position, target.transform.position) > 0.001) || 
                Quaternion.Angle(transform.rotation, Quaternion.LookRotation(look.transform.position - transform.position)) > 0.01) && 
                !isControl && movingIn){
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * kioskZoom);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look.transform.position - transform.position), Time.deltaTime * kioskZoom);

            yield return null;
        }

        transform.position = target.transform.position;
        transform.rotation.SetLookRotation(look.transform.position);
        player.GetComponent<PlayerMove>().giveControl();

        inScreen = true;
    }

    private IEnumerator cameraOut(){
        player.GetComponent<PlayerMove>().takeControl();
        Cursor.lockState = CursorLockMode.Locked;
        movingIn = false;

        while(Vector3.Distance(transform.position, playerBody.transform.position + offset) > 0.01 && !movingIn){
            transform.position = Vector3.Lerp(transform.position, playerBody.transform.position + offset, Time.deltaTime * kioskZoom);
            transform.rotation = playerBody.transform.rotation;

            yield return null;
        }

        transform.parent = playerBody;
        isControl = true;
        transform.position = playerBody.transform.position + offset;
        player.GetComponent<PlayerMove>().giveControl();
        inScreen = false;
    }
}
