using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Shape_Cutter : MonoBehaviour
{
    private Cutter_And_Enemy_Shape_Enums.ShapeType selectedShape; // Currently selected shape based on iterator.
    private Cutter_And_Enemy_Shape_Enums.ShapeType SelectedShape { get { return selectedShape; } set { selectedShape = value; _selectedShapeEnum?.Invoke(value); } }

    public delegate void PassSelectedShapeEnum(Cutter_And_Enemy_Shape_Enums.ShapeType currentShapeTypeEnum);
    public PassSelectedShapeEnum _selectedShapeEnum;

    // Internal Script Logic Variables
    private int arrayIterator = 0; // iterates through the available shape enums. 
    private int shapeCyclerInt = 0;
    private int numberOfPossibleShapes; // upon instantiation, sets the possible number of shapes to iterate through.

    private void Awake()
    {
        ShapeCutterListSetup();
    }  

    void OnCycleShapes(InputValue value)
    {
        // Link below is for reading further into Getting the total number of values in an ENUM/ how ENUMS work.
        // https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum
        Vector2 valueAsVector = value.Get<Vector2>();
        shapeCyclerInt = (int)valueAsVector.x;
        SwitchActiveShape();
    } // Listens for player action from input system

    private void SwitchActiveShape()
    {
        if (arrayIterator == numberOfPossibleShapes - 1) // -1 due to the iterator being used on an array (indexed from 0)
        {
            arrayIterator = 0; // Resets iterator if value is greater than the potential maximum shape array index value. 
        }
        else if(arrayIterator < 0)
        {
            arrayIterator = numberOfPossibleShapes - 1; // cylces back to the last option if trying to go backwards from the start of the array
        }
        else { arrayIterator += shapeCyclerInt; }

        
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
    } // Logic for switching shape


    private void ShapeCutterListSetup() // Initial player weapon setup.
    {
        SelectedShape = Cutter_And_Enemy_Shape_Enums.ShapeType.Circle;
        Debug.Log($"Selected shape: {SelectedShape}");
        arrayIterator = 0;
        numberOfPossibleShapes = Enum.GetNames(typeof(Cutter_And_Enemy_Shape_Enums.ShapeType)).Length;
        Debug.Log($"Number of possible shapes: {numberOfPossibleShapes}");
    }

}
