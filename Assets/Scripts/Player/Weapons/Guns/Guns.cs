using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/NewGun", order = 1)]
public class Guns : ScriptableObject
{
    public string gunName;
    public GameObject model;
    public int gunDamageMin;
    public int gunDamageMax;
    public int maxAmmo;
    public float fireRate;
    public float range;
    public float reloadTime;

    public AmmoType ammoType;
    public WeaponType weaponType;

    public enum WeaponType
    {
        singleFire,
        automatic,
    }

    public enum AmmoType
    {
        _9mm,
        _shells,
        _5_56mm,
        _7_62mm,
        _50_cal,
    }
}
