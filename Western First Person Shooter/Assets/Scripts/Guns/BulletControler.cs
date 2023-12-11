using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    float bulletSpeed;
    [SerializeField]
    int bulletDamage;
    float timeTillBulletDestroy;
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
        BulletSelfDestroyTimer();
    }

    public void SetBulletStats(float _bulletSpeed, int _bulletDamage, float _timeTillBulletDestory)
    {
        bulletSpeed = _bulletSpeed;
        bulletDamage = _bulletDamage;
        timeTillBulletDestroy = _timeTillBulletDestory;
    }

    void BulletSelfDestroyTimer()
    {
        if(timeTillBulletDestroy >= 0) { timeTillBulletDestroy -= Time.fixedDeltaTime; }
        if(timeTillBulletDestroy < 0) { Destroy(gameObject); }
    }

    void MoveBulletForward()
    {
        rigidbody.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) 
        {
            other.gameObject.GetComponent<PlayerStats>().SubtractPlayerHealth(bulletDamage);
            Debug.LogWarning("Hit Player");
            Destroy(gameObject); 
        }

        else if (other.gameObject.layer == 11) 
        {
            other.transform.TryGetComponent<IDestructible>(out IDestructible _destructible);
            _destructible.DamageObject(bulletDamage);
            Destroy(gameObject);
        }

        else if (other.gameObject.layer != 14) { Destroy(gameObject); Debug.Log("Destory bullet"); }
    }
}
