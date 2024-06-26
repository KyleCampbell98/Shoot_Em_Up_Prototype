using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Shape_Cutter : MonoBehaviour
{
    /// <summary>
    /// This class is used for setting the player projectile's shape. Firing is handled by a different script
    /// </summary>

    private Cutter_And_Enemy_Shape_Enums.ShapeType selectedShape; // Currently selected shape based on iterator.
    private Cutter_And_Enemy_Shape_Enums.ShapeType SelectedShape { get { return selectedShape; } set { selectedShape = value; _selectedShapeEnum?.Invoke(value); } }

    public delegate void PassSelectedShapeEnum(Cutter_And_Enemy_Shape_Enums.ShapeType currentShapeTypeEnum);
    public PassSelectedShapeEnum _selectedShapeEnum;

    // Internal Script Logic Variables
    private int arrayIterator = 0; // iterates through the available shape enums. 
    private int shapeCyclerInt = 0;
    private int numberOfPossibleShapes; // upon instantiation, sets the possible number of shapes to iterate through.
    bool canCycleShapes;
    private void Awake()
    {
        ShapeCutterListSetup();
    }
    private void Start()
    {
        if (FindObjectOfType<GameManager>())
        {
            GameManager.m_GameStateChanged += CanCycleShapes;
        }
        else
        {
            Debug.LogWarning("Player Shape Cutter: no game manager found, could not assign event listeners. ");
        }

        canCycleShapes = true;
    }
    void OnCycleShapes(InputValue value)
    {
        // Link below is for reading further into Getting the total number of values in an ENUM/ how ENUMS work.
        // https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum
        if (!canCycleShapes) { return; } 
        Vector2 valueAsVector = value.Get<Vector2>();
     
        shapeCyclerInt = (int)valueAsVector.x;
        ArrayCycler();
        SwitchActiveShape();
        

    } // Listens for player action from input system
    void OnCycleShapesController(InputValue value)
    {
        var convert = value.Get<Vector2>();
        Debug.Log("Controller trigger input: " + convert.x.ToString());
    }

    private void SwitchActiveShape()
    {
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
      //  Debug.Log($"Selected shape: {SelectedShape}");
    } // Logic for switching shape

    private void ArrayCycler()
    {
        if (arrayIterator + shapeCyclerInt > numberOfPossibleShapes - 1)
        {
            arrayIterator = 0;
        }
        else if (arrayIterator + shapeCyclerInt < 0)
        {
            arrayIterator = numberOfPossibleShapes - 1;
        }
        else
        {
            arrayIterator += shapeCyclerInt;
        }
    }

    private void ShapeCutterListSetup() // Initial player weapon setup.
    {
        SelectedShape = Cutter_And_Enemy_Shape_Enums.ShapeType.Circle;
       // Debug.Log($"Selected shape: {SelectedShape}");
        arrayIterator = 0;
        numberOfPossibleShapes = Enum.GetNames(typeof(Cutter_And_Enemy_Shape_Enums.ShapeType)).Length;
      //  Debug.Log($"Number of possible shapes: {numberOfPossibleShapes}");
      
    }

    private void CanCycleShapes (GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case
                GameManager.GameState.In_Play:
                    {
                    Debug.LogWarning("Player Shape Cutter: Cycle Shapes Enabled");
                    canCycleShapes = true; break;
                }
            default: canCycleShapes = false; 
                    Debug.LogWarning("Player Shape Cutter: Cycle Shapes DISABLED");
                break;
        }
    }

    private void OnDisable()
    {
        Debug.Log("Player shape cutter disabled");
        GameManager.m_GameStateChanged -= CanCycleShapes;
    }
}
