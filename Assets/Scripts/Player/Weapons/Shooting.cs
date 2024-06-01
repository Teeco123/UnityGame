using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Guns gunsStats;

    public int currentAmmo;

    private bool isReloading = false;

    public Camera playerCamera;
    public Transform muzzle;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = gunsStats.maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }

        //Player can hold or needs to click to shoot based on weapon type
        if (gunsStats.weaponType == Guns.WeaponType.automatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / gunsStats.fireRate;
                Shoot();
            }
        }
        else if (gunsStats.weaponType == Guns.WeaponType.singleFire)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / gunsStats.fireRate;
                Shoot();
            }
        }
    }

    //Stops player from shooting and adds ammo
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(gunsStats.reloadTime);
        currentAmmo = gunsStats.maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        gunsStats.muzzleFlash.Play();
        currentAmmo--;

        //Shoots raycast from gun
        RaycastHit hit;
        if (
            Physics.Raycast(
                muzzle.transform.position,
                playerCamera.transform.forward,
                out hit,
                gunsStats.range
            )
        )
        {
            //Gets enemy script from hit target
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(Random.Range(gunsStats.gunDamageMin, gunsStats.gunDamageMax));
            }
            //Creates bullet impact game object
            GameObject impactGO = Instantiate(
                gunsStats.impactEffect,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

            //Destroys impact so it won't fill whole scene 💀
            Destroy(impactGO, 2f);
        }
    }
}
