using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected_Shape_UI_Manager : MonoBehaviour
{
    [SerializeField] private Image currently_Selected_Bomb_Sprite;
    [SerializeField] private Sprite defaultDisplaySprite;
    bool initialSetupComplete;
    private void Awake()
    {
        currently_Selected_Bomb_Sprite = GetComponent<Image>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Player_Bullet_Object_Pool.OnSelectedProjectileChanged += UpdateImage;
        currently_Selected_Bomb_Sprite.sprite = defaultDisplaySprite;
        initialSetupComplete = true;

    }

    private void OnEnable()
    {
        if (!initialSetupComplete)
        {
            return;
        }
        else
        {
            Player_Bullet_Object_Pool.OnSelectedProjectileChanged += UpdateImage;
        }
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
