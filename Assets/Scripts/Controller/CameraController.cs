using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public CinemachineFreeLook FreeLookCamera;
    public Transform CameraTransform;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        FreeLookCamera = this.GetComponentInChildren<CinemachineFreeLook>();
        CameraTransform = GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FreeLookCamera.m_XAxis.Value += PlayerInputData.CameraMove.x * Time.deltaTime;
        FreeLookCamera.m_XAxis.Value += PlayerInputData.CameraMove.y * Time.deltaTime;
    }
}
