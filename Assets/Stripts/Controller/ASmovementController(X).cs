using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ASmovementController : MonoBehaviour
//{
//    public static ASmovementController Instance;
//    public BoosterBaseData BoosterBaseData;
//    public ASBaseData ASBaseData;
//    CharacterController controller;
//    private float HorizontalSpeed;
//    private float VerticalSpeed;
//    private Vector3 HorizontalVelocity;
//    private Vector3 VerticalVelocity;
//    //Animator anim
//    private Vector3 moveDirHorizontal;
//    private Vector3 moveDirVertical;
//    public bool isGrounded;
//    private float JumpDir;
//    public Rigidbody Rigidbody;
//    void Awake()
//    {
//        if(Instance == null)
//        {
//            Instance = this;
//        }
//        else if (Instance != this)
//        {
//            Destroy(gameObject);
//        }
//        //获取角色控制器
//        controller = GetComponent<CharacterController>();
//        Rigidbody = GetComponent<Rigidbody>();
//        //获取动画控制器
//        //anim = GetComponent<Animator>();

//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        moveDirHorizontal = Vector3.zero;
//        moveDirVertical = Vector3.zero;
//        HorizontalVelocity = Vector3.zero;
//        VerticalVelocity = Vector3.zero;
//        HorizontalSpeed = 0f;
//        VerticalSpeed = 0f;

//        Physics.gravity= new Vector3(0f, LevelManager.Instance.Gravity, 0f);
//        Debug.Log(Physics.gravity);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        moveDirHorizontal = new Vector3(InputManager.instance.MoveInput.x, 0f, InputManager.instance.MoveInput.z);

//        moveDirVertical = new Vector3(0f, InputManager.instance.JumpInput, 0f);

//        isGrounded = controller.isGrounded;
//        if (moveDirVertical != Vector3.zero)
//        {
//            Rigidbody.AddForce(moveDirVertical * BoosterBaseData.BoosterVerticalThrust);
//        }


//        //Debug.Log((Rigidbody.velocity));
//    }

//    void FixedUpdate()
//    {

//        if (moveDirHorizontal != Vector3.zero)
//        {
//            Rigidbody.AddForce(moveDirHorizontal * BoosterBaseData.BoosterHorizontalThrust);



//        }


        

//        HorizontalVelocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);
//        VerticalVelocity = new Vector3(0f, Rigidbody.velocity.y, 0f);
//        HorizontalVelocity = Vector3.ClampMagnitude(HorizontalVelocity, BoosterBaseData.BoosterHrizontalMaxSpeed);
//        VerticalVelocity = Vector3.ClampMagnitude(VerticalVelocity, BoosterBaseData.BoosterVerticalMaxSpeed);
//        Rigidbody.velocity = HorizontalVelocity + VerticalVelocity;
//    }
//}
