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

    [SerializeField] [Range(-1.5f, 0)]
    float blockedGunPositionOffset;

    [SerializeField] [Range(0, 400)]
    float gunRotationSpeed = 5;

    float blockedGunCurrentRotationX;

    [SerializeField]
    float gunBlockCheckSize;

    [SerializeField]
    float blockCheckOffsetZ;

    [SerializeField]
    LayerMask layersThatBlockGun;

    [SerializeField]
    bool blockedGunCantShoot;

    bool gunIsBlocked;

    public delegate void SetNewGun(HitScanGun setGun);
    public static event SetNewGun gunSet;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        if(currentHitScanGun != null) { SetCurrentGun(currentHitScanGun); }
    }

    // Update is called once per frame
    void Update()
    {
        if(blockedGunCantShoot && currentHitScanGun != null) { RotateGunVertically(); }

        if (blockedGunCantShoot)
        {
            if (inputManager.FirePressed && currentHitScanGun != null && !gunIsBlocked) currentHitScanGun.Fire();

            if (inputManager.ReloadPressed && currentHitScanGun != null && !gunIsBlocked) currentHitScanGun.Reload();
        }

        else
        {
            if (inputManager.FirePressed && currentHitScanGun != null) currentHitScanGun.Fire();

            if (inputManager.ReloadPressed && currentHitScanGun != null) currentHitScanGun.Reload();
        }
    }

    private void FixedUpdate()
    {
        CheckIfGunIsBlocked();
    }

    void SetCurrentGun(HitScanGun _newHitScanGun)
    {
        if(currentHitScanGun == null)
        {
            currentHitScanGun = _newHitScanGun;
            currentHitScanGun.transform.parent = gunPosition;
            currentHitScanGun.transform.position = gunPosition.position;
            currentHitScanGun.transform.forward = gunPosition.forward;
            currentHitScanGun.gameObject.layer = 14;
        }

        else
        {
            currentHitScanGun.transform.parent = null;
            currentHitScanGun.transform.position = _newHitScanGun.transform.position;
            currentHitScanGun.transform.rotation = _newHitScanGun.transform.rotation;
            currentHitScanGun.gameObject.layer = 10;
            currentHitScanGun = _newHitScanGun;
            currentHitScanGun.transform.parent = gunPosition;
            currentHitScanGun.transform.position = gunPosition.position;
            currentHitScanGun.transform.forward = gunPosition.forward;
            currentHitScanGun.gameObject.layer = 14;
        }
            //Transform _gunPosition = GameObject.Find("GunPosition").transform;
            //if (_gunPosition.childCount > 0)
            //{
                /* tried to get walking animations to work, gonna give up now and just get the level built - ian
                Animator gunAnim = GetComponent<Animator>();
                gunAnim.SetFloat("vertical", Input.GetAxis("Vertical"));
                gunAnim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
                */
                //this works, but resetting it to the interactable layer doesnt for some reason - ian
                //gameObject.layer = 14;
                //Debug.Log("gun layer is: " + gameObject.layer);

                /*Transform _playersOldGun = _gunPosition.GetChild(0);
                _playersOldGun.parent = null;
                _playersOldGun.position = transform.position;
                _playersOldGun.rotation = transform.rotation;
                _playersOldGun.gameObject.layer = 10;*/


            //}

            // this isn't getting called for some reason - ian
            //gameObject.layer = 14;
            //Debug.Log("gun layer is: " + gameObject.layer);

            //transform.position = _gunPosition.position;
            //transform.rotation = _gunPosition.rotation;
            //transform.parent = _gunPosition;


            //GetComponentInParent<PlayerGunControler>().SetCurrentGun(this);


        

        //currentHitScanGun = _hitScanGun;
        gunSet?.Invoke(currentHitScanGun);
    }

    void CheckIfGunIsBlocked()
    {
        Vector3 _blockCheckCenter = gunPosition.position + gunPosition.forward * blockCheckOffsetZ;
        

        gunIsBlocked = Physics.CheckSphere(_blockCheckCenter, gunBlockCheckSize, layersThatBlockGun);

    }

    void RotateGunVertically()
    {
        if(gunIsBlocked && currentHitScanGun.transform.localRotation.x >  - 90)
        {
            //float _negitiveRotationPerFrame = -gunRotationSpeed * Time.deltaTime;
            blockedGunCurrentRotationX -= gunRotationSpeed * Time.deltaTime;
            if (blockedGunCurrentRotationX >= -90)
            {
                
                currentHitScanGun.transform.localRotation = Quaternion.Euler(blockedGunCurrentRotationX, 0, 0);
            }

            else if(blockedGunCurrentRotationX < -90)
            {
                blockedGunCurrentRotationX = -90;
                currentHitScanGun.transform.localRotation = Quaternion.Euler(blockedGunCurrentRotationX, 0, 0);
            }

        }

        else if (!gunIsBlocked && currentHitScanGun.transform.localRotation.x < 0)
        {
            //float _positiveRotationPerFrame = gunRotationSpeed * Time.deltaTime;
            blockedGunCurrentRotationX += gunRotationSpeed * Time.deltaTime;
            if (blockedGunCurrentRotationX <= 0)
            {
                currentHitScanGun.transform.localRotation = Quaternion.Euler(blockedGunCurrentRotationX, 0, 0);
            }

            else if (blockedGunCurrentRotationX > 0)
            {
                blockedGunCurrentRotationX = 0;
                currentHitScanGun.transform.localRotation = Quaternion.Euler(blockedGunCurrentRotationX, 0, 0);
            }
        }

        if(!gunIsBlocked && inputManager.FirePressed && currentHitScanGun.transform.localRotation.x < 0)
        {
            blockedGunCurrentRotationX = 0;
            currentHitScanGun.transform.localRotation = Quaternion.Euler(blockedGunCurrentRotationX, 0, 0);
        }
    }

    private void OnEnable()
    {
        HitScanGun.GunInteractedWith += SetCurrentGun;
    }

    private void OnDisable()
    {
        HitScanGun.GunInteractedWith -= SetCurrentGun;
    }

    //This draws the sphere that represents the check for if the gun is blocked 
    //If the gun is not blocked the sphere will draw white.
    //If the gun is blocked the sphear will draw red
    private void OnDrawGizmos()
    {
        if (!gunIsBlocked)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(gunPosition.position + gunPosition.forward * blockCheckOffsetZ, gunBlockCheckSize);
        }

        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gunPosition.position + gunPosition.forward * blockCheckOffsetZ, gunBlockCheckSize);
        }
    }
}
