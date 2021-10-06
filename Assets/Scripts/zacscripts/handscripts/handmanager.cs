using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Interaction;
using UnityEngine.UI;
using System;

public class handmanager : MonoBehaviour
{

    [SerializeField]
    GameObject p1Sprites;

    [SerializeField]
    GameObject p2Sprites;

    [SerializeField]
    float roundLength;

    [SerializeField]
    float postRoundLength;
    [SerializeField]
    Text roundText;

    [SerializeField]
    GameObject p1Model;

    [SerializeField]
    GameObject p2Model;

    [SerializeField]
    LeapProvider provider;

    [SerializeField]
    GameObject floor;

    [SerializeField]
    Text debugText;
    [SerializeField]
    Text debugText2;


    public PlayerHand p1;
    public PlayerHand p2;

    public class PlayerHand
    {
        public Hand hand;
        public gestures.Gesture gesture;
        public GameObject model;
        public GameObject sprites;
 
    }

    private bool listening;
    private float bounds;
    private float roundTimer;
    private float postRoundTimer;
    int rounds = 1;
    bool roundstart = false;

    RoundResult result;
    // Start is called before the first frame update
    void Start()
    {
        p1 = new PlayerHand();
        p2 = new PlayerHand();
        Debug.Log("starting...");
        listening = false;
        roundstart = true;
        bounds = (floor.transform.position + floor.transform.lossyScale / 2).z;
        p1.model = p1Model;
        p2.model = p2Model;
        p1.gesture = gestures.Gesture.NONE;
        p2.gesture = gestures.Gesture.NONE;
        roundTimer = roundLength;
        postRoundTimer = postRoundLength;
        p1.sprites = p1Sprites;
        p2.sprites = p2Sprites;
        playerhands = new List<PlayerHand>();
        playerhands.Add(p1);
        playerhands.Add(p2);
    }

    List<PlayerHand> playerhands;

    // Update is called once per frame
    void Update()
    {
        Frame frame = provider.CurrentFrame;
        if (frame.Hands.Count >= 2) listening = true;
        if (listening)
        {           
            if (roundstart)
            {
                roundTimer -= Time.deltaTime;
                roundText.text = "Round " + rounds + "\n" + Mathf.CeilToInt(roundTimer).ToString();
            }
            foreach(Hand h in frame.Hands)
            {
                PlayerHand current = AssignPlayerHand(h);
                gestures.HandleGestures(current);       
                positioning.HandlePosition(current,bounds);
                PrintHandInfo(current);            
            }

            if(roundTimer <= 0 && postRoundTimer == postRoundLength)
            {
                result = HandleRoundResult();            
                rounds++;
                if (result == RoundResult.P1WIN)
                {                 
                    p1.model.GetComponentInChildren<Shooting>().FireGun();
                    Choices.player2Health--;
                    
                }
                else if (result == RoundResult.P2WIN)
                {
                    p2.model.GetComponentInChildren<Shooting>().FireGun();
                    Choices.player1Health--;
                    
                }

                foreach (PlayerHand ph in playerhands)
                {
                    foreach (Transform child in ph.sprites.transform)
                    {
                        if (child.gameObject.name.ToLower() == ph.gesture.ToString().ToLower())
                        {
                            child.gameObject.SetActive(true);
                        }
                        else child.gameObject.SetActive(false);
                    }
                }
                //^ this is terrible but i forgot how to code and the presentation is tomorrow

                roundTimer = roundLength;
                result = RoundResult.NONE;
                roundstart = false;
            }
            else if(!roundstart)
            {
                postRoundTimer -= Time.deltaTime;
                roundText.text = "Get Ready: " + Mathf.CeilToInt(postRoundTimer).ToString();
                if (postRoundTimer <= 0)
                {
                    postRoundTimer = postRoundLength;
                    roundstart = true;
                    foreach (PlayerHand ph in playerhands)
                    {
                        foreach (Transform child in ph.sprites.transform)
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                    //same thing here 

                }
            }
        }
    }



    enum RoundResult
    {
        P1WIN,
        P2WIN,
        TIE,
        NONE
    }

    RoundResult HandleRoundResult()
    {
        //there is 100% a better way to do this but this is fine for now
        if (p1.gesture == p2.gesture) return RoundResult.TIE;
        else if(p1.gesture == gestures.Gesture.ROCK)
        {
            if (p2.gesture == gestures.Gesture.SCISSORS) return RoundResult.P1WIN;
            else if(p2.gesture == gestures.Gesture.PAPER) return RoundResult.P2WIN;
        }
        else if (p1.gesture == gestures.Gesture.PAPER)
        {
            if (p2.gesture == gestures.Gesture.ROCK) return RoundResult.P1WIN;
            else if (p2.gesture == gestures.Gesture.SCISSORS) return RoundResult.P2WIN;
        }
        else if (p1.gesture == gestures.Gesture.SCISSORS)
        {
            if (p2.gesture == gestures.Gesture.PAPER) return RoundResult.P1WIN;
            else if (p2.gesture == gestures.Gesture.ROCK) return RoundResult.P2WIN;
        }
        return RoundResult.NONE;

    }

    PlayerHand AssignPlayerHand(Hand h)
    {
        PlayerHand current = null;
        if (h.IsLeft) current = p1;
        else if (h.IsRight) current = p2;
        else Debug.Log("Failed to assign hand");
        current.hand = h;
        return current;
    }

    void PrintHandInfo(PlayerHand ph)
    {
        Text current;
        if (ph == p1) current = debugText;
        else current = debugText2;
        current.text = "pitch: " + ph.hand.Direction.Pitch +
        "\nyaw: " + ph.hand.Direction.Yaw +
        "\nroll:" + ph.hand.PalmNormal.Roll +
        "\nposition: " + ph.hand.PalmPosition.z +
        "\n" + ph.gesture.ToString();
    }
}
