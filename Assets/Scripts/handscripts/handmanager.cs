using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Interaction;
using UnityEngine.UI;

public class handmanager : MonoBehaviour
{

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

    PlayerHand p1;
    PlayerHand p2;

    public class PlayerHand
    {
        public Hand hand;
        public gestures.Gesture gesture;
        public GameObject model;
    }

    private bool listening;
    private float bounds;
    // Start is called before the first frame update
    void Start()
    {
        p1 = new PlayerHand();
        p2 = new PlayerHand();
        Debug.Log("starting...");
        listening = true;
        bounds = (floor.transform.position + floor.transform.lossyScale / 2).z;
        p1.model = p1Model;
        p2.model = p2Model;

    }

    // Update is called once per frame
    void Update()
    {
        if (listening)
        {
            Frame frame = provider.CurrentFrame;
            foreach(Hand h in frame.Hands)
            {
                PlayerHand current = AssignPlayerHand(h);
                gestures.HandleGestures(current);
                positioning.HandlePosition(current,bounds);
                PrintHandInfo(current);
            }
        }
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
        "\n" + ph.gesture.ToString();
    }
}
