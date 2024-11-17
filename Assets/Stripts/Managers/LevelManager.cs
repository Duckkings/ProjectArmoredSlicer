using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Vector3 Gravity;
    public bool init;
    public float frictionCoefficient;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);

        }
        Physics.gravity = Gravity;  

    }
    // Start is called before the first frame update
    void Start()
    {
        if (Gravity == Vector3.zero)
        {
            Gravity = new Vector3(0f, -10f, 0f);
        }
        
        init = true;
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
