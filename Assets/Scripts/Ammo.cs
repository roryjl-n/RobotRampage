using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    GameUI gameUI;

    // //The variables pistolAmmo, shotgunAmmo and assaultRifleAmmo track their respective ammunition counts.
    [SerializeField]
    private int pistolAmmo = 20;
    [SerializeField]
    private int shotgunAmmo = 10;
    [SerializeField]
    private int assaultRifleAmmo = 50;
    [SerializeField]
    private int sniperRifleAmmo = 3;
    // tagToAmmo is a dictionary of type string and int, which lets us map a gun’s type to its ammunition count.

    public Dictionary<string, int> tagToAmmo;

    //Awake() is a special method called before Start()
    //This method simply makes each gun type a key in the Dictionary and sets that key’s value to the appropriate ammunition type. 
    void Awake()
    {
        tagToAmmo = new Dictionary<string, int>
        {
            { Constants.Pistol , pistolAmmo },
            { Constants.Shotgun , shotgunAmmo },
            { Constants.AssaultRifle , assaultRifleAmmo },
            { Constants.SniperRifle , sniperRifleAmmo },
        };
    }

    //This will add ammunition to the appropriate gun type.
    // If we’ve passed in an unrecognized bad gun type, you log it as an error.
    public void AddAmmo(string tag, int ammo)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }
        tagToAmmo[tag] += ammo;
    }

    // Returns true if gun has ammo
    //This will return true if the gun type has at least one bullet left, or false if it has no more bullets.
    public bool HasAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }
        return tagToAmmo[tag] > 0;
    }

    //This simply returns the bullet count for a gun type.
    public int GetAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed:" + tag);
        }
        return tagToAmmo[tag];
    }

    // this checks for the correct tag.  If it finds the appropriate ammunition, it subtracts a bullet.
    public void ConsumeAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed:" + tag);
        }
        tagToAmmo[tag]--;
        // This updates the UI each times the user fires his weapon.
        gameUI.SetAmmoText(tagToAmmo[tag]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
