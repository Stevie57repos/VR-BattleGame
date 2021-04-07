using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    public event Action<int, int> OnHealthChnaged;
    private int currentHealth;


    [SerializeField] private ActionOnTimer actionOnTimer;
    bool hasTimerElapsed = false;





    private void Start()
    {
        actionOnTimer.SetTimer(1f);

        SetHealth(maxHealth);
    }


    void SetHealth(int value)
    {
        currentHealth = value;
        OnHealthChnaged?.Invoke(currentHealth, maxHealth);
    }

    private void Update()
    {

        if (!hasTimerElapsed && actionOnTimer.IsTimerComplete())
        {
            Debug.Log("Timer complete!");
            hasTimerElapsed = true;
        }

        if (!Keyboard.current.spaceKey.wasPressedThisFrame) { return; }
        DealDamage(10);
        Debug.Log("dealt 10 damage");
        Debug.Log("current health is " + currentHealth);







    }

    void DealDamage(int damageAmount)
    {
        SetHealth(Mathf.Max(currentHealth - damageAmount, 0));
    }
}
