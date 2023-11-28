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

    void SetCurrentUI(string _uiTypeToSet)
    {
        if(currentUI != null)
        {
            currentUI.SetActive(false);
        }

        if(_uiTypeToSet == "revolver")
        {
            currentUI = revolverUI;
            currentUI.SetActive(true);
        }

        else if (_uiTypeToSet == "rifle")
        {
            currentUI = rifleUI;
            currentUI.SetActive(true);
        }

        else if (_uiTypeToSet == "shotgun")
        {
            currentUI = shotgunUI;
            currentUI.SetActive(true);
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
