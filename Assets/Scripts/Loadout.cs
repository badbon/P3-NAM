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
    public float engagementRange = 10.0f;  // The range at which the NPC starts firing
    public float skill = 0.5f;  // Skill of NPC. 0 is worst, 1 is best. This will influence accuracy.

    public Transform fireTarget; // The target to fire at (player or hostiles, etc)
    public GameObject muzzleFlashObj;
    public GameObject shellCasingPrefab; // Prefab of the shell casing
    public AudioSource firingSound; // Sound to play when firing


    void Start()
    {
        // Dummy test weapon
        equipedWeapon = new Weapon("M16", true, 10.0f, 10.0f, 0, 700f, 30, 1.0f, 1.0f);
        weapons = new List<Weapon>();
        weapons.Add(equipedWeapon);

        firingSound = GetComponent<AudioSource>();
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

                // Calculate the bullet's direction
                Vector3 bulletDirection = bulletRotation * Vector3.right; // Assumes firing to the right is the default.

                // Calculate the offset for the bullet's initial position
                float playerRadius = GetComponent<Collider2D>().bounds.extents.magnitude; // Assumes a Collider2D on the player
                float bulletOffset = playerRadius + 0.25f; // Added a 0.1f padding to ensure it's outside the player
                Vector3 bulletSpawnPosition = transform.position + bulletDirection * bulletOffset;

                //Spawn bullet with the adjusted rotation and position
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, bulletRotation);
                
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.damage = equipedWeapon.damage;
                bulletScript.range = equipedWeapon.range;
                bulletScript.accuracyModifier = equipedWeapon.accuracyModifier;

                // Enable muzzle flash
                //muzzleFlashObj = 

                //StartCoroutine(DelayGameobjectDisable(0.1f, muzzleFlashPosition))

                // Spawn shell casing
                //GameObject shellCasing = Instantiate(shellCasingPrefab, muzzleFlashPosition.position, muzzleFlashPosition.rotation);
                //shellCasing.transform.parent = muzzleFlashPosition; // Make the shell casing a child of the muzzle flash position

                // Play firing sound
                if(firingSound != null)
                    firingSound.Play();
            }
            else
            {
                // No ammo. Reload
                Debug.Log("No ammo! Reloading.");
                StartCoroutine(Reload());
            }
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, fireTarget.position);

        if (distanceToPlayer <= engagementRange)
        {
            // Aim and fire at player
            float aimAngle = Mathf.Atan2(fireTarget.position.y - transform.position.y, fireTarget.position.x - transform.position.x) * Mathf.Rad2Deg;
            FireWithAccuracy(aimAngle, distanceToPlayer);
        }
    }

    public void DelayDestroyObj(GameObject obj, float delay)
    {
        Destroy(obj, delay);
    }

    public IEnumerator DelayGameobjectDisable(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);

    }


    public void FireWithAccuracy(float angleInDegrees, float distanceToPlayer)
    {
        // Calculate inaccuracy based on distance and skill
        // Longer distances and lower skills increase the inaccuracy
        float inaccuracy = (1.0f - skill) * distanceToPlayer / engagementRange * equipedWeapon.accuracyModifier;

        // Randomize angle within the inaccuracy range
        float randomizedAngle = angleInDegrees + Random.Range(-inaccuracy, inaccuracy);

        // Fire with the randomized angle
        Fire(randomizedAngle);
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