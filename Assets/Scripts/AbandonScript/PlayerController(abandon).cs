//using System.Collections;
//using System.Collections.Generic;
//using Unity.Mathematics;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    //���ڿ������
//    public static PlayerController instance;
//    public CharacterController characterController;
//    public BoosterBaseData boosterBaseData;
//    public ASBaseData ASBaseData;
//    public Vector3 CurVelocity;
//    public Vector3 CurHorizontalVelocity;
//    public Vector3 CurVerticalVelocity;
//    public Vector3 TargetVelocity;
//    public Vector3 Gravity;
//    public float airDragBouns;
//    public float airHorizontalDrag;
//    public float airVerticalDrag;
//    public Vector3 NormalForce;
//    public Vector3 InputForce;
//    public float frictionForce;
//    public Vector3 MoveInput;
//    public Vector3 JumpInput;
//    public Vector3 LastMoveInput;
//    public Vector3 LastJumpInput;
//    public bool DashInput;
//    public bool isGrounded;
//    public Transform playerTransform;
//    public float CameraRotationSpeed;
//    public float xAxieLimitAngle;
//    public float yAxieLimitAngle;
//    public float groundDistance = 1f;

//    public Transform CurLookPositionTransform;
//    public Transform LookPositionTransform;

//    private bool lastIsGrounded = false;

//    public delegate void PlayerControllerDelegate();
//    public event PlayerControllerDelegate EntityIsGrounded;
//    public event PlayerControllerDelegate Init;
//    public event PlayerControllerDelegate PlayerInput;


//    private float TargetXRotation = 0f;
//    private float TargetYRotation;
//    private float CurXRotation;
//    private float CurYRotation;
//    private float smoothMouseX;
//    private float smoothMouseY;
//    public float smoothingSpeed;
//    private Transform Transform;
//    private bool TurnForward = false;

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

//        playerTransform = GetComponent<Transform>();
//        characterController = GetComponentInParent<CharacterController>();
//        Transform = GetComponent<Transform>();
//        PlayerInput+=StartTurnForward;
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        CurVelocity = Vector3.zero;
//        TargetVelocity = Vector3.zero;
//        Gravity = LevelManager.Instance.Gravity;
//        airHorizontalDrag = 0;
//        airVerticalDrag = 0;
//        if (airDragBouns == 0)
//        {
//            airDragBouns = 0.1f;
//        }
//        NormalForce = ASBaseData.MechMass * Gravity;
//        Init?.Invoke();


//    }

//    // Update is called once per frame
//    void Update()
//    {
//        IsGroundedDetect();
//        //PointToCamera();
//        PlayerViewMovement();
//        PlayerRotateToForward();

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

//        CurVelocity = CurHorizontalVelocity + CurVerticalVelocity;
//        characterController.Move(CurVelocity * Time.deltaTime);
//        //CurVelocity = characterController.velocity;
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
//        characterController.Move(CurVelocity * Time.deltaTime);
//        //ColliderDetect();
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
//        characterController.Move(CurVelocity * Time.deltaTime);
//        //ColliderDetect();
//    }


//    public void AllowInput()
//    {
//        MoveInput = PlayerInputData.MoveInput;
//        MoveInput = playerTransform.TransformDirection(MoveInput);
//        JumpInput = PlayerInputData.JumpInput;
//        DashInput = PlayerInputData.DashInput;
//        PlayerInput?.Invoke();
//    }


//    void HorizontalVelocity()
//    {
//        //Vector3 HorizontalForce = MoveInput * boosterBaseData.BoosterHorizontalThrust - new Vector3(CurHorizontalVelocity.normalized.x, 0f, CurHorizontalVelocity.normalized.z) * (airHorizontalDrag + frictionForce);
//        //CurHorizontalVelocity += HorizontalForce / ASBaseData.MechMass * Time.deltaTime;

//        Vector3 DesiredHorizontalVelocity=MoveInput* boosterBaseData.BoosterHrizontalMaxSpeed;
//        CurHorizontalVelocity=Vector3.Lerp(CurHorizontalVelocity, DesiredHorizontalVelocity, Time.deltaTime *1f);
//    }

//    public void HorizontalBoost()
//    {
//        Vector3 HorizontalForce = MoveInput * boosterBaseData.BoosterHorizontalBurstThrust - new Vector3(CurHorizontalVelocity.normalized.x, 0f, CurHorizontalVelocity.normalized.z) * (airHorizontalDrag + frictionForce);
//        CurHorizontalVelocity += HorizontalForce / ASBaseData.MechMass * Time.deltaTime;

//    }



//    void VerticalVelocity()
//    {
//        Vector3 VerticalForce = JumpInput * boosterBaseData.BoosterVerticalThrust - new Vector3(0f, CurVerticalVelocity.normalized.y, 0f) * airVerticalDrag + ASBaseData.MechMass * Gravity;
//        CurVerticalVelocity += VerticalForce / ASBaseData.MechMass * Time.deltaTime;
//        if (isGrounded)
//        {
//            CurVerticalVelocity = Vector3.zero;
//        }

//    }

//    void VerticalBoost()
//    {
//        Vector3 VerticalForce = JumpInput * boosterBaseData.BoosterVerticalBurstThrust - new Vector3(0f, CurVerticalVelocity.normalized.y, 0f) * airVerticalDrag + ASBaseData.MechMass * Gravity;
//        CurVerticalVelocity += VerticalForce / ASBaseData.MechMass * Time.deltaTime;

//    }

