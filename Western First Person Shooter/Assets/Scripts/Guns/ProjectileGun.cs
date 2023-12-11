using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : GunBase
{
    [SerializeField]
    GameObject bullet;

    Transform player;

    [SerializeField]
    int bulletSpeed;

    [SerializeField]
    float timeTillBulletDestorySelf;

    [SerializeField]
    AudioSource gunSound;
    [SerializeField]
    AudioSource reloadSound;

    [SerializeField]
    bool testBulletFire;
    // Start is called before the first frame update
    void Start()
    {
        currentTimeBetweenFire = 0;
        firePoint = transform.GetChild(0);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Timer();
        if (testBulletFire)
        {
            testBulletFire = false;
            Fire();
            
        }
        firePoint.transform.LookAt(Camera.main.transform);
    }

    public float GetTimeBetweenFire()
    {
        return currentTimeBetweenFire;
    }

    public float GetBulletDistance()
    {
        return bulletDistance;
    }
    //Curently the enemy does not reload the gun. when reloading is added the the currentAmmoInClip will be incriminted.
    public override void Fire()
    {
        //currentAmmoInClip > 0 && 
        if (currentReloadTime <= 0 && currentTimeBetweenFire <= 0)
        {
            for (int _bullet = 0; _bullet < bulletsPerFire; _bullet++)
            {
                Vector3 _bulletSpreadPosition = new Vector3(Random.Range(-randomBulletSpread, randomBulletSpread), Random.Range(-randomBulletSpread, randomBulletSpread), 0);

                //RaycastHit _hitScan;
                //Physics.Raycast(firePoint.position, firePoint.forward + _bulletSpreadPosition, out _hitScan, bulletDistance, shootableLayers, QueryTriggerInteraction.Collide);

                GameObject _firedBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                Vector3 _bulletRotation = _firedBullet.transform.rotation.eulerAngles + _bulletSpreadPosition;
                _firedBullet.transform.rotation = Quaternion.Euler(_bulletRotation);
                _firedBullet.GetComponent<BulletControler>().SetBulletStats(bulletSpeed, bulletDamage, timeTillBulletDestorySelf);

                //Debug.DrawLine(firePoint.position, firePoint.position + (firePoint.forward + _bulletSpreadPosition) * bulletDistance, Color.green, 1);
                //Debug.Log(_bulletSpreadPosition);
                //if (_hitScan.collider.gameObject.layer == enemyLayer) {  }

                //gets gun animator controller and plays the firing animation
                //Animator gunAnim = GetComponent<Animator>();
                //gunAnim.SetTrigger("gunFireAnim");

                //plays gunshot
                //gunSound.Play();

                //if (_hitScan.collider != null)
                //{
                //    _hitScan.transform.TryGetComponent<IDestructible>(out IDestructible _destructible);
                //    if (_destructible != null)
                //    {
                //        _destructible.DamageObject(bulletDamage);
                //        Debug.Log("Player Shot: " + _hitScan.collider.name);
                //    }

                //}

            }
            //gunFired?.Invoke();
            gunSound.Play();
            currentAmmoInClip--;
            currentTimeBetweenFire = timeBetweenFire;
            Debug.Log("Current bullets left in " + gameObject.name + ": " + currentAmmoInClip);
        }
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }
}
