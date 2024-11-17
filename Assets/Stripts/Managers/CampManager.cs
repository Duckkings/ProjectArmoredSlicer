using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampManager : MonoBehaviour
{
    public static CampManager instance;
    // Start is called before the first frame update

    private Dictionary<Camp.CampType, List<GameObject>> camps = new Dictionary<Camp.CampType, List<GameObject>>();
    private Dictionary<Camp.CampType, int> campsCount = new Dictionary<Camp.CampType, int>();
    private Dictionary<Camp.CampType, int> lastCampsCount = new Dictionary<Camp.CampType, int>();


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Destroy(this);
    }

    private void Update()
    {
        
    }

    public void RegisterCamp(Camp camp)
    {
        if (!camps.ContainsKey(camp.campType))
        {
            camps[camp.campType] = new List<GameObject>();
        }
        if (!campsCount.ContainsKey(camp.campType))
        {
            campsCount[camp.campType] = new int();
        }
        if (!lastCampsCount.ContainsKey(camp.campType))
        {
            lastCampsCount[camp.campType] = new int();
        }
        camps[camp.campType].Add(camp.gameObject);
        campsCount[camp.campType] = camps[camp.campType].Count;
        
    }

    public void UnregisterCamp(Camp camp)
    {
        if (camps.ContainsKey(camp.campType))
        {
            camps[camp.campType].Remove(camp.gameObject);
        }
        if (campsCount.ContainsKey(camp.campType))
        {
            campsCount.Remove(camp.campType);
        }

        if (camps[camp.campType] == null)
        {
            camps.Remove(camp.campType);
        }
        
    }

    public void ClearCamp(Camp.CampType campType)
    {
        if (camps.ContainsKey(campType))
        {
            camps[campType].Clear();
        }
    }

    public List<GameObject> GetCampObject(Camp.CampType campType) 
    {
        return camps.ContainsKey(campType) ? camps[campType] : new List<GameObject>();
    }

    public bool CampCountChange(Camp.CampType campType)
    {
        if (lastCampsCount[campType] != campsCount[campType])
        {
            lastCampsCount[campType] = campsCount[campType];
            return true;
        }
        return false;
    }


}
