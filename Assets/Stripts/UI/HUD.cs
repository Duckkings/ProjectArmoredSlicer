using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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
        if (PlayerController.instance.characterController != null)
        {
            text.text = Vector3.Magnitude(PlayerController.instance.characterController.velocity).ToString("F0") + Vector3.Magnitude(PlayerController.instance.CurHorizontalVelocity).ToString("F0") + Vector3.Magnitude(PlayerController.instance.CurVerticalVelocity).ToString("F0");

        }
        else
        {
            text.text = "N/A"; // 或者其他适当的默认值
        }
    }
}
