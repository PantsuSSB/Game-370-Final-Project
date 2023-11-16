using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderControler : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private bool playerIsChild;
    bool canNotInteract;
    float offsetFromLadder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        AtachToLadder();
        DetachFromLadder();
        canNotInteract = false;
    }

    void AtachToLadder()
    {
        if (player != null && !playerIsChild && !canNotInteract && Input.GetKeyDown("e") )
        {
            player.transform.rotation = transform.rotation;
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z) + transform.forward * offsetFromLadder;
            player.transform.SetParent(transform);
            playerIsChild = true;
            canNotInteract = true;
        }
    }

    void DetachFromLadder()
    {
        if (player != null && playerIsChild && !canNotInteract && Input.GetKeyDown("e"))
        {
            //player.transform.rotation = transform.rotation;
            //player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z) + transform.forward * offsetFromLadder;
            player.transform.parent = null;
            playerIsChild = false;
            canNotInteract = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DetachFromLadder();
            player = null;
            
        }
    }
}
