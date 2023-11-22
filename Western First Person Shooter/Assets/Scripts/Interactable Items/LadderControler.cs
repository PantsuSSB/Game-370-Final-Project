using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderControler : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private bool playerIsChild;
    bool canNotInteract;
    float offsetFromLadder;

    public delegate void LadderInteract(GameObject gameObject, GameObject gameObject2);
    public static event LadderInteract LadderInteracted;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //AtachToLadder();
        //DetachFromLadder();
        //canNotInteract = false;
    }

    void AtachToLadder()
    {
        if (player != null && !playerIsChild && !canNotInteract && Input.GetKeyDown("e"))
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

    public void Interact()
    {
        LadderInteracted?.Invoke(this.gameObject ,transform.parent.gameObject);
        //private void OnTriggerEnter(Collider other)
        //{
        //    if(other.gameObject.tag == "Player")
        //    {
        //        player = other.gameObject;
        //    }
        //}

        


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LadderInteracted?.Invoke(null,null);

        }
    }
}