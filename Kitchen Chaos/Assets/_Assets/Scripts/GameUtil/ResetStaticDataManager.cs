using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResestStaticData();
        BaseCounter.ResestStaticData(); 
        TrashCounter.ResestStaticData();
    }
}
