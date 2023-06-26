using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        Debug.Log(other.gameObject.tag);
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("collided to friendly");
                break;
            case "Finish":
                Debug.Log("collided to Finish");
                break;
            case "Fuel":
                Debug.Log("collided to Fuel");
                break;
             default:
                Debug.Log("collided to Obstacle");
                break;
        }

    }
}
