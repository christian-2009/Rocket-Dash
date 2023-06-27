using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound; 
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles; 

    int currentScreenIndex;
    AudioSource audioSource;
    bool isTransitioning = false;

    void Start() {
        currentScreenIndex = SceneManager.GetActiveScene().buildIndex;

        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartNextSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        PlayAudioClip(crashSound);
        Invoke("ReloadScene", delay);
    }

    void StartNextSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        successParticles.Play();
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
