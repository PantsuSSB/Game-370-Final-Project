using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanGun : GunBase, IInteractable
{
    [SerializeField]
    LayerMask shootableLayers;
    [SerializeField]
    LayerMask enemyLayer;
    [SerializeField]
    LayerMask destructibleLayer;
    [SerializeField]
    AudioSource gunSound;
    [SerializeField]
    AudioSource reloadSound;

    public delegate void HitScanGunEvents();
    public static event HitScanGunEvents gunFired;
    public static event HitScanGunEvents gunReloaded;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = Camera.main.transform;
        currentAmmoInClip = maxAmmoPerClip;
        if (transform.parent != null && transform.parent.name == "GunPosition")
        {
            GetComponentInParent<PlayerGunControler>().SetCurrentGun(this);
            gameObject.layer = 14;
        }
            


    }

    // Update is called once per frame
    void Update()
    {
        Timer();


    }

    public override void Fire()
    {
        if (currentAmmoInClip > 0 && currentReloadTime <= 0 && currentTimeBetweenFire <= 0)
        {
            for(int _bullet = 0; _bullet < bulletsPerFire; _bullet++)
            {
                Vector3 _bulletSpreadPosition = new Vector3(Random.Range(-randomBulletSpread, randomBulletSpread), Random.Range(-randomBulletSpread, randomBulletSpread), 0);

                RaycastHit _hitScan;
                Physics.Raycast(firePoint.position, firePoint.forward + _bulletSpreadPosition, out _hitScan, bulletDistance, shootableLayers, QueryTriggerInteraction.Collide);
                
                Debug.DrawLine(firePoint.position, firePoint.position + (firePoint.forward + _bulletSpreadPosition) * bulletDistance , Color.green, 1);
                //Debug.Log(_bulletSpreadPosition);
                //if (_hitScan.collider.gameObject.layer == enemyLayer) {  }

                //gets gun animator controller and plays the firing animation
                Animator gunAnim = GetComponent<Animator>();
                gunAnim.SetTrigger("gunFireAnim");

                //plays gunshot
                gunSound.Play();

                if (_hitScan.collider != null) 
                {
                    _hitScan.transform.TryGetComponent<IDestructible>(out IDestructible _destructible);
                    if(_destructible != null)
                    {
                        _destructible.DamageObject(bulletDamage);
                        Debug.Log("Player Shot: " + _hitScan.collider.name);
                    }
                    
                }

            }
            gunFired?.Invoke();
            currentAmmoInClip--;
            currentTimeBetweenFire = timeBetweenFire;
            Debug.Log("Current bullets left in " + gameObject.name + ": " + currentAmmoInClip);
        }
    }

    public override void Reload()
    {
        if(currentAmmoInClip < maxAmmoPerClip)
        {
            currentAmmoInClip = maxAmmoPerClip;
            currentReloadTime = reloadTime;
            Debug.Log(gameObject.name + " reloaded. current ammo in clip: " + currentAmmoInClip);

            //gets gun animator controller and plays the reloading animation
            Animator gunAnim = GetComponent<Animator>();
            gunAnim.SetTrigger("gunReloadAnim");
            gunReloaded?.Invoke();
            //plays reload sound
            reloadSound.Play();
        }
    }

    public void Interact()
    {

        if (!gunIsHeld)
        {
            Transform _gunPosition = GameObject.Find("GunPosition").transform;
            if (_gunPosition.childCount > 0)
            {
                /* tried to get walking animations to work, gonna give up now and just get the level built - ian
                Animator gunAnim = GetComponent<Animator>();
                gunAnim.SetFloat("vertical", Input.GetAxis("Vertical"));
                gunAnim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
                */
                //this works, but resetting it to the interactable layer doesnt for some reason - ian
                //gameObject.layer = 14;
                //Debug.Log("gun layer is: " + gameObject.layer);

                Transform _playersOldGun = _gunPosition.GetChild(0);
                _playersOldGun.parent = null;
                _playersOldGun.position = transform.position;
                _playersOldGun.rotation = transform.rotation;
                _playersOldGun.gameObject.layer = 10;

                
            }

            // this isn't getting called for some reason - ian
             gameObject.layer = 14;
            //Debug.Log("gun layer is: " + gameObject.layer);

            transform.position = _gunPosition.position;
            transform.rotation = _gunPosition.rotation;
            transform.parent = _gunPosition;


            GetComponentInParent<PlayerGunControler>().SetCurrentGun(this);

            
        }
        
    }

}

    
