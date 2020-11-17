using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //This allows you to use the SceneManager type
// SceneManger is a class that allows you to manage your scenes such as loading, unloading or even searching for a scene.


public class Menu : MonoBehaviour
{
    // 1 This method will be used when the user clicks the Begin button; it simply loads the Battle scene.
    public void StartGame()
    {
        SceneManager.LoadScene("Battle");
    }
    // 2 This method will be used to exit the app. However, note that if you are running the game in Unity (instead of within a standalone build) this won’t do anything.
    public void Quit()
    {
        Application.Quit();
    }
}
