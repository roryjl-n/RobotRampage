using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{
    // pickups will store all the possible pickup types.
    [SerializeField]
    private GameObject[] pickups;

    // 1  Instantiates a random pickup and sets its position to that of the PickupSpawn GameObject.
    void spawnPickup()
    {
        // Instantiate a random pickup
        GameObject pickup = Instantiate(pickups[Random.Range(0,
       pickups.Length)]);
        pickup.transform.position = transform.position;
        pickup.transform.parent = transform;
    }
    // 2 Waits 20 seconds before calling spawnPickup()
    IEnumerator respawnPickup()
    {
        yield return new WaitForSeconds(20);

        spawnPickup();
    }
    // 3 Spawns a pickup as soon as the game beings.
    void Start()
    {
        spawnPickup();
    }
    // 4 Starts the coroutine to respawn when the player has picked up something.
    public void PickupWasPickedUp()
    {
        StartCoroutine("respawnPickup");
    }
}
