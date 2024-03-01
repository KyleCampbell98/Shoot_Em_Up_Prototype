using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected_Shape_UI_Manager : MonoBehaviour
{
    private Image currently_Selected_Bomb_Sprite;

    // Start is called before the first frame update
    void Start()
    {
        Player_Bullet_Object_Pool.OnSelectedProjectileChanged += UpdateImage;
        currently_Selected_Bomb_Sprite = GetComponent<Image>();
    }

    private void UpdateImage(Sprite newImage)
    {
        currently_Selected_Bomb_Sprite.sprite = newImage;
    }

    private void OnDisable()
    {
        Player_Bullet_Object_Pool.OnSelectedProjectileChanged -= UpdateImage;
    }
}
