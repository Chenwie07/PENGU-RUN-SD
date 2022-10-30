using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        // make this script generic
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    internal void resetPlayerPosition()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform; 
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.forward * playerPosition.position.z;
    }
}
