using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewMechData", menuName = "ScriptableObjects/MechData", order = 1)]
public class ASBaseData : ScriptableObject
{
    public float DragForce;
    public float MechMass;
}
