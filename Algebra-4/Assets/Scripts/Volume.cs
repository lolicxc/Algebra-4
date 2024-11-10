using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Volume
{
    public abstract bool isOnFrustrum(Frustrum camFrustrum, Transform modelTransform);
}
