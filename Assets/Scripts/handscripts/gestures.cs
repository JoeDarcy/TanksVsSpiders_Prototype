using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.UI;

public static class gestures 
{

    private static float fistThreshold = 0.6f;
    private static float rollTolerance = 0.15f;


     public enum Gesture
    {
        ROCK,
        PAPER,
        SCISSORS,
    }

    public static void HandleGestures(handmanager.PlayerHand ph)
    {
        if (IsRock(ph.hand)) ph.gesture = Gesture.ROCK;
        else if (IsPaper(ph.hand)) ph.gesture = Gesture.PAPER;
        else if (IsScissors(ph.hand)) ph.gesture = Gesture.SCISSORS;
    }
   
  
    private static bool IsScissors(Hand hand)
    {
        if (CountExtended(hand) == 0) return false;
        return utilities.WithinThreshold(hand.PalmNormal.Roll, 1.50f, rollTolerance) || utilities.WithinThreshold(hand.PalmNormal.Roll, -1.50f, rollTolerance);
    }

    private static int CountExtended(Hand hand)
    {
        int result = 0;
        foreach(Finger f in hand.Fingers)
        {
            if (f.IsExtended) result++;
        }
        return result;
    }

    private static bool IsPaper(Hand hand)
    {
        if (CountExtended(hand) != hand.Fingers.Count) return false;
        return utilities.WithinThreshold(hand.PalmNormal.Roll, 0, rollTolerance);
    } 

    private static bool IsRock(Hand hand)
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
