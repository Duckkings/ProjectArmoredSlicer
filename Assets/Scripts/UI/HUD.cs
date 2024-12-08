using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class HUD : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Awake()
    {
        text=this.GetComponentInChildren<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Speed = PlayerStatusData.Velocity.magnitude*3.6f;
        text.text= $"{(int)Speed} KM/H";
    }
}
