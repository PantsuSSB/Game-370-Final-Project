using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractControler : MonoBehaviour
{
    Camera camera;

   
    LayerMask interactableLayer;
    
    //[SerializeField]
    //string[] interactableLayerNames;
    //[SerializeField]
    //int[] interactableLayersValue;
    //int currentLayer;

    [SerializeField]
    float interactDetectionDistance;

    PlayerInputManager inputManager;
    PlayerMovementControler movementControler;
    [SerializeField]
    IInteractable currentInteractable = null;
    string interactableObjectName = null;

    void Start()
    {
        camera = Camera.main;
        interactableLayer = LayerMask.GetMask("Interactable");
        inputManager = GetComponent<PlayerInputManager>();
        movementControler = GetComponent<PlayerMovementControler>();
        
    }

    void Update()
    {
        interactWithObjects();
    }

    void FixedUpdate()
    {
        checkForInteractables();
        
    }

    void checkForInteractables()
    {

        //Vector3 _lookPosition = camera.transform.position + (camera.transform.position +);
        RaycastHit _currentObjectLookingAt;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out _currentObjectLookingAt, interactDetectionDistance, interactableLayer, QueryTriggerInteraction.Collide);
        
        if (_currentObjectLookingAt.collider != null && interactableObjectName != _currentObjectLookingAt.collider.name) 
        {
            _currentObjectLookingAt.collider.TryGetComponent<IInteractable>(out IInteractable _interactable);
            currentInteractable = _interactable;
            interactableObjectName =  _currentObjectLookingAt.collider.name;
            Debug.Log(interactableObjectName + " is interactable");
        }

        else if(_currentObjectLookingAt.collider == null) 
        {
            currentInteractable = null;
            interactableObjectName = null;
        }

    }

    void interactWithObjects()
    {
        if (inputManager.InteractPressed)
        {
            currentInteractable?.Interact();
            //Debug.Log("Interacting");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * interactDetectionDistance);
    }
}
//foreach (LayerMask _layerMask in interactableLayers)
//            {
//                if (_currentObjectLookingAt.collider.gameObject.layer == _layerMask)
//                {
//                    currentLayer = _currentObjectLookingAt.collider.gameObject.layer;
//                    Debug.Log(LayerMask.LayerToName(currentLayer));
//                }
//            }