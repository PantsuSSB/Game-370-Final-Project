using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAmmoUIControler : MonoBehaviour
{
    [SerializeField]
    GameObject revolverUI;

    [SerializeField]
    GameObject rifleUI;

    [SerializeField]
    GameObject shotgunUI;

    
    GameObject currentUI;

    void SetCurrentUI(HitScanGun _uiTypeToSet)
    {
        if(currentUI != null)
        {
            currentUI.SetActive(false);
        }

        if(_uiTypeToSet.GunType == "revolver")
        {
            
            
            currentUI = revolverUI;
            currentUI.SetActive(true);

            currentUI.GetComponent<UIAmmoControler>().SetUIToCurrentGunBulletCount(_uiTypeToSet.currentAmmoInClip);
        }

        else if (_uiTypeToSet.GunType == "rifle")
        {
            currentUI = rifleUI;
            currentUI.SetActive(true);
            currentUI.GetComponent<UIAmmoControler>().SetUIToCurrentGunBulletCount(_uiTypeToSet.currentAmmoInClip);
        }

        else if (_uiTypeToSet.GunType == "shotgun")
        {
            currentUI = shotgunUI;
            currentUI.SetActive(true);

            currentUI.GetComponent<UIAmmoControler>().SetUIToCurrentGunBulletCount(_uiTypeToSet.currentAmmoInClip);
        }
    }

    private void OnEnable()
    {
        PlayerGunControler.gunSet += SetCurrentUI;
    }

    private void OnDisable()
    {
        PlayerGunControler.gunSet -= SetCurrentUI;
    }
}
