using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawn : MonoBehaviour
{
    // robots is an array of robot GameObjects to instantiate; red, yellow and blue robots.
    [SerializeField]
    GameObject[] robots;

    private int timesSpawned; //  tracks the spawn cycle of the robots.
    private int healthBonus = 0; // how much health each robot gains each new wave.

    // SpawnRobot() spawns a robot, sets its health, then sets the robot’s position. 
    public void SpawnRobot()
    {
        timesSpawned++;
        healthBonus += 1 * timesSpawned;
        GameObject robot = Instantiate(robots[Random.Range(0, robots.Length)]);
        robot.transform.position = transform.position;
        robot.GetComponent<Robot>().health += healthBonus;
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
