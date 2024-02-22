using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shape_Info")]
public class Shape_Info : ScriptableObject
{
   [SerializeField] private Cutter_And_Enemy_Shape_Enums.ShapeType shapeType;
   [SerializeField] private Sprite _sprite;

    public Cutter_And_Enemy_Shape_Enums.ShapeType ShapeType {  get { return shapeType; } } // This can be used to activate animation triggers upon enemy creation. use the shape type as "ToString",
    // then make sure the trigger matches the enum strings So instead of "SetTrigger("Triangle"), use ("SetTrigger(ShapeType.ToString())".
    public Sprite Sprite { get { return _sprite; } }
}
