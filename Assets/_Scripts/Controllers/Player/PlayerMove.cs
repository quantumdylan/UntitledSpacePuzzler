using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName, verticalInputName;
    private float speed;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private CharacterController charController;

    [SerializeField] private float walkSpeed, runSpeed;
    [SerializeField] private float runBuildUpSpeed;
    [SerializeField] private KeyCode runKey;

    [SerializeField] private AnimationCurve jumpFalloff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    private bool isJumping;
    public bool isControl = true;

    // Start is called before the first frame update
    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if(isControl)
            PlayerMovement();
    }

    private void PlayerMovement(){
        SetMovementSpeed();
        
        float vertInput = Input.GetAxis(verticalInputName);
        float horInput = Input.GetAxis(horizontalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 lateralMovement = transform.right * horInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + lateralMovement, 1f) * speed);

        if((vertInput != 0 || horInput != 0) && OnSlope()) charController.Move(Vector3.down * charController.height/2 * slopeForce * Time.deltaTime);
            
        
        JumpInput();
    }

    private void SetMovementSpeed(){
        if(Input.GetKey(runKey)){ 
            speed = Mathf.Lerp(speed, runSpeed, Time.deltaTime * runBuildUpSpeed); 
        }
        else {
            speed = Mathf.Lerp(speed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
        }
        
        if(Mathf.Approximately(runSpeed, speed)) speed = runSpeed;
        if(Mathf.Approximately(walkSpeed, speed)) speed = walkSpeed;
    }

    private bool OnSlope(){
        if(isJumping)
            return false;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, charController.height/2 * slopeForceRayLength)){
            if(hit.normal != Vector3.up) return true;
        }

        return false;
    }

    private void JumpInput(){
        if(Input.GetKeyDown(jumpKey) && !isJumping){
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent(){
        float slopeLimitInitial = charController.slopeLimit;
        charController.slopeLimit = 90.0f;
        float timeInAir = 0f;

        do{
            float jumpForce = jumpFalloff.Evaluate(timeInAir);
            
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;

            yield return null;
        } while(!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above); // handles floor and ceiling collisions

        charController.slopeLimit = slopeLimitInitial;
        isJumping = false;
    }

    public void takeControl(){
        isControl = false;
    }
    public void giveControl(){
        isControl = true;
    }
}
