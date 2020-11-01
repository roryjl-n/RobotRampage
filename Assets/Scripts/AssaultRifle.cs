using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Gun
{
    override protected void Update()
    {
        base.Update();
        // Automatic Fire
        // this script uses GetMouseButton to see if the player is holding down the left mouse button to auto-fire.
        if (Input.GetMouseButton(0) && Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}
