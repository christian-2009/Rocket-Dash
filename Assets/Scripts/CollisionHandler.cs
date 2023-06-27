using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound; 

    int currentScreenIndex;
    AudioSource audioSource;

    void Start() {
        currentScreenIndex = SceneManager.GetActiveScene().buildIndex;

        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("collided to friendly");
                break;
            case "Finish":
                StartNextSequence();
                break;
            // case "Fuel":
            //     Debug.Log("collided to Fuel");
            //     break;
             default:
                StartCrashSequence();
                break;
        }

    }

    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        PlayAudioClip(crashSound);
        Invoke("ReloadScene", delay);
    }

    void StartNextSequence()
    {
        GetComponent<Movement>().enabled = false;
        PlayAudioClip(successSound);
        Invoke("NextScene", delay);
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

    void PlayAudioClip(AudioClip audioClip)
    {    
        audioSource.PlayOneShot(audioClip);
            
    }
}
