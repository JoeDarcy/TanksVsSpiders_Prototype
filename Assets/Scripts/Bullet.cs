using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRb = null;
    [SerializeField] private float push = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();

        bulletRb.AddForce(push, push, 0, ForceMode.Impulse);
    }

}
