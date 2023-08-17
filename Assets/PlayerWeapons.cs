using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    // Handles player weapon controls (Loadout.cs is the one that controls the actual weapons)
    public Loadout loadout;

    void Start()
    {
        loadout = GetComponent<Loadout>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            loadout.Fire();
        }
    }

}
