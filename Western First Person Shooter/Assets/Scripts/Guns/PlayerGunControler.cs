using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunControler : MonoBehaviour
{

    [SerializeField]
    HitScanGun currentHitScanGun;

    PlayerInputManager inputManager;

    public delegate void SetNewGun(string gunType);
    public static event SetNewGun gunSet;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.FirePressed && currentHitScanGun != null) currentHitScanGun.Fire();

        if (inputManager.ReloadPressed && currentHitScanGun != null) currentHitScanGun.Reload();
    }

    public void SetCurrentGun(HitScanGun _hitScanGun)
    {
        currentHitScanGun = _hitScanGun;
        gunSet?.Invoke(_hitScanGun.GunType);
    }
}
