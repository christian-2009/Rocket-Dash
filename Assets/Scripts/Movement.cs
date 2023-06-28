using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem mainBoosterParticles; 


    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }else  {
            StopThrusting();
        }

    }

    void ProcessRotation() 
    {
    
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            StartRotatingLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            StartRotatingRight();
        }
        else {
            StopRotating();
        } 

    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

            if(!mainBoosterParticles.isPlaying)
            {
                mainBoosterParticles.Play();
            }
    }

    void StopThrusting() 
    {
        audioSource.Pause();
        mainBoosterParticles.Stop();
    }

    void StartRotatingLeft() 
    {
        if(!rightBoosterParticles.isPlaying)
            {
                rightBoosterParticles.Play();
            }
            ApplyRotation(rotationThrust);
    }

    void StartRotatingRight()
    {
        if(!leftBoosterParticles.isPlaying)
            {
                leftBoosterParticles.Play();
            }
            ApplyRotation(-rotationThrust);
    }

    void StopRotating()
    {   
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    void ApplyRotation(float rotationThrust) 
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate - prevents the ground rotating our objects
        transform.Rotate( Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
