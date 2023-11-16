using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractControler : MonoBehaviour
{
    Camera camera;

    [SerializeField]
    LayerMask playerLayer;
    
    [SerializeField]
    LayerMask[] interactableLayers;
    

    int currentLayer;

    [SerializeField]
    float interactDetectionDistance;
    
    void Start()
    {
        camera = Camera.main;
        playerLayer = ~LayerMask.GetMask("Player");
    }

    void FixedUpdate()
    {
        checkForInteraction();
    }

    void checkForInteraction()
    {

        //Vector3 _lookPosition = camera.transform.position + (camera.transform.position +);
        RaycastHit _currentObjectLookingAt;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out _currentObjectLookingAt, interactDetectionDistance, playerLayer, QueryTriggerInteraction.Collide);
        if(_currentObjectLookingAt.collider != null && currentLayer != _currentObjectLookingAt.collider.gameObject.layer) 
        {
            foreach (LayerMask _layerMask in interactableLayers)
            {
                if (_currentObjectLookingAt.collider.gameObject.layer == _layerMask)
                {
                    currentLayer = _currentObjectLookingAt.collider.gameObject.layer;
                    Debug.Log(LayerMask.LayerToName(currentLayer));
                }
            } //Debug.Log(LayerMask.LayerToName(currentLayer));
        }
        
        //Physics.Raycast(camera.transform.position, camera.transform.forward, out _currentObjectLookingAt, interactDetectionDistance, playerLayer);
        //if (_currentObjectLookingAt.collider.gameObject.name != null)
        //{
        //    Debug.Log(_currentObjectLookingAt.collider.gameObject.name);
        //    //foreach(LayerMask _layerMask in interactableLayers)
        //    //{
        //    //    if(_currentObjectLookingAt.collider.gameObject.layer == _layerMask) { Debug.Log(_currentObjectLookingAt.collider.gameObject.layer); }
        //    //}
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * interactDetectionDistance);
    }
}
