using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatesManager : MonoBehaviour
{
    playerMovemetStates currentMovemetState;
    public playerMovemetStates CurrentMovementState { get { return currentMovemetState; } }
    public enum playerMovemetStates
    {
        idle,
        walking,
        jumping,
        crouching,
        climbing,
    }

    PlayerInputManager inputManager;
    PlayerCollisionManager collisionManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        collisionManager = GetComponent<PlayerCollisionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SetIdleState();
        SetWalkingState();

    }

   void SetIdleState()
    {
        if(collisionManager.PlayerIsGrounded && inputManager.DirectionalInput == Vector2.zero 
            && currentMovemetState != playerMovemetStates.idle && currentMovemetState != playerMovemetStates.crouching)
        {
            currentMovemetState = playerMovemetStates.idle;
        }
    }

   void SetWalkingState()
    {
        if(collisionManager.PlayerIsGrounded && inputManager.DirectionalInput != Vector2.zero && currentMovemetState != playerMovemetStates.walking)
        {
            currentMovemetState = playerMovemetStates.walking;
        }
    }


}
