using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    override protected void Update()
    {
        base.Update();
        // Shotgun & Pistol have semi-auto fire rate
        //This checks whether enough time has elapsed between shots to allow for another one.
        //If there is, it will trigger the gun firing animation
        if (Input.GetMouseButtonDown(0) && (Time.time - lastFireTime)
             > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}
