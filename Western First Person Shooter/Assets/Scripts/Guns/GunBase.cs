using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GunBase : MonoBehaviour
{
    [SerializeReference]
    protected int maxAmmoPerClip;
    
    [HideInInspector]
    public int currentAmmoInClip;

    [SerializeReference]
    protected int bulletsPerFire;

    [SerializeReference]
    protected int bulletDistance;

    [SerializeReference]
    protected float randomBulletSpread;

    [SerializeReference]
    protected int bulletDamage;

    [SerializeReference]
    protected float reloadTime;

    protected float currentReloadTime;

    [SerializeReference]
    protected float timeBetweenFire;

    protected float currentTimeBetweenFire;
    [SerializeReference]
    protected bool gunIsHeld;

    [SerializeReference]
    public string GunType;

    protected Transform firePoint;

    protected AudioSource audioSource;

    public abstract void Fire();

    public abstract void Reload();

    protected void Timer()
    {
        if (currentTimeBetweenFire > 0) currentTimeBetweenFire -= Time.deltaTime;

        if (currentReloadTime > 0) currentReloadTime -= Time.deltaTime;
    }

    public void SetGunIsHeld(bool _setHeldValue) { gunIsHeld = _setHeldValue; }
}
