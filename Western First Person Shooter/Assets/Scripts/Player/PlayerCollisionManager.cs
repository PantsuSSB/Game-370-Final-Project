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

    public delegate void KillPlayerOnTouch();
    public static event KillPlayerOnTouch PlayerTouchedFloor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckIfPlayerIsGrounded();
        CheckIfPlayerTouchedDeathLayer();
    }

    void CheckIfPlayerIsGrounded()
    {
        Vector3 _groundCheckCenter = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y + groundCheckHight, transform.position.z);
        Vector3 _groundCheckSize = new Vector3(transform.lossyScale.x - 0.05f, groundCheckSize, transform.lossyScale.z - 0.05f);
        
        playerIsGrounded = Physics.CheckSphere(_groundCheckCenter, groundCheckSize, groundLayer);
    }

    void CheckIfPlayerTouchedDeathLayer()
    {
        Vector3 _groundCheckCenter = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y + groundCheckHight, transform.position.z);
        Vector3 _groundCheckSize = new Vector3(transform.lossyScale.x - 0.05f, groundCheckSize, transform.lossyScale.z - 0.05f);

         playerTouchedDeath = Physics.CheckSphere(_groundCheckCenter, groundCheckSize, deathLayer);
        if (playerTouchedDeath)
        {
            Debug.Log("PlayerTouchedDeath");
            PlayerTouchedFloor?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 _groundCheckCenter = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y + groundCheckHight, transform.position.z);
        Vector3 _groundCheckSize = new Vector3(transform.lossyScale.x - 0.05f, groundCheckSize * 2, transform.lossyScale.z - 0.05f);
        Gizmos.DrawWireSphere(_groundCheckCenter, groundCheckSize);
    }
}
