using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    None,
    Attack1,
    Attack2,
    Attack3
}
public class PlayerAttackCombo : MonoBehaviour
{
    private AnimatorManager animatorManager;

    private bool resetTimer;
    [SerializeField]private float defaultComboTimer = 0.4f;
    private float currentComboTimer;

    private ComboState currentComboState;
    void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }

     void Start()
    {
        currentComboTimer = 0;
        currentComboState = ComboState.None;
    }
    // Update is called once per frame
    void Update()
    {
        AttackCombo();
        ResetComboState();
    }

    void AttackCombo()
    {
        //Attack Combo
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentComboState++;
            resetTimer = true;
            currentComboTimer = defaultComboTimer;

            if(currentComboState == ComboState.Attack1)
            {
                animatorManager.Attack1();
            }
            if (currentComboState == ComboState.Attack2)
            {
                animatorManager.Attack2();
            }
            if (currentComboState == ComboState.Attack3)
            {
                animatorManager.Attack3();
            }
        }

    }

    void ResetComboState()
    {
        if(resetTimer)
        {
            currentComboTimer -= Time.deltaTime;
            if(currentComboTimer <= 0)
            {
                currentComboState = ComboState.None;
                resetTimer = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }
}