using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    
    public enum CampType
    {
        Player,
        Enemy
    }

    public CampType campType;
    // Start is called before the first frame update
    void Start()
    {
        CampManager.instance.RegisterCamp(this);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
