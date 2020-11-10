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

    public float zoomFactor; // Control zoom level
    public int range; // range of the gun
    public int damage; // damage of the gun

    private float zoomFOV; // Field of view based on the zoom factor.
    private float zoomSpeed = 6;

    //lastFireTime tracks the last time the gun was fired.
    protected float lastFireTime;

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

        // This creates a ray and checks what that ray hits. 
        // This determines if the GameObject hit was a robot; if so, it passes on the damage.
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            processHit(hit.collider.gameObject);
        }
    }

    // This passes the damage to the correct GameObject. 
    private void processHit(GameObject hitObject)
    {
        if (hitObject.GetComponent<Player>() != null)
        {
            hitObject.GetComponent<Player>().TakeDamage(damage);
        }
        if (hitObject.GetComponent<Robot>() != null)
        {
            hitObject.GetComponent<Robot>().TakeDamage(damage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initializes the zoom factor.
        zoomFOV = Constants.CameraDefaultZoom / zoomFactor;

        //This sets lastFireTime to 10 seconds ago.
        lastFireTime = Time.time - 10;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Right Click (Zoom)
        // If the player hits the right mouse button, this smoothly animates the zoom effect via Mathf.Lerp().
        if (Input.GetMouseButton(1))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,
           zoomFOV, zoomSpeed * Time.deltaTime);
        }
        else
        {
            Camera.main.fieldOfView = Constants.CameraDefaultZoom;
        }
    }


}