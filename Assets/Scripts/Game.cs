using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // This uses the singleton pattern to have one and only one item
    // We only want one Game to track everything; score, remaining robots and current wave.
    private static Game singleton; 

    [SerializeField]
    RobotSpawn[] spawns; // spawns is the array of teleporters that spawn robots each wave

    public int enemiesLeft; //  is a counter, which tracks how many robots are still alive.

    // 1 Initialize the singleton and call SpawnRobots().
    void Start()
    {
        singleton = this;
        SpawnRobots();
    }
    // 2 Go through each RobotSpawn in the array and call SpawnRobot() to actually spawn a robot.
    private void SpawnRobots()
    {
        foreach (RobotSpawn spawn in spawns)
        {
            spawn.SpawnRobot();
            enemiesLeft++;
        }
    }

}
