using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float delay = 0.25f;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] LayerMask aimLayerMask;
    [SerializeField] Transform firePoint;

    float nextFireTime;
    List<PowerUp> powerups = new List<PowerUp>();

    public GameObject muzzleFlashObject;
    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool muzzleFlashEnabled = false;

    public void AddPowerUp(PowerUp powerup) => powerups.Add(powerup);

    public void RemovePowerUp(PowerUp powerup) => powerups.Remove(powerup);

    // Start is called before the first frame update
    void Start()
    {
        muzzleFlashTimerStart = muzzleFlashTimer;
    }

    // Update is called once per frame
    void Update()
    {
        AimTowardsMouse();

        if (Input.GetMouseButton(0))
        {
            if (ReadyToFire())
            {
                Fire();
                muzzleFlashEnabled = true;
            }
        }
        if(muzzleFlashEnabled == true)
        {
            muzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if(muzzleFlashTimer <= 0)
        {
            muzzleFlashObject.SetActive(false);
            muzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;
        }
    }

    void AimTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // If we hit something,
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
        {
            var destination = hitInfo.point;
            destination.y = transform.position.y;

            Vector3 direction = destination - transform.position;
            // Gets rid of magnitude
            direction.Normalize();

            //Set rotation
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    // Time.time is the amount of time since the game started running, for e.g., if time is 1, then nextFireTime = 1.25, then once time is greater than nextFireTime, it's been 0.25 seconds, so it's a delay
    bool ReadyToFire() => Time.time >= nextFireTime;

    // Have a delay, when instantiating the shot, position is player's position + up, and firePoint is the gameobject position of barrel
    void Fire()
    {
        float newDelay = delay;
        foreach (var powerup in powerups)
        {
            newDelay *= powerup.DelayMultiplier;
        }

        nextFireTime = Time.time + newDelay;
        Bullet shot = Instantiate(bulletPrefab, firePoint.position + Vector3.up, transform.rotation);
        shot.Launch(transform.forward);
    }


}