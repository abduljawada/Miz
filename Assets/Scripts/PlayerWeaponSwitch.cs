using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeaponSwitch : MonoBehaviour
{
    List<WeaponScript> weapons = new List<WeaponScript>();
    WeaponScript currentWeapon;

    private void Awake()
    {
        CheckWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (weapons.Count > 1)
            {
                if (!currentWeapon.attacking)
                {
                    if (weapons.IndexOf(currentWeapon) == weapons.Count - 1)
                    {
                        currentWeapon = weapons[0];

                        UpdateWeapon();
                    }
                    else
                    {
                        currentWeapon = weapons[weapons.IndexOf(currentWeapon) + 1];

                        UpdateWeapon();
                    }
                }
            }
        }
    }

    public void CheckWeapons()
    {
        WeaponScript[] weaponsArray = GetComponentsInChildren<WeaponScript>();

        weapons.Clear();

        foreach (WeaponScript weaponInArray in weaponsArray)
        {
            weapons.Add(weaponInArray);
        }

        if (weapons.Count > 0)
        {
            currentWeapon = weapons.First();

            UpdateWeapon();
        }
    }

    void UpdateWeapon()
    {
        foreach (WeaponScript weaponInList in weapons)
        {
            if (weaponInList == currentWeapon)
            {
                weaponInList.gameObject.SetActive(true);
            }
            else
            {
                weaponInList.gameObject.SetActive(false);
            }
        }
    }
}