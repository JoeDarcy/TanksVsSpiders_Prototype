using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class utilities
{ 

    public static bool WithinThreshold(float value, float target, float thresh)
    {
        float min = target - thresh;
        float max = target + thresh;
        return (value > min) && (value < max);
    }

}
