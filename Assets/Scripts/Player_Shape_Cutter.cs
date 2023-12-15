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
    private int numberOfPossibleShapes;

    private void Awake()
    {
       
        SelectedShape = Cutter_And_Enemy_Shape_Enums.ShapeType.Circle;
        Debug.Log($"Selected shape: {SelectedShape}");

        numberOfPossibleShapes = Enum.GetNames(typeof(Cutter_And_Enemy_Shape_Enums.ShapeType)).Length;
        Debug.Log($"Number of possible shapes: {numberOfPossibleShapes}");
    }

    void OnCycleShapes()
    {
        // Link below is for reading further into Getting the total number of values in an ENUM/ how ENUMS work.
        // https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum

        if (arrayIterator == numberOfPossibleShapes - 1)
        {
            arrayIterator = 0;
        }
        else { arrayIterator++; }
        Debug.Log($"Iterator value is now: {arrayIterator}");
        switch (arrayIterator)
        {
            case 0:
                SelectedShape = Cutter_And_Enemy_Shape_Enums.ShapeType.Circle;
                break;

                case 1:
                SelectedShape = Cutter_And_Enemy_Shape_Enums.ShapeType.Square;
                break;

                case 2:
                SelectedShape = Cutter_And_Enemy_Shape_Enums.ShapeType.Triangle;
                break;

            default:
                Debug.LogWarning("Enum list contains more values than shape selector switch case has scenarios for. UPDATE \"OnCycleShapes\" method.");
                break;
        }
        Debug.Log($"Selected shape: {SelectedShape}");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
