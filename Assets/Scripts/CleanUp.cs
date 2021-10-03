using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
	[SerializeField] private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    timer -= Time.deltaTime;

	    if (timer <= 0)
	    {
            Destroy(gameObject);
	    }
    }
}
