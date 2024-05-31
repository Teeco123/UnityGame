using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NewAmmo", order = 1)]
public class Ammo : ScriptableObject
{
    public string ammoName;
    public int ammoDamage;
    public AmmoType ammoType;
    public AmmoEffect ammoEffect;

    public enum AmmoType
    {
        _9mm,
        _shells,
        _5_56mm,
        _7_62mm,
        _50_cal,
    }

    public enum AmmoEffect
    {
        none,
        fire,
        poison,
    }
}
