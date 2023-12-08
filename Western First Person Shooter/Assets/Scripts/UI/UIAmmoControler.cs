using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAmmoControler : MonoBehaviour
{
    [SerializeField]
    GameObject[] bulletIcons;

    int currentActiveBullet;

    public void SetUIToCurrentGunBulletCount(int _currentGunBulletCount)
    {
        
        int _numIconsToDisable = bulletIcons.Length - _currentGunBulletCount;
        ResetBulletUI();
        for(; currentActiveBullet < _numIconsToDisable; currentActiveBullet++)
        {
            bulletIcons[currentActiveBullet].SetActive(false);
        }
    }

    public void RemoveBulletFromUI()
    {
        if(currentActiveBullet < bulletIcons.Length)
        {
            bulletIcons[currentActiveBullet].SetActive(false);
            currentActiveBullet++;
        }
    }

    public void ResetBulletUI()
    {
        int i = 0;
        foreach(GameObject _icon in bulletIcons)
        {
            bulletIcons[i].SetActive(true);
            i++;
        }
        currentActiveBullet = 0;
    }

    private void OnEnable()
    {
        HitScanGun.gunFired += RemoveBulletFromUI;
        HitScanGun.gunReloaded += ResetBulletUI;
    }

    private void OnDisable()
    {
        HitScanGun.gunFired -= RemoveBulletFromUI;
        HitScanGun.gunReloaded -= ResetBulletUI;
    }
}
