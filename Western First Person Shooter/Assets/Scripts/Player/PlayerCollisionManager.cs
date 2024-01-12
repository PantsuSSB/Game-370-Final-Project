using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField]
    bool playerIsGrounded;

    [SerializeField]
    bool playerTouchedDeath;

    public bool PlayerIsGrounded { get { return playerIsGrounded; } }

    [SerializeField]
    float groundCheckHight;

    [SerializeField]
    float groundCheckSize;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    LayerMask deathLayer;

    [SerializeField]
    CharacterController characterController;

    public delegate void KillPlayerOnTouch();
    public static event KillPlayerOnTouch PlayerTouchedFloor;

    void Start()
    {
        //characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        CheckIfPlayerIsGrounded();
        CheckIfPlayerTouchedDeathLayer();
    }

    void CheckIfPlayerIsGrounded()
    {
        Vector3 _groundCheckCenter = new Vector3(transform.position.x, transform.position.y - (characterController.height / 2) + groundCheckHight, transform.position.z);
        Vector3 _groundCheckSize = new Vector3(transform.lossyScale.x - 0.05f, groundCheckSize, transform.lossyScale.z - 0.05f);
        
        playerIsGrounded = Physics.CheckSphere(_groundCheckCenter, groundCheckSize, groundLayer);
    }

    void CheckIfPlayerTouchedDeathLayer()
    {
        Vector3 _groundCheckCenter = new Vector3(transform.position.x, transform.position.y - (characterController.height / 2) + groundCheckHight, transform.position.z);
        Vector3 _groundCheckSize = new Vector3(transform.lossyScale.x - 0.05f, groundCheckSize, transform.lossyScale.z - 0.05f);

         playerTouchedDeath = Physics.CheckSphere(_groundCheckCenter, groundCheckSize, deathLayer);
        if (playerTouchedDeath)
        {
            Debug.Log("PlayerTouchedDeath");
            PlayerTouchedFloor?.Invoke();
        }
    }

    //This draws the sphere that represents the check for if the player is grounded 
    //If the player is grounded the sphere will draw white.
    //If the player is not grounded the sphear will draw red
    private void OnDrawGizmos()
    {
        Vector3 _groundCheckCenter = new Vector3(transform.position.x, transform.position.y - (characterController.height / 2) + groundCheckHight, transform.position.z);
        Vector3 _groundCheckSize = new Vector3(transform.lossyScale.x - 0.05f, groundCheckSize * 2, transform.lossyScale.z - 0.05f);
        if (playerIsGrounded)
        {
            Gizmos.color = Color.white;
        }

        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(_groundCheckCenter, groundCheckSize);
    }
}
