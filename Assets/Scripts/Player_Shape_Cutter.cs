using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shape_Cutter : MonoBehaviour
{
    [SerializeField] private Cutter_And_Enemy_Shape_Enums.ShapeType selectedCutOutShape;


    void OnCycleShapes()
    {
        Debug.Log("Shape cycling detected");
        var values = Enum.GetValues(typeof(Cutter_And_Enemy_Shape_Enums.ShapeType));
        Debug.Log($"Enum value printed: {values.GetValue(0).ToString()}");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
