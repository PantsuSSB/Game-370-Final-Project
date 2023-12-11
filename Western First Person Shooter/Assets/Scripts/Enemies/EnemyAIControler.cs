using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIControler : MonoBehaviour
{
    [SerializeField]
    ProjectileGun projectileGun;
    Transform player;
    [SerializeField]
    float distanceFromPlayer;
    [SerializeField]
    float currentTimeBetweenFire;
    [SerializeField]
    float gunRange;

    // Start is called before the first frame update
    void Start()
    {
        gunRange = projectileGun.GetBulletDistance();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        currentTimeBetweenFire = projectileGun.GetTimeBetweenFire();
        CheckIfEnemyCanShoot();
    }

    void CheckIfEnemyCanShoot()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceFromPlayer <= gunRange && currentTimeBetweenFire <= 0)
        {
            projectileGun.Fire();
        }
    }

    void LookAtPlayer()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
