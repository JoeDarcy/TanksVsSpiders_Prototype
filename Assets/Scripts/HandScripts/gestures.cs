using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.UI;

public class gestures : MonoBehaviour
{

    [SerializeField]
    float fistThreshold = 0.5f;
    [SerializeField]
    float rollTolerance = 0.1f;
    [SerializeField]
    LeapServiceProvider provider;
    [SerializeField]
    Text debugText;
    [SerializeField]
    Text debugText2;
    bool listening;

    PlayerHand p1;
    PlayerHand p2;

    // Start is called before the first frame update
    public class PlayerHand {
        public Hand hand;
        public Gesture gesture;
    }
    void Start()
    {
        p1 = new PlayerHand();
        p2 = new PlayerHand();
        p1.gesture = Gesture.NONE;
        p2.gesture = Gesture.NONE;
        Debug.Log("starting...");
        listening = true;
    }

     public enum Gesture
    {
        ROCK,
        PAPER,
        SCISSORS,
        NONE
    }
    // Update is called once per frame
    void Update()
    {
        if (listening)
        {
            Frame frame = provider.CurrentFrame;
            foreach (Hand h in frame.Hands) //leftmost and rightmost is depricated so we have to assume one person is using left hand and one is using right hand

            {
                PlayerHand current = null;
                if (h.IsLeft) {
                    current = p1;
                    p1.hand = h;
                }
                else if (h.IsRight) {
                    current = p2;
                    p2.hand = h;
                }
                else Debug.Log("failed to assign players");

                if (IsRock(h))
                {
                    current.gesture = Gesture.ROCK;
                }
                else if (IsPaper(h))
                {
                    current.gesture = Gesture.PAPER;
                }
                else if (IsScissors(h))
                {
                    current.gesture = Gesture.SCISSORS;
                }
                PrintHandInfo(current);
            }
        }
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

    bool WithinThreshold(float value, float target, float thresh)
    {
        float min = target - thresh;
        float max = target + thresh;
        return (value > min) && (value < max);
    }

    bool IsScissors(Hand hand)
    {
        if (CountExtended(hand) == 0) return false;
        return WithinThreshold(hand.PalmNormal.Roll, 1.50f, rollTolerance) || WithinThreshold(hand.PalmNormal.Roll, -1.50f, rollTolerance);
    }

    int CountExtended(Hand hand)
    {
        int result = 0;
        foreach(Finger f in hand.Fingers)
        {
            if (f.IsExtended) result++;
        }
        return result;
    }

    bool IsPaper(Hand hand)
    {
        foreach (Finger f in hand.Fingers)
        {
            if (!f.IsExtended) return false;
        }
        return WithinThreshold(hand.PalmNormal.Roll, 0, rollTolerance);
    } 

    bool IsRock(Hand hand)
    {
        float sum = 0.0f;
        if (CountExtended(hand) != 0) return false;
        foreach (Finger f in hand.Fingers)
        {
            Vector metacarpal = f.bones[(int)Bone.BoneType.TYPE_METACARPAL].Direction;
            Vector proximal = f.bones[(int)Bone.BoneType.TYPE_PROXIMAL].Direction;
            Vector intermedial = f.bones[(int)Bone.BoneType.TYPE_INTERMEDIATE].Direction;
            sum += metacarpal.Dot(proximal) + proximal.Dot(intermedial);
        }
        return (sum / hand.Fingers.Count) < fistThreshold;
  ;
    }
}
