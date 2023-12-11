using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectControler : MonoBehaviour, IDestructible
{
    [SerializeField]
    int objectHealth;

    [SerializeField]
    AudioClip objectHit;

    [SerializeField]
    AudioClip objectDestoried;

    AudioSource audioSource;

    [SerializeField]
    GameObject objectDestroyEffect;

    private void Start()
    {
        gameObject.layer = 11;
        if (objectHealth <= 0) objectHealth = 1;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = objectHit;
    }

    public void DamageObject(int _damageDealtToObject)
    {
        objectHealth -= _damageDealtToObject;
        audioSource.Play();
        if (objectHealth <= 0)
        {
            if(objectDestroyEffect != null) { Instantiate(objectDestroyEffect, transform.position, Quaternion.identity); }
            
            Destroy(gameObject);
        }
    }
}
