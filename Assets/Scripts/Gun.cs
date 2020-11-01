using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //fireRate is the speed at which the gun will fire.
    public float fireRate;
    //lastFireTime tracks the last time the gun was fired.
    protected float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        //This sets lastFireTime to 10 seconds ago.
        lastFireTime = Time.time - 10;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }
    protected void Fire()
    {
    }
}
