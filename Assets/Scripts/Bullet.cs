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
	private float verticalPush = 8.0f; //magic number this is bad sorry for ruining ur script - Zac

	// Start is called before the first frame update
	void Start()
	{
		bulletRb = GetComponent<Rigidbody>();

		bulletRb.AddForce(push, verticalPush, 0, ForceMode.Impulse);
	}

	void Update()
	{
		if (transform.position.y < 0)
		{
			Destroy(bulletInstance);
		}
	}
}