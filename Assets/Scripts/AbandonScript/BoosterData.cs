using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoosterData", menuName = "ScriptableObjects/BoosterData", order = 1)]
public class BoosterBaseData : ScriptableObject
{
    
    public string boostername;
    public float BooterHorizontalacceleration;
    public float BooterVerticalacceleration;
    public float BoosterHrizontalMaxSpeed;
    public float BoosterVerticalMaxSpeed;
    public float BoosterHorizontalThrust;
    public float BoosterVerticalThrust;
    public float BoosterHorizontalBurstThrust;
    public float BoosterVerticalBurstThrust;
    public float BurstBoostDuration;

}
