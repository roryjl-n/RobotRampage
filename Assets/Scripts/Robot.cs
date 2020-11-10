﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
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

    private void fire()
    {
        Debug.Log("Fire");
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