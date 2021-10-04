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
    LeapProvider controller;
    [SerializeField]
    Text debugText;

    bool listening;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting...");
        listening = true;
        last = Gesture.NONE;
    }

    enum Gesture
    {
        ROCK,
        PAPER,
        SCISSORS,
        NONE
    }

    Gesture last;
    // Update is called once per frame
    void Update()
    {
        if (listening)
        {
            Frame frame = controller.CurrentFrame;
            foreach (Hand h in frame.Hands)
            {
                PrintHandInfo(h);
                if (last != Gesture.ROCK && IsRock(h))
                {
                    Debug.Log("Rock");
                    last = Gesture.ROCK;
                }
                else if (last != Gesture.SCISSORS && IsScissors(h))
                {
                    Debug.Log("Scissors");
                    last = Gesture.SCISSORS;
                }
                else if (last != Gesture.PAPER && IsPaper(h))
                {
                    Debug.Log("Paper");
                    last = Gesture.PAPER;
                }
            }
        }
    }



    void PrintHandInfo(Hand hand)
    {
        debugText.text = "pitch: " + hand.Direction.Pitch +
        "\nyaw: " + hand.Direction.Yaw +
        "\nroll:" + hand.PalmNormal.Roll +
        "\n" + last.ToString();
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
        return WithinThreshold(hand.PalmNormal.Roll, 1.50f, rollTolerance);
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
        return ((sum / hand.Fingers.Count) < fistThreshold);
  ;
    }
}
