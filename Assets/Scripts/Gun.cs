using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //fireRate is the speed at which the gun will fire.
    public float fireRate;
    //This tracks the guns' ammunition and stores references to the fire and dry-fire sound effects.
    public Ammo ammo;
    public AudioClip liveFire;
    public AudioClip dryFire;

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
        //This checks if the player has any remaining ammunition.
        // If so, you play the liveFire sound; otherwise, play the dryFire sound.
        if (ammo.HasAmmo(tag))
        {
            GetComponent<AudioSource>().PlayOneShot(liveFire);
            ammo.ConsumeAmmo(tag);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(dryFire);
        }
        GetComponentInChildren<Animator>().Play("Fire");
    }
}
