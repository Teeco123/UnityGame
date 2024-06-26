using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        //Scrolls through all game object children to choose weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;

        //Based on i value changes active game object
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                //Enabling every component of weapon
                foreach (Behaviour childCompnent in weapon.GetComponentsInChildren<Behaviour>())
                {
                    childCompnent.enabled = true;
                }
                //And mesh renderer cause its retarded
                weapon.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                //Same but disabling
                foreach (Behaviour childCompnent in weapon.GetComponentsInChildren<Behaviour>())
                {
                    childCompnent.enabled = false;
                }
                weapon.GetComponent<MeshRenderer>().enabled = false;
            }
            i++;
        }
    }
}
