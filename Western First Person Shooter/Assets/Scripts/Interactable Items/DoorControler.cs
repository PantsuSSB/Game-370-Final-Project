using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour, IInteractable
{
    [SerializeField]
    bool doorOpen = false;
    Transform player;
    Animator animator;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("CloseDoorIdle"))
        {
            doorOpen = false;
        }

        else { doorOpen = true; ; }
    }

    //This is used by the Player interaction Manager
    public void Interact()
    {
        if (!doorOpen)
        {
            if(player.position.z < transform.position.z) 
            {
                animator.SetBool("DoorOpenForward", true);
                animator.SetTrigger("DoorInteract");
                audioSource.Play();
            }

            else
            {
                animator.SetBool("DoorOpenForward", false);
                animator.SetTrigger("DoorInteract");
                audioSource.Play();
            }
        }

        else 
        {
            animator.SetTrigger("DoorInteract");
            audioSource.Play();
        }
    }

        //Debug.Log(gameObject.name + " opened");
    
}
