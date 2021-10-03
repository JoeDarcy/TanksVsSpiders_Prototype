using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bullet : MonoBehaviour
{
	[SerializeField] private Rigidbody bulletRb = null;
	[SerializeField] private float push = 0.0f;
	[SerializeField] private GameObject bulletInstance = null;

	// Start is called before the first frame update
	void Start()
	{
		bulletRb = GetComponent<Rigidbody>();

		bulletRb.AddForce(push, push, 0, ForceMode.Impulse);
	}

	void Update()
	{
		if (transform.position.y < 0)
		{
			Destroy(bulletInstance);
		}
	}
}