using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunControler : MonoBehaviour
{

    [SerializeField]
    HitScanGun currentHitScanGun;

    PlayerInputManager inputManager;

    [SerializeField]
    Transform gunPosition;

    [SerializeField]
    float gunBlockCheckSize;

    [SerializeField]
    float blockCheckOffsetZ;

    [SerializeField]
    LayerMask layersThatBlockGun;
    [SerializeField]
    bool gunIsBlocked;

    public delegate void SetNewGun(HitScanGun setGun);
    public static event SetNewGun gunSet;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckIfPlayerIsGrounded();
        if (inputManager.FirePressed && currentHitScanGun != null) currentHitScanGun.Fire();

        if (inputManager.ReloadPressed && currentHitScanGun != null) currentHitScanGun.Reload();
    }

    public void SetCurrentGun(HitScanGun _hitScanGun)
    {
        currentHitScanGun = _hitScanGun;
        gunSet?.Invoke(_hitScanGun);
    }

    void CheckIfPlayerIsGrounded()
    {
        Vector3 _blockCheckCenter = gunPosition.position + new Vector3(0, 0, blockCheckOffsetZ);
        

        gunIsBlocked = Physics.CheckSphere(_blockCheckCenter, gunBlockCheckSize, layersThatBlockGun);

    }

    private void OnDrawGizmos()
    {
        Vector3 _groundCheckCenter = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y + gunBlockCheckSize, transform.position.z);
        Vector3 _groundCheckSize = new Vector3(transform.lossyScale.x - 0.05f, gunBlockCheckSize * 2, transform.lossyScale.z - 0.05f);
        Gizmos.DrawWireSphere(gunPosition.position + new Vector3(0,0,blockCheckOffsetZ), gunBlockCheckSize);
    }
}
