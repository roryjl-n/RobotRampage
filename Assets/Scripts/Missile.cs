using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 30f; //how fast the missile should travel.
    public int damage = 10; // how much damage this missile will cause when it hits the player.

    /* The script checks if the Missile GameObject collided with the Player GameObject based on its tag.
       It also checks to see if if the player is still active.
       If so, it tells the Player script to take damage and passes along its damage value.
       Once the missile hits the player, it destroys itself. */
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.GetComponent<Player>() != null
        && collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    // Coroutines -  Coroutines take methods that return IEnumerator.
    //  These determine the duration of the coroutine. 

    //1 When we instantiate a missile, we start a coroutine called deathTimer().
    // This is name of the method that the coroutine will call.
    void Start()
    {
        StartCoroutine("deathTimer");
    }
    // 2 Move the missile forward at speed multiplied by the time between frames.
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    // 3 The method immediately returns a WaitForSeconds, set to 10.
    // Once those 10 seconds have passed, the method will resume after the yield statement.
    // If the missile doesn’t hit the player, it should auto-destruct.
    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
