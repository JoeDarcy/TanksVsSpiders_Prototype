using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    private Vector3 spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.anyKeyDown) {
            Instantiate(bullet, spawnLocation , Quaternion.identity);
	    }
    }

    
}
