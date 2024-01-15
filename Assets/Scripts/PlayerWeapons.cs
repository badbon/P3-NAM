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
        // Fire
        if (Input.GetButton("Fire1"))
        {
            float aimAngle = GetAimAngle();
            loadout.Fire(aimAngle);
        }

        //Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            loadout.StartCoroutine(loadout.Reload());
        }
    }

    private float GetAimAngle()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.transform.position.z - transform.position.z));

        Vector3 directionToMouse = mouseWorldPosition - transform.position;
        
        return Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
    }


}
