using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKillPlayer : MonoBehaviour
{
    [SerializeField]
    LayerMask player;
    [SerializeField]
    bool didPlayerTouchGround;
    Vector3 checkBoxSize;
    BoxCollider collider;
    
    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        checkBoxSize = new Vector3(collider.size.x, 0.002f, collider.size.z);
    }

    private void FixedUpdate()
    {
        CheckIfPlayerTouchedGround();
    }

    void CheckIfPlayerTouchedGround()
    {

        didPlayerTouchGround = Physics.CheckBox(transform.position, checkBoxSize, Quaternion.identity, player, QueryTriggerInteraction.Collide);

        if (didPlayerTouchGround) 
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PlayerTouchedDeath");

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, checkBoxSize / 2);
    }
}
