using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructureDataBase : MonoBehaviour
{
    
}

public static class PlayerStatusData 
{
    public static float Speed{get;set;}
    public static Vector3 Velocity{get;set;}
    public static float Energy{get;set;}
    public static float Drag{get;set;}
    public static float Thrust{get;set;}

    public static float rotationSpeed{ get; set; }

}



public static class PlayerInputData 
{
    public static Vector3 MoveInput { get; set; }


    public static Vector3 JumpInput { get; set; }

    public static Vector3 CameraMove { get; set; }

    public static bool DashInput { get; set; }
    public static bool IsUsingGamepad { get; set; }
}

