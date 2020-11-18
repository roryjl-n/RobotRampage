using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Gun
{
    override protected void Update()
    {
        base.Update();
        // Semi-auto Fire rate
        if (Input.GetMouseButtonDown(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}
