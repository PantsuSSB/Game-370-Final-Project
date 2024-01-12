using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControler : MonoBehaviour
{
    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float crouchSpeed;

    float currentMovementSpeed;

    float currentLookRotationY;

    [SerializeField]
    float jumpHight;

    [SerializeField]
    float jumpAirTime;

    [SerializeField]
    float playerYVelocity;

    [SerializeField]
    float gravity;

    float jumpSpeed;

    GameObject ladder;
    GameObject ladderParent;

    [SerializeField]
    Vector2 lookRotationSpeed;
    [SerializeField]
    Vector2 lookRotation;


    Transform camera;
    CharacterController characterController;
    PlayerInputManager inputManager;
    PlayerCollisionManager collisionManager;

    [SerializeField]
    MovementStates currentMovementState;
    public enum MovementStates
    {
        idle,
        walking,
        crouching,
        jumping,
        aiming,
        grabingLadder,
        climbing,
    }


    // Start is called before the first frame update
    void Start()
    {
        gravity = jumpHight / 2 / (jumpAirTime * jumpAirTime);
        jumpSpeed = Mathf.Sqrt(2 * jumpHight * gravity);
        camera = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<PlayerInputManager>();
        collisionManager = GetComponent<PlayerCollisionManager>();
        currentMovementSpeed = walkSpeed;
        LadderControler.LadderInteracted += SetLadderObject;
    }

    // Update is called once per frame
    void Update()
    {
        SetGravity();
        SetPlayerJump();
        SetCrouchAndSpeed();
        if(currentMovementState != MovementStates.grabingLadder && currentMovementState != MovementStates.climbing)
        {
            MovePlayer();
            RotateCameraXPlayreY();
        }


        else { LadderMovementControler(); }
        
    }

    void SetLadderObject(GameObject _ladder, GameObject _ladderParent)
    {
        if(currentMovementState != MovementStates.grabingLadder && currentMovementState != MovementStates.climbing && _ladder != null)
        {
            ladder = _ladder;
            ladderParent = _ladderParent;
            currentMovementState = MovementStates.grabingLadder;
            camera.rotation = transform.rotation;
        }

        else 
        {
            currentMovementState = MovementStates.idle;
            camera.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void LadderMovementControler()
    {
        if(currentMovementState == MovementStates.grabingLadder)
        {
            //camera.transform.LookAt(new Vector3(ladder.transform.position.x, camera.position.y, ladder.transform.position.z));
            //camera.transform.rotation = Quaternion.Euler(new Vector3(ladder.transform.position.x, camera.position.y, ladder.transform.position.z) - transform.position);
            camera.transform.LookAt(new Vector3(ladderParent.transform.position.x, camera.position.y, ladderParent.transform.position.z));

            transform.rotation = Quaternion.Euler(transform.position - new Vector3(ladder.transform.position.x, camera.position.y, ladder.transform.position.z)  );
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ladder.transform.position.x, transform.position.y, ladder.transform.position.z), crouchSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, new Vector3(ladder.transform.position.x, transform.position.y, ladder.transform.position.z)) < 0.01)
            {
                currentMovementState = MovementStates.climbing;
                
            }
        }

        else if(currentMovementState == MovementStates.climbing)
        {
            characterController.Move((Vector3.up * inputManager.DirectionalInput.y * currentMovementSpeed) * Time.deltaTime);
        }
    }

    void MovePlayer()
    {
        Vector3 _yVelocity = Vector3.up * playerYVelocity;
        Vector3 _moveDirection = (transform.forward * inputManager.DirectionalInput.y + transform.right * inputManager.DirectionalInput.x);
        characterController.Move((_moveDirection * currentMovementSpeed + _yVelocity) * Time.deltaTime);
        if (collisionManager.PlayerIsGrounded && inputManager.DirectionalInput == Vector2.zero && currentMovementState != 
            MovementStates.crouching && currentMovementState != MovementStates.grabingLadder && currentMovementState !=
            MovementStates.climbing)
        {
            currentMovementState = MovementStates.idle; 
        }
        else if (collisionManager.PlayerIsGrounded && inputManager.DirectionalInput != Vector2.zero && currentMovementState !=
            MovementStates.crouching && currentMovementState != MovementStates.grabingLadder && currentMovementState !=
            MovementStates.climbing) 
        {
            currentMovementState = MovementStates.walking; 
        }
    }


    void SetGravity()
    {


        if (!collisionManager.PlayerIsGrounded && currentMovementState != MovementStates.grabingLadder && currentMovementState != MovementStates.climbing)
        {
            playerYVelocity -= gravity * Time.deltaTime;
            
        }
    }

    void SetPlayerJump()
    {
        if (inputManager.JumpPressed && collisionManager.PlayerIsGrounded && currentMovementState != MovementStates.grabingLadder
            && currentMovementState != MovementStates.climbing && currentMovementState != MovementStates.crouching)
        {

            currentMovementState = MovementStates.jumping;
            playerYVelocity = jumpSpeed;
        }
    }

    void SetCrouchAndSpeed()
    {
        if (inputManager.CrouchPressed && currentMovementState != MovementStates.crouching)
        {
            currentMovementState = MovementStates.crouching;
            currentMovementSpeed = crouchSpeed;
            characterController.height = characterController.height / 2;
            camera.position = new Vector3(camera.position.x, camera.position.y - .5f, camera.position.z);
            //transform.localScale = new Vector3(1, .5f, 1);
        }

        else if (inputManager.CrouchPressed && currentMovementState == MovementStates.crouching || 
            inputManager.JumpPressed && currentMovementState == MovementStates.crouching)
        {
            currentMovementState = MovementStates.idle;
            currentMovementSpeed = walkSpeed;
            characterController.height = characterController.height * 2;
            camera.position = new Vector3(camera.position.x, camera.position.y + .5f, camera.position.z);
            //transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void RotateCameraXPlayreY()
    {
        lookRotation.x = inputManager.MouseMovement.x * lookRotationSpeed.x * Time.deltaTime;
        lookRotation.y = inputManager.MouseMovement.y * -1 *  lookRotationSpeed.y * Time.deltaTime;
        currentLookRotationY += lookRotation.y;
        currentLookRotationY = Mathf.Clamp(currentLookRotationY, -90, 90);

        if (currentMovementState != MovementStates.grabingLadder || currentMovementState != MovementStates.climbing)
        {
            lookRotation.x = inputManager.MouseMovement.x * lookRotationSpeed.x * Time.deltaTime;
            lookRotation.y = inputManager.MouseMovement.y * -1 * lookRotationSpeed.y * Time.deltaTime;
            currentLookRotationY += lookRotation.y;
            currentLookRotationY = Mathf.Clamp(currentLookRotationY, -90, 90);
            camera.transform.localRotation = Quaternion.Euler(currentLookRotationY, 0f, 0f);
            transform.Rotate(Vector3.up * lookRotation.x);
            camera.transform.localRotation = Quaternion.Euler(currentLookRotationY, 0f, 0f);
            transform.Rotate(Vector3.up * lookRotation.x);
        }
    }
}
