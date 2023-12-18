using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_Shape_Cutter : MonoBehaviour
{
    private Cutter_And_Enemy_Shape_Enums.ShapeType selectedShape; // Currently selected shape based on iterator.
    private Cutter_And_Enemy_Shape_Enums.ShapeType SelectedShape { get { return selectedShape; } set { selectedShape = value; Debug.Log("SHAPE CHANGED"); } }
    
    // Internal Script Logic Variables
    private int arrayIterator = 0; // iterates through the available shape enums. 
    private int numberOfPossibleShapes; // upon instantiation, sets the possible number of shapes to iterate through.

    private void Awake()
    {
        ShapeCutterListSetup();
    }  

    void OnCycleShapes()
    {
        // Link below is for reading further into Getting the total number of values in an ENUM/ how ENUMS work.
        // https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum

        SwitchActiveShape();
    }

    private void SwitchActiveShape()
    {
        if (arrayIterator == numberOfPossibleShapes - 1) // -1 due to the iterator being used on an array (indexed from 0)
        {
            arrayIterator = 0; // Resets iterator if value is greater than the potential maximum shape array index value. 
        }
        else { arrayIterator++; }

        
        switch (arrayIterator) // Selects shape based on iterator value
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


    private void ShapeCutterListSetup()
    {
        SelectedShape = Cutter_And_Enemy_Shape_Enums.ShapeType.Circle;
        Debug.Log($"Selected shape: {SelectedShape}");
        arrayIterator = 0;
        numberOfPossibleShapes = Enum.GetNames(typeof(Cutter_And_Enemy_Shape_Enums.ShapeType)).Length;
        Debug.Log($"Number of possible shapes: {numberOfPossibleShapes}");
    }

}
