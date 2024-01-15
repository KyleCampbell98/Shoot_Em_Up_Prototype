using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shape_Info")]
public class Shape_Info : ScriptableObject
{
   [SerializeField] private Cutter_And_Enemy_Shape_Enums.ShapeType shapeType;
   [SerializeField] private Sprite _sprite;

    public Cutter_And_Enemy_Shape_Enums.ShapeType ShapeType {  get { return shapeType; } }
    public Sprite Sprite { get { return _sprite; } }
}
