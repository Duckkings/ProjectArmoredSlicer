//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    //用于管理玩家的数据
//    public static PlayerController instance;
//    public Rigidbody Rigidbody;
//    public BoosterBaseData boosterBaseData;
//    public ASBaseData ASBaseData;
//    public Vector3 CurVelocity;
//    public Vector3 CurHorizontalVelocity;
//    public Vector3 CurVerticalVelocity;
//    public Vector3 TargetVelocity;
//    public Vector3 Gravity;
//    public float airDragBouns;
//    public Vector3 airHorizontalDrag;
//    public Vector3 airVerticalDrag;
//    public Vector3 NormalForce;
//    public Vector3 InputForce;
//    public Vector3 frictionForce;
//    public Vector3 MoveInput;
//    public Vector3 JumpInput;
//    public Vector3 LastMoveInput;
//    public Vector3 LastJumpInput;
//    public bool DashInput;
//    public bool isGround;

//    void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
            
//        }
//        else
//        {
//            Destroy(this);
//        }
        

//        Rigidbody = GetComponentInParent<Rigidbody>();
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        CurVelocity= Vector3.zero;
//        TargetVelocity= Vector3.zero;
//        Gravity=LevelManager.Instance.Gravity;
//        airHorizontalDrag = Vector3.zero;
//        airVerticalDrag = Vector3.zero;
//        if (airDragBouns== 0)
//        {
//            airDragBouns = 0.1f;
//        }
//        NormalForce = ASBaseData.MechMass * Gravity;

//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//        isGround = Physics.Raycast(this.transform.position, Vector3.down, 2f);
//        CurVelocity = Rigidbody.velocity;
//    }

//    public void NormalMovement()
//    {
//        AllowInput();
//        VelocitySplit();
//        FrictionForce();
//        AirDragHorizontalForce();
//        AirDragVerticalForce();
//        if (DashInput)
//        {
//            HorizontalBoost();
//        }
//        else
//        {
//            HorizontalVelocity();
//        }
        
//        VerticalVelocity();
        
//        //CurVelocity = CurHorizontalVelocity + CurVerticalVelocity;
//        //ColliderDetect();
//    }


//    public void BurstJumpBoost()
//    {
//        AllowInput();
//        VelocitySplit();
//        FrictionForce();
//        AirDragHorizontalForce();
//        AirDragVerticalForce();
//        HorizontalVelocity();
//        VerticalBoost();
        
//        CurVelocity = CurHorizontalVelocity + CurVerticalVelocity;
        
//    }

//    public void BurstDashBoost()
//    {
//        AllowInput();
//        VelocitySplit();
//        FrictionForce();
//        AirDragHorizontalForce();
//        AirDragVerticalForce();
//        HorizontalBoost();
//        VerticalVelocity();
        
//        CurVelocity = CurHorizontalVelocity + CurVerticalVelocity;
        
//    }


//    public void AllowInput()
//    {
//        MoveInput = InputManager.instance.MoveInput;
//        JumpInput = InputManager.instance.JumpInput;
//        DashInput = InputManager.instance.DashInput;
//    }
    

//    void HorizontalVelocity()
//    {
//        Vector3 HorizontalForce = MoveInput * boosterBaseData.BoosterHorizontalThrust-airHorizontalDrag-frictionForce;
//        //CurHorizontalVelocity+=HorizontalForce / ASBaseData.MechMass * Time.deltaTime;
//        Rigidbody.AddForce(HorizontalForce);

//    }

//    public void HorizontalBoost()
//    {
//        Vector3 HorizontalForce = MoveInput * boosterBaseData.BoosterHorizontalBurstThrust -airHorizontalDrag - frictionForce;
//        //CurHorizontalVelocity += HorizontalForce / ASBaseData.MechMass * Time.deltaTime;
//        Rigidbody.AddForce(HorizontalForce);

//    }

    

//    void VerticalVelocity()
//    {
//        Vector3 VerticalForce = JumpInput * boosterBaseData.BoosterVerticalThrust -airVerticalDrag+ASBaseData.MechMass*Gravity;
//        Rigidbody.AddForce(VerticalForce);
        

//    }

//    void VerticalBoost()
//    {
//        Vector3 VerticalForce = JumpInput * boosterBaseData.BoosterVerticalBurstThrust - airVerticalDrag + ASBaseData.MechMass * Gravity;
//        Rigidbody.AddForce(VerticalForce);

//    }

//    void VelocitySplit()
//    {
//        CurHorizontalVelocity = new Vector3(CurVelocity.x, 0f, CurVelocity.z);
//        CurVerticalVelocity = new Vector3(0f, CurVelocity.y, 0f);
//    }

    

//    public void FrictionForce()
//    {
//        if (isGround)
//        {
//            frictionForce = LevelManager.Instance.frictionCoefficient * NormalForce.magnitude*-CurVelocity.normalized;
            
            
//        }
//        else
//        {
//            frictionForce = Vector3.zero;
            
//        }
//    }

//    public void AirDragHorizontalForce()
//    {
//        airHorizontalDrag =-CurHorizontalVelocity.normalized* CurHorizontalVelocity.magnitude * CurHorizontalVelocity.magnitude * airDragBouns;
//    }

//    public void AirDragVerticalForce()
//    {
//        airVerticalDrag = -CurHorizontalVelocity.normalized * CurVerticalVelocity.magnitude * CurVerticalVelocity.magnitude * airDragBouns;
//    }

//    public void InputCache()
//    {
//        LastJumpInput = JumpInput;
//        LastMoveInput = MoveInput;
        
//    }

//    //private void ColliderDetect()
//    //{
//    //    if(Physics.Raycast(this.transform.position,CurVelocity.normalized,out RaycastHit hit, 5f))
//    //    {
//    //        CurVelocity = Vector3.ProjectOnPlane(CurVelocity, hit.normal);
//    //    }
//    //}

  

//}
