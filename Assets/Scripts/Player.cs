using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
    public AudioClip playerDead;

    public int health;
    public int armor; // Once armor reaches zero, then the player receives full damage.

    // gameUI and gunEquipper are simply references to their respective script types.
    public GameUI gameUI;
    private GunEquipper gunEquipper;

    private Ammo ammo; // reference to the Ammo class 

    // TakeDamage() takes the incoming damage and reduces its amount based on how much armor the player has remaining.
    //  If the player has no armor, then you apply the total  damage to the player’s health.
    public void TakeDamage(int amount)
    {
        int healthDamage = amount;
        if (armor > 0)
        {
            int effectiveArmor = armor * 2;
            effectiveArmor -= healthDamage;
            // If there is still armor, don't need to process
            // health damage
            //These lines will update the armor as it takes damage.
            if (effectiveArmor > 0)
            {
                armor = effectiveArmor / 2;
                gameUI.SetArmorText(armor);
                return;
            }
            armor = 0;
            gameUI.SetArmorText(armor);
        }

        // If health reaches zero, it’s game over for the player; for now, you just log this to the console.
        health -= healthDamage;
        //Debug.Log("Health is " + health);
        gameUI.SetHealthText(health); // updates the health as it changes in the UI.
        if (health <= 0)
        {
            //Debug.Log("GameOver");
            //PLays playerDead audio and then call GameOver on the Game script.
            GetComponent<AudioSource>().PlayOneShot(playerDead);
            game.GameOver();
        }
    }

    // 1 This adds to the players health and armor respectively.
    private void pickupHealth()
    {
        health += 50;
        if (health > 200)
        {
            health = 200;
        }
        // This shows the pickup text alert and updates the health UI.
        gameUI.SetPickUpText("Health picked up + 50 Health");
        gameUI.SetHealthText(health);
    }
    private void pickupArmor()
    {
        armor += 15;
        // This shows the pickup text alert and updates the armor UI.
        gameUI.SetPickUpText("Armor picked up + 15 armor");
        gameUI.SetArmorText(armor);
    }
    // 2 This adds ammunition for that gun type.
    private void pickupAssaultRifleAmmo()
    {
        ammo.AddAmmo(Constants.AssaultRifle, 50);

        // First, this alerts the player of the ammunition pickup in the UI. 
        // Then, the code checks to see if the active gun matches the assault rifle before setting the ammo count. 
        gameUI.SetPickUpText("Assault rifle ammo picked up + 50 ammo");
        if (gunEquipper.GetActiveWeapon().tag == Constants.AssaultRifle)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.AssaultRifle));
        }
    }
    private void pickupPisolAmmo()
    {
        ammo.AddAmmo(Constants.Pistol, 20);

        gameUI.SetPickUpText("Pistol ammo picked up + 20 ammo");
        if (gunEquipper.GetActiveWeapon().tag == Constants.Pistol)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.Pistol));
        }
    }
    private void pickupShotgunAmmo()
    {
        ammo.AddAmmo(Constants.Shotgun, 10);

        gameUI.SetPickUpText("Shotgun ammo picked up + 10 ammo");
        if (gunEquipper.GetActiveWeapon().tag == Constants.Shotgun)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.Shotgun));
        }
    }

    // PickUpItem() takes an int that represents the type of item being picked up.
    public void PickUpItem(int pickupType)
    {
        // The Constants file references the IDs of all the pickups.
        switch (pickupType)
        {
            case Constants.PickUpArmor:
                pickupArmor();
                break;
            case Constants.PickUpHealth:
                pickupHealth();
                break;
            case Constants.PickUpAssaultRifleAmmo:
                pickupAssaultRifleAmmo();
                break;
            case Constants.PickUpPistolAmmo:
                pickupPisolAmmo();
                break;
            case Constants.PickUpShotgunAmmo:
                pickupShotgunAmmo();
                break;
            default:
                Debug.LogError("Bad pickup type passed" + pickupType);
                break;
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        // This just gets the Ammo and GunEquipper component attached to the Player GameObject.
        ammo = GetComponent<Ammo>();
        gunEquipper = GetComponent<GunEquipper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
