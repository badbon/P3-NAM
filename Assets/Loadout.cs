using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadout : MonoBehaviour
{
    // controls the actual weapons, equipment, and armament that the player (or, NPC) has
    public Weapon equipedWeapon;
    public List<Weapon> weapons; // List of weapons that the player has
    public int currentMagazineBullets; // Number of bullets in the magazine

    public GameObject bulletPrefab; // Prefab of the generic bullet that is fired from the weapon

    void Start()
    {
        // Dummy test weapon
        equipedWeapon = new Weapon("M16", true, 10.0f, 10.0f, 0, 0.1f, 30, 1.0f, 1.0f);
        weapons = new List<Weapon>();
        weapons.Add(equipedWeapon);
        
    }

    public void Fire()
    {
        // Fires the weapon
        if (equipedWeapon.isFirearm)
        {
            // Firearm
            if(equipedWeapon.magazineSize > 0)
            {
                // Has ammo
                equipedWeapon.magazineSize -= 1;
                //Spawn bullet. With some firing offset for bullet travel
                GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.right / 2, transform.rotation);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.damage = equipedWeapon.damage;
                bulletScript.range = equipedWeapon.range;
                bulletScript.accuracyModifier = equipedWeapon.accuracyModifier;

            }
            else
            {
                // No ammo. Reload
                Debug.Log("No ammo!");
                StartCoroutine(Reload());
            }
        }
    }

    public IEnumerator Reload()
    {
        // Reloads the weapon
        yield return new WaitForSeconds(equipedWeapon.reloadTime);
        currentMagazineBullets = equipedWeapon.magazineSize;
    }
}

public class Weapon 
{
    // Weapon class (firearm, knife, etc. stores all data about the weapon)
    
    public string name;
    public bool isFirearm; // false for knives, true for grenades, guns, etc.
    public float damage;
    public float range;
    public int accuracyModifier; // If above 0.0, it causes rotations of bullet to be less accurate
    public float fireRate;
    public int magazineSize;
    public float reloadTime;
    public float weightModifier;

    public Weapon(string name, bool isFirearm, float damage, float range, int accuracyModifier, float fireRate, int magazineSize, float reloadTime, float weightModifier)
    {
        this.name = name;
        this.isFirearm = isFirearm;
        this.damage = damage;
        this.range = range;
        this.accuracyModifier = accuracyModifier;
        this.fireRate = fireRate;
        this.magazineSize = magazineSize;
        this.reloadTime = reloadTime;
        this.weightModifier = weightModifier;
    }

}