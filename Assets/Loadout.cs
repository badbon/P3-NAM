using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadout : MonoBehaviour
{
    // controls the actual weapons, equipment, and armament that the player (or, NPC) has
    public Weapon equipedWeapon;
    public List<Weapon> weapons; // List of weapons that the player has

    void Start()
    {
        
    }


    void Update()
    {
        
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