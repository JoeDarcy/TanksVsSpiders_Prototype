using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    private Vector3 spawnLocation;

    // Public Choices
    public static bool p1RockPressed = false;
    public static bool p1PaperPressed = false;
    public static bool p1ScissorsPressed = false;

    public static bool p2RockPressed = false;
    public static bool p2PaperPressed = false;
    public static bool p2ScissorsPressed = false;

    public static bool startTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Fire when any key is pressed
        if (Input.anyKeyDown) {
            // Start global timer
            startTimer = true;
            // Create an instance of the bullet
            Instantiate(bullet, spawnLocation , Quaternion.identity);
	    }

        // - Player 1
        // Left pressed (Rock)
        if (Input.GetKeyDown("a")) {
            Debug.Log("LeftArrow");
            p1RockPressed = true;
        }

        // Up pressed (Paper)
        if (Input.GetKeyDown("w")) {
            Debug.Log("UpArrow");
            p1PaperPressed = true;
	    }

        // Right pressed (Scissors)
        if (Input.GetKeyDown("d")) {
            Debug.Log("RightArrow");
            p1ScissorsPressed = true;
        }

        // -Player 2
        // Left pressed (Rock)
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Debug.Log("LeftArrow");
            p2RockPressed = true;
        }

        // Up pressed (Paper)
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("UpArrow");
            p2PaperPressed = true;
        }

        // Right pressed (Scissors)
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Debug.Log("RightArrow");
            p2ScissorsPressed = true;
        }

    }   
}
