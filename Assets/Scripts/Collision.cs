using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
	[SerializeField] private GameObject Explosion = null;

	private void OnTriggerEnter(Collider other) {
		Instantiate(Explosion, transform.position, Quaternion.identity);
	}
}
