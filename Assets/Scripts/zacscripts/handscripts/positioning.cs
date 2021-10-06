using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Interaction;

public class positioning
{
   public static void HandlePosition(handmanager.PlayerHand ph,float maxZ)
    {
        float handPosZ = (ph.hand.PalmPosition * 10).Normalized.z*maxZ;
        Vector3 pos = ph.model.transform.position;
        pos.z = handPosZ;
        ph.model.transform.position = pos;
    }
}
