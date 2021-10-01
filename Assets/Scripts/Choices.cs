using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choices : MonoBehaviour
{

    // Sprites Player_1
    [SerializeField] GameObject p1RockSprite = null;
    [SerializeField] GameObject p1PaperSprite = null;
    [SerializeField] GameObject p1ScissorsSprite = null;
    // Sprite Player_2
    [SerializeField] GameObject p2RockSprite = null;
    [SerializeField] GameObject p2PaperSprite = null;
    [SerializeField] GameObject p2ScissorsSprite = null;

    // Round timer
    public float roundTimer = 0.0f;

    // Player health
    public int player1Health = 5;
    public int player2Health = 5;


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
		}
    }
}