//    void VelocitySplit()
//    {
//        CurHorizontalVelocity = new Vector3(CurVelocity.x, 0f, CurVelocity.z);
//        CurVerticalVelocity = new Vector3(0f, CurVelocity.y, 0f);
//    }



//    public void FrictionForce()
//    {
//        if (isGrounded)
//        {
//            frictionForce = LevelManager.Instance.frictionCoefficient * NormalForce.magnitude;
//            Gravity = Vector3.zero;

//        }
//        else
//        {
//            frictionForce = 0;
//            Gravity = LevelManager.Instance.Gravity;
//        }
//    }

//    public void AirDragHorizontalForce()
//    {
//        airHorizontalDrag = CurHorizontalVelocity.magnitude * CurHorizontalVelocity.magnitude * airDragBouns;
//    }

//    public void AirDragVerticalForce()
//    {
//        airVerticalDrag = CurVerticalVelocity.magnitude * CurVerticalVelocity.magnitude * airDragBouns;
//    }

//    public void InputCache()
//    {
//        LastJumpInput = JumpInput;
//        LastMoveInput = MoveInput;

//    }

//    //private void ColliderDetect()
//    //{
//    //    if (Physics.Raycast(this.transform.position, CurVelocity.normalized, out RaycastHit hit, 5f))
//    //    {
//    //        CurVelocity = Vector3.ProjectOnPlane(CurVelocity, hit.normal);
//    //    }
//    //}

//    private void OnControllerColliderHit(ControllerColliderHit hit)
//    {
//        if (Vector3.Dot(hit.normal, Vector3.up) < 0.1f)
//        {
//            // �� CurVelocity ������ײ����ͶӰ��ƽ���ϣ�ȥ���������
//            CurVelocity = Vector3.ProjectOnPlane(CurVelocity, hit.normal);
//        }


//    }

//    private void IsGroundedDetect()
//    {
        

//        if (JumpInput!=Vector3.zero)
//        {
//            isGrounded = false;
//        }
//        else
//        {
//            isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance);
//        }

        

//        if(isGrounded && !lastIsGrounded)
//        {
//            EntityIsGrounded?.Invoke();
//        }
//        lastIsGrounded = isGrounded;


//    }


//    private void PlayerViewMovement()
//    {
//        // ��ȡԭʼ���������
//        float targetMouseX = PlayerInputData.CameraMove.x;
//        float targetMouseY = PlayerInputData.CameraMove.y;

//        // ʹ�� Lerp ƽ���������
//        smoothMouseX = Mathf.Lerp(smoothMouseX, targetMouseX, smoothingSpeed * Time.deltaTime);
//        smoothMouseY = Mathf.Lerp(smoothMouseY, targetMouseY, smoothingSpeed * Time.deltaTime);

//        float yRotation = smoothMouseX * CameraRotationSpeed * Time.deltaTime;
//        float xRotation = -smoothMouseY * CameraRotationSpeed * Time.deltaTime;

//        Vector3 TargetEulerAngle = CurLookPositionTransform.rotation.eulerAngles;
        

//        TargetXRotation += xRotation;
//        TargetXRotation = Mathf.Clamp(TargetXRotation, -xAxieLimitAngle, xAxieLimitAngle);
//        TargetEulerAngle.x = TargetXRotation;
//        TargetYRotation += yRotation;
//        TargetEulerAngle.y = TargetYRotation;

//        CurXRotation = Mathf.Lerp(CurXRotation, TargetXRotation, 10f * Time.deltaTime);
//        CurYRotation = Mathf.Lerp(CurYRotation, TargetYRotation, 10f * Time.deltaTime);

//        // Ӧ����ת����ҵ�Transform
//        //playerTransform.Rotate(0, 0, 0);
//        LookPositionTransform.rotation= Quaternion.Euler(TargetEulerAngle);
//        CurLookPositionTransform.rotation = Quaternion.Euler(CurXRotation, CurYRotation,0f);


//    }

//    private void PlayerRotateToForward()
//    {
//        Vector3 CameraLocalAngle = CurLookPositionTransform.localEulerAngles;
//        Vector3 CameraWorldAngle = CurLookPositionTransform.eulerAngles;
//        Vector3 ThisWorldAngle = transform.eulerAngles;

       

//        CameraLocalAngle = CaculateDataBase.CaculateEuler(CameraLocalAngle);
//        ThisWorldAngle = CaculateDataBase.CaculateEuler(ThisWorldAngle);
//        CameraWorldAngle = CaculateDataBase.CaculateEuler(CameraWorldAngle);
//        float angleDifference = Mathf.DeltaAngle(ThisWorldAngle.y, CameraWorldAngle.y);
//        // ������ת���ݶ�����ֱ������Transform
//        //Quaternion cameraRotation = CurLookPositionTransform.rotation;

//        // �Ը��Ƶ���ת�����޸�
//        //CameraWorldAngle = new Vector3(0f, CameraWorldAngle.y, 0f);
//        if (TurnForward)
//        {
//            float yRotation = Mathf.LerpAngle(ThisWorldAngle.y, CameraWorldAngle.y, 10f * Time.deltaTime);
//            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
//        }

//        if (CameraLocalAngle.y> 90f || CameraLocalAngle.y<-90f)
//        {
//            StartTurnForward();


//        }
        
//        if (Mathf.Abs(angleDifference) < 0.1f)
//        {
//            TurnForward = false;
//        }


//    }

//    private void StartTurnForward()
//    {
//        TurnForward = true;
//    }



//}
