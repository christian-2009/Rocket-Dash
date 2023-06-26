using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
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
        // Debug.Log();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }else  {
            audioSource.Pause();
  
        }

    }

    void ProcessRotation() 
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
             ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }

    }
    void ApplyRotation(float rotationThrust) 
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate( Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
