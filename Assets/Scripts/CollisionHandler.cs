using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    int currentScreenIndex;

    void Start() {
        currentScreenIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnCollisionEnter(Collision other) 
    {
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("collided to friendly");
                break;
            case "Finish":
                NextScene();
                break;
            case "Fuel":
                Debug.Log("collided to Fuel");
                break;
             default:
                ReloadScene();
                break;
        }

    }

    void ReloadScene()
    {
        SceneManager.LoadScene(currentScreenIndex);
    }

    void NextScene()
    {   
        
        int numberOfScenes = SceneManager.sceneCountInBuildSettings;
        int nextScreenIndex = currentScreenIndex +1;

        if(nextScreenIndex == numberOfScenes)
        {
            nextScreenIndex = 0;
        }

        SceneManager.LoadScene(nextScreenIndex);

    }
}
