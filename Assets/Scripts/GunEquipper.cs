using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEquipper : MonoBehaviour
{
    // reference to the GameUI
    [SerializeField]
    GameUI gameUI;

    public static string activeWeaponType;

    //This GameObject variables reference each gun
    public GameObject pistol;
    public GameObject assaultRifle;
    public GameObject shotgun;

    //activeGun keeps track of the currently equipped gun.
    GameObject activeGun;

    //This method will turn off all gun GameObjects, set the passed-in GameObject as active, then update the activeGun reference. 
    private void loadWeapon(GameObject weapon)
    {
        pistol.SetActive(false);
        assaultRifle.SetActive(false);
        shotgun.SetActive(false);
        weapon.SetActive(true);
        activeGun = weapon;
    }

    //This simply returns the activeGun so that other scripts can read that piece of info.
    public GameObject GetActiveWeapon()
    {
        return activeGun;
    }

    // Start is called before the first frame update
    void Start()
    {
        //we initialize the starting gun as the pistol.
        activeWeaponType = Constants.Pistol;
        activeGun = pistol;
    }

    // Update is called once per frame
    void Update()
    {
        // The code in each if statement checks whether the pressed key is 1, 2 or 3.
        // If one of those number keys is hit, the appropriate block calls loadWeapon() and passes it the appropriate weapon to activate.
        // It then sets activeWeaponType as a static. 
        if (Input.GetKeyDown("1"))
        {
            loadWeapon(pistol);
            activeWeaponType = Constants.Pistol;
            //This will update the reticle every time the player changes weapons.
            gameUI.UpdateReticle();
        }
        else if (Input.GetKeyDown("2"))
        { 
            loadWeapon(assaultRifle);
            activeWeaponType = Constants.AssaultRifle;
            gameUI.UpdateReticle();
        }
        else if (Input.GetKeyDown("3"))
        {
            loadWeapon(shotgun);
            activeWeaponType = Constants.Shotgun;
            gameUI.UpdateReticle();
        }
    }
}
