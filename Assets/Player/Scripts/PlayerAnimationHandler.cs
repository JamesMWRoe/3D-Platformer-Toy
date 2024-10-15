using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    bool hasClimbed;

    public void ClimbUp()
    {
        hasClimbed = true;
    }

    public void ClimbFinished()
    {
        hasClimbed = false;
    }

    public bool HasClimbed()
    {
        return hasClimbed;
    }
}
