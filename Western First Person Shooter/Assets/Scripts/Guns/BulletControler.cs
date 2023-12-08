using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    float bulletSpeed;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBulletForward();
    }

    public void SetBulletSpeed(float _bulletSpeed)
    {
        bulletSpeed = _bulletSpeed;
    }

    void MoveBulletForward()
    {
        rigidbody.velocity = transform.forward * bulletSpeed;
    }
}
