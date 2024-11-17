using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnManager : MonoBehaviour
{
    public static LockOnManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LockOn()
    {
        

    }
}