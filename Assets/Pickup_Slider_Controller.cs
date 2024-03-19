using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Pickup_Slider_Controller : MonoBehaviour
{
    [Header("HUD element Refs")]
    [SerializeField] private Slider pickupDropProgressBar;
    [SerializeField] private Animator sliderAnimator;

    private const string enemyGoalHitAnim_param = "Enemy_Goal_Hit";

    public static Action a_ResetSlider;

    private void Awake()
    {
        pickupDropProgressBar = GetComponent<Slider>();
        sliderAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupSlider();
        EventSubscriptions();
    }

    private void EventSubscriptions()
    {
        GameManager.a_PlayerDefeatedEnemy += UpdateSliderOnEnemyKill;
        a_ResetSlider += ResetSlider;
    }

    private void SetupSlider()
    {
        Debug.Log("Pickup Slider Controller: Setup slider.");
        pickupDropProgressBar.maxValue = GameManager.EnemyDefeatsNeededForNextHPDrop;
        pickupDropProgressBar.value = 0;
    }

    private void UpdateSliderOnEnemyKill()
    {
        Debug.Log("Pickup Slider Controller: Update slider on Enemy kill called.");
        pickupDropProgressBar.value++;
        if(pickupDropProgressBar.value >= pickupDropProgressBar.maxValue)
        {
            SliderFlash();
        }
    }

    private void ResetSlider()
    {
        Debug.Log("Pickup Slider Controller: Reset Slider Called.");
        sliderAnimator.SetBool(enemyGoalHitAnim_param, false);
        pickupDropProgressBar.value = 0;
        pickupDropProgressBar.maxValue = GameManager.EnemyDefeatsNeededForNextHPDrop;
    }

    private void SliderFlash()
    {
        Debug.Log("Pickup Slider Controller: Slider flash anim bool set to: True.");
        sliderAnimator.SetBool(enemyGoalHitAnim_param, true);
    }

    private void OnDisable()
    {
        GameManager.a_PlayerDefeatedEnemy -= UpdateSliderOnEnemyKill;
    }
}
