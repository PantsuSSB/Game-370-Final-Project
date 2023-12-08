using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectControler : MonoBehaviour, IDestructible
{
    [SerializeField]
    int objectHealth;

    [SerializeField]
    GameObject objectDestroyEffect;

    private void Start()
    {
        gameObject.layer = 11;
        if (objectHealth <= 0) objectHealth = 1;
    }

    public void DamageObject(int _damageDealtToObject)
    {
        objectHealth -= _damageDealtToObject;
        if (objectHealth <= 0)
        {
            if(objectDestroyEffect != null) { Instantiate(objectDestroyEffect, transform.position, Quaternion.identity); }
            Destroy(gameObject);
        }
    }
}
