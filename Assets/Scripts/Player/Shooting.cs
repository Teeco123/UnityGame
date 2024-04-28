using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int damage = 1;
    public float range = 50;
    public float fireRate = 15;

    public enum WeaponType
    {
        singleFire,
        Automatic
    }

    [SerializeField]
    private WeaponType weaponType;

    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (weaponType == WeaponType.Automatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else if (weaponType == WeaponType.singleFire)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        if (
            Physics.Raycast(
                playerCamera.transform.position,
                playerCamera.transform.forward,
                out hit,
                range
            )
        )
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            GameObject impactGO = Instantiate(
                impactEffect,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );
            Destroy(impactGO, 2f);
        }
    }
}
