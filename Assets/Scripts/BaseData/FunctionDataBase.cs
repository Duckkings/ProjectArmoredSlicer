using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FunctionDataBase
{
    public static Vector3 CalculateMovementForce()
    {
        Vector3 moveDirection = PlayerInputData.MoveInput;
        float thrust = PlayerStatusData.Thrust;
        Vector3 force = moveDirection * thrust;
        return force;
    }
}
