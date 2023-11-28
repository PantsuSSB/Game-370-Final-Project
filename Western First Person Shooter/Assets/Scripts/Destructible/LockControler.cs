using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControler : MonoBehaviour, IDestructible
{
    int objectCurrentHealth;
    [SerializeField]
    int objectHealth;

    public void DamageObject(int _damageDealtToObject)
    {
        objectCurrentHealth -= _damageDealtToObject;
        if(objectCurrentHealth <= 0)
        {
            transform.GetComponentInParent<DoorControler>().SetDoorLockValue(false);
            Destroy(gameObject,0.01f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        objectCurrentHealth = objectHealth;
    }
}
