using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioClip weakHitSound;

    // missilePrefab is the prefab for the missile
    [SerializeField]
    GameObject missileprefab;

    //robotType is the type of robot: RedRobot, BlueRobot or YellowRobot. 
    [SerializeField]
    private string robotType;

    public int health; // how much damage this robot can take before dying.
    public int range; // the distance the gun can fire.
    public float fireRate; // how fast it can fire.

    public Transform missileFireSpot; 
    UnityEngine.AI.NavMeshAgent agent; // reference to the NavMesh Agent component

    private Transform player; // what the robot should should track.
    private float timeLastFired; 

    private bool isDead; // tracks whether the robot is alive or dead.

    public Animator robot;

    private void fire()
    {
        // This creates a new missilePrefab and sets its position and rotation to the robot’s firing spot.
        GameObject missile = Instantiate(missileprefab);
        missile.transform.position = missileFireSpot.transform.position;
        missile.transform.rotation = missileFireSpot.transform.rotation;
        robot.Play("Fire"); //This plays the Fire animation when the robot fires a missile.

        GetComponent<AudioSource>().PlayOneShot(fireSound); // play the missile firing sound
    }

    // 1 Roughly same logic as the player TakeDamage() method.
    // It just plays a death animation before calling DestroyRobot().
    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }
        health -= amount;
        if (health <= 0)
        {
            isDead = true;
            robot.Play("Die");
            StartCoroutine("DestroyRobot");
            GetComponent<AudioSource>().PlayOneShot(deathSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(weakHitSound);
        }
    }
    // 2
    IEnumerator DestroyRobot()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 1 By default, all robots are alive.
        isDead = false;
        // We then set the agent and player values to the NavMesh Agent and Player components respectively.
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 2 Check if the robot is dead before continuing.
        if (isDead)
        {
            return;
        }

        // 3 Make the robot face the player.
        transform.LookAt(player);
        // 4 Tell the robot to use the NavMesh to find the player.
        agent.SetDestination(player.position);
        // 5 Check to see if the robot is within firing range and if there’s been enough time between shots to fire again.
        if (Vector3.Distance(transform.position, player.position) < range
        && Time.time - timeLastFired > fireRate)
        {
            // 6 Update timeLastFired to the current time and call Fire().
            timeLastFired = Time.time;
            fire();
        }
    }
}
