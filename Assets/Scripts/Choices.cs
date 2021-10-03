using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Choices : MonoBehaviour
{

    // Sprites Player_1
    [SerializeField] GameObject p1RockSprite = null;
    [SerializeField] GameObject p1PaperSprite = null;
    [SerializeField] GameObject p1ScissorsSprite = null;
    // Player_1 Hearts
    [SerializeField] GameObject p1heart1 = null;
    [SerializeField] GameObject p1heart2 = null;
    [SerializeField] GameObject p1heart3 = null;
    // Sprite Player_2
    [SerializeField] GameObject p2RockSprite = null;
    [SerializeField] GameObject p2PaperSprite = null;
    [SerializeField] GameObject p2ScissorsSprite = null;
    // Player_2 Hearts
    [SerializeField] GameObject p2heart1 = null;
    [SerializeField] GameObject p2heart2 = null;
    [SerializeField] GameObject p2heart3 = null;

    // Round timer
    public float roundTimer = 0.0f;

    // Player health
    public int player1Health = 3;
    public int player2Health = 3;


    // Start is called before the first frame update
    void Start()
    {
        roundTimer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Start round timer
        if (Shooting.startTimer == true) {
            roundTimer -= Time.deltaTime;
		}

        if (roundTimer <= 0) {
            // Reset all sprites to false ready for the next round
            p1RockSprite.SetActive(false);
            p1PaperSprite.SetActive(false);
            p1ScissorsSprite.SetActive(false);
            p2RockSprite.SetActive(false);
            p2PaperSprite.SetActive(false);
            p2ScissorsSprite.SetActive(false);
            // Reset all the shooting flags (public static bools)
            Shooting.p1RockPressed = false;
            Shooting.p1PaperPressed = false;
            Shooting.p1ScissorsPressed = false;
            Shooting.p2RockPressed = false;
            Shooting.p2PaperPressed = false;
            Shooting.p2ScissorsPressed = false;
            // Reset startTimer public bool 
            Shooting.startTimer = false;
            // Reset roundTimer
            roundTimer = 2.0f;
            // Reset player1ChoiceMade bool
            Shooting.player1ChoiceMade = false;
        }

        // Turn on sprites for each players choice
        if (Shooting.p1RockPressed == true) {
            p1RockSprite.SetActive(true);
		}

        if (Shooting.p1PaperPressed == true) {
            p1PaperSprite.SetActive(true);
        }

        if (Shooting.p1ScissorsPressed == true) {
            p1ScissorsSprite.SetActive(true);
        }

        if (Shooting.p2RockPressed == true) {
            p2RockSprite.SetActive(true);
        }

        if (Shooting.p2PaperPressed == true) {
            p2PaperSprite.SetActive(true);
        }

        if (Shooting.p2ScissorsPressed == true) {
            p2ScissorsSprite.SetActive(true);
        }

        // Game logic for damage and blocking
        if (Shooting.p1RockPressed == true && Shooting.p2PaperPressed != true) {
            player2Health -= 1;
            Debug.Log("Player 2 health: " + player2Health);
            Shooting.p1RockPressed = false;
        }



        // Heart controller
        //Player_1
        if (player1Health == 3)
        {
            p1heart1.SetActive(true);
            p1heart2.SetActive(true);
            p1heart3.SetActive(true);
        }
        else if (player1Health == 2)
        {
	        p1heart1.SetActive(true);
	        p1heart2.SetActive(true);
	        p1heart3.SetActive(false);
        } 
        else if (player1Health == 1) {
	        p1heart1.SetActive(true);
	        p1heart2.SetActive(false);
	        p1heart3.SetActive(false);
        } 
        else if (player1Health == 0)
        {
	        p1heart1.SetActive(false);
	        p1heart2.SetActive(false);
	        p1heart3.SetActive(false);
        }

        //Player_2
        if (player2Health == 3) {
	        p2heart1.SetActive(true);
	        p2heart2.SetActive(true);
	        p2heart3.SetActive(true);
        } else if (player2Health == 2) {
	        p2heart1.SetActive(true);
	        p2heart2.SetActive(true);
	        p2heart3.SetActive(false);
        } else if (player2Health == 1) {
	        p2heart1.SetActive(true);
	        p2heart2.SetActive(false);
	        p2heart3.SetActive(false);
        } else if (player2Health == 0) {
	        p2heart1.SetActive(false);
	        p2heart2.SetActive(false);
	        p2heart3.SetActive(false);
        }
    }
}
