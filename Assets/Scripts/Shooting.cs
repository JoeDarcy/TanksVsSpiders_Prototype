using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    private Vector3 spawnLocation;
    private GameObject bulletInstance = null;

    // Public Choices
    public static bool p1RockPressed = false;
    public static bool p1PaperPressed = false;
    public static bool p1ScissorsPressed = false;

    public static bool p2RockPressed = false;
    public static bool p2PaperPressed = false;
    public static bool p2ScissorsPressed = false;

    public static bool startTimer = false;

    public static bool player1ChoiceMade = false;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        spawnLocation = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z); //moved this line of code so that the shot comes from correct position even if the player has moved - Zac
        // - Player 1
        // Left pressed (Rock)
        if (Input.GetKeyDown("a") && player1ChoiceMade == false) {
            Debug.Log("LeftArrow");
            p1RockPressed = true;
        }

        // Up pressed (Paper)
        if (Input.GetKeyDown("w") && player1ChoiceMade == false) {
            Debug.Log("UpArrow");
            p1PaperPressed = true;
	    }

        // Right pressed (Scissors)
        if (Input.GetKeyDown("d") && player1ChoiceMade == false) {
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


        // Fire when any key is pressed
        if (Input.anyKeyDown) {
            // Start global timer
            startTimer = true;
            // Fire gun
            FireGun();
            // Lock shooting (choice made)
            player1ChoiceMade = true;
        }
    }   

    public void FireGun() {
	    // Create an instance of the bullet if an instance doesn't exist
	    if (bulletInstance == null)
	    {
		    bulletInstance = Instantiate(bullet, spawnLocation, Quaternion.identity);
        }
        
    }
}
