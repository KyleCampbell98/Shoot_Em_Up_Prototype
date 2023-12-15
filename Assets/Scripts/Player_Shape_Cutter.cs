using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_Shape_Cutter : MonoBehaviour
{
    private Cutter_And_Enemy_Shape_Enums.ShapeType selectedShape;
    [SerializeField] private Cutter_And_Enemy_Shape_Enums.ShapeType SelectedShape { get { return selectedShape; } set { selectedShape = value; } }
    
    [SerializeField] private int arrayIterator = 0;

    private void Awake()
    {
       
        SelectedShape = Cutter_And_Enemy_Shape_Enums.ShapeType.Circle ;
    }

    void OnCycleShapes()
    {

        if (arrayIterator >= Enum.GetNames(typeof(Cutter_And_Enemy_Shape_Enums.ShapeType)).Length)
        {
            arrayIterator = 0;
        }
       else arrayIterator++;
        // Link below is for reading further into Getting the total number of values in an ENUM/ how ENUMS work. 
        Debug.Log($"No of Enum value printed: {Enum.GetNames(typeof(Cutter_And_Enemy_Shape_Enums.ShapeType)).Length}"); // https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
