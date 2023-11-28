using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour, IInteractable
{
    [SerializeField]
    bool doorOpen = false;
    [SerializeField]
    bool doorLocked;
    Transform player;
    Animator animator;
    AudioSource audioSource;
    [SerializeField]
    GameObject doorLock;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if(doorLock != null) 
        {
            SetDoorLockValue(true);
            
            GameObject _lockTransform = Instantiate(doorLock, transform.GetChild(0));
            _lockTransform.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
        }
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
        if (!doorLocked)
        {
            if (!doorOpen)
            {
                if (player.position.z < transform.position.z)
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

        else
        {
            animator.SetTrigger("DoorInteract");
        }
    }

    public void SetDoorLockValue(bool _doorLockedValue) 
    { 
        doorLocked = _doorLockedValue;
        animator.SetBool("DoorLocked", _doorLockedValue);
    }
    
}