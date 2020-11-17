using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Start()
    {
        // This simply ensures the GameObject that the script that it is attached to isn’t deleted when changing scenes,
        // so the music will keep playing when you switch to the battle! 
        DontDestroyOnLoad(gameObject);  
    }
}
