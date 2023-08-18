using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadout : MonoBehaviour
{
    // controls the actual weapons, equipment, and armament that the player (or, NPC) has
    public Weapon equipedWeapon;
    public List<Weapon> weapons; // List of weapons that the player has
    public int currentMagazineBullets; // Number of bullets in the magazine
    private float lastFiredTime = 0; // Time at which the weapon was last fired.
    public GameObject bulletPrefab; // Prefab of the generic bullet that is fired from the weapon

    void Start()
    {
        // Dummy test weapon
        equipedWeapon = new Weapon("M16", true, 10.0f, 10.0f, 0, 700f, 30, 1.0f, 1.0f);
        weapons = new List<Weapon>();
        weapons.Add(equipedWeapon);
    }

    public void Fire(float angleInDegrees)
    {
        float timeBetweenShots = 60.0f / equipedWeapon.fireRate; // Convert RPM to time between shots
        float currentTime = Time.time; // Get the current game time

        // Check if enough time has passed since the last shot
        if (currentTime - lastFiredTime < timeBetweenShots)
            return; // Too soon to fire again, exit the method

        lastFiredTime = currentTime; // Update the last fired time to now
        
        if (currentMagazineBullets > 0)
            {
                // Has ammo
                currentMagazineBullets -= 1;

                // Calculate the rotation based on the provided angle
                Quaternion bulletRotation = Quaternion.Euler(0, 0, angleInDegrees);

                // Calculate the offset for the bullet's initial position
                // Here we use the bullet's direction (calculated from the angle) and a fixed distance to determine the offset
                Vector3 bulletDirection = bulletRotation * Vector3.right; // Assumes firing to the right is the default.
                Vector3 bulletSpawnPosition = transform.position + bulletDirection * 0.5f; // Adjust the multiplier as needed for your offset.

                //Spawn bullet with the adjusted rotation and position
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, bulletRotation);
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
    
    public IEnumerator Reload()
    {
        // Reloads the weapon
        Debug.Log("Reloading... " + equipedWeapon.reloadTime);
        yield return new WaitForSeconds(equipedWeapon.reloadTime);
        currentMagazineBullets = equipedWeapon.magazineSize;
    }
}

[System.Serializable]
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