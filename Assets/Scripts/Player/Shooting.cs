using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int damage = 1;
    public float range = 50;
    public float fireRate = 15;

    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public enum WeaponType
    {
        singleFire,
        Automatic
    }

    [SerializeField]
    private WeaponType weaponType;

    public Camera playerCamera;
    public Transform muzzle;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
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

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
        RaycastHit hit;

        if (
            Physics.Raycast(
                muzzle.transform.position,
                playerCamera.transform.forward,
                out hit,
                range
            )
        )
        {
            Debug.DrawRay(
                muzzle.transform.position,
                playerCamera.transform.forward,
                Color.red,
                100
            );
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
