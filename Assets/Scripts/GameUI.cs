using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // These are simply references that correspond to the the UI elements that we created above and to the Player script as well.
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text armorText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text pickupText;
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private Text enemyText;
    [SerializeField]
    private Text waveClearText;
    [SerializeField]
    private Text newWaveText;
    [SerializeField]
    Player player;

    //With this Attributes we are declaring that variables are accessible from the Unity Inspector but not from other scripts.
    //A sprite represents an imported texture meant to be used with a 2D game or the UI.
    //An Image is what displays the Sprite to the screen.
    [SerializeField]
    Sprite redReticle;
    [SerializeField]
    Sprite yellowReticle;
    [SerializeField]
    Sprite blueReticle;
    [SerializeField]
    Image reticle;

    //This will change the Sprite to reflect the active gun.
    public void UpdateReticle()
    {
        switch (GunEquipper.activeWeaponType)
        {
            case Constants.Pistol:
                reticle.sprite = redReticle;
                break;
            case Constants.Shotgun:
                reticle.sprite = yellowReticle;
                break;
            case Constants.AssaultRifle:
                reticle.sprite = blueReticle;
                break;
            case Constants.SniperRifle:
                reticle.sprite = blueReticle;
                break;
            default:
                return;
        }
    }

    // 1 Show the wave clear bonus text by setting its enabled state to true, then
    // immediately call a coroutine that will hide the text again. 
    public void ShowWaveClearBonus()
    {
        waveClearText.GetComponent<Text>().enabled = true;
        StartCoroutine("hideWaveClearBonus");
    }
    // 2 Wait for four seconds before setting the enabled state to false — therefore, hiding the text.
    IEnumerator hideWaveClearBonus()
    {
        yield return new WaitForSeconds(4);
        waveClearText.GetComponent<Text>().enabled = false;
    }
    // 3 Enable and set the text for the pickup alert and restart the hidePickup() coroutine. 
    // This lets the player pick up two or more pickups in quick succession, 
    // without the second pickup’s text label displaying before the first pickup’s text times out.
    public void SetPickUpText(string text)
    {
        pickupText.GetComponent<Text>().enabled = true;
        pickupText.text = text;
        // Restart the Coroutine so it doesn’t end early
        StopCoroutine("hidePickupText");
        StartCoroutine("hidePickupText");
    }
    // 4 Wait for four seconds before removing the pickup text.
    IEnumerator hidePickupText()
    {
        yield return new WaitForSeconds(4);
        pickupText.GetComponent<Text>().enabled = false;
    }
    // 5 Show the new wave text.
    public void ShowNewWaveText()
    {
        StartCoroutine("hideNewWaveText");
        newWaveText.GetComponent<Text>().enabled = true;
    }
    // 6 Wait for four seconds before removing the new wave text.
    IEnumerator hideNewWaveText()
    {
        yield return new WaitForSeconds(4);
        newWaveText.GetComponent<Text>().enabled = false;
    }

    // Start is called before the first frame update
    // 1 Initializes the player health and ammunition text.
    void Start()
    {
        SetArmorText(player.armor);
        SetHealthText(player.health);
    }
    // 2 This, and the rest of the methods, is simply a setter that sets the related text values.
    public void SetArmorText(int armor)
    {
        armorText.text = "Armor: " + armor;
    }
    public void SetHealthText(int health)
    {
        healthText.text = "Health: " + health;
    }
    public void SetAmmoText(int ammo)
    {
        ammoText.text = "Ammo: " + ammo;
    }
    public void SetScoreText(int score)
    {
        scoreText.text = "" + score;
    }
    public void SetWaveText(int time)
    {
        waveText.text = "Next Wave: " + time;
    }
    public void SetEnemyText(int enemies)
    {
        enemyText.text = "Enemies: " + enemies;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
