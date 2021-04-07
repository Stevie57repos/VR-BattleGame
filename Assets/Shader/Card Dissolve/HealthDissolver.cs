using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDissolver : MonoBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private Renderer[] healthRenderers = new Renderer[0];
    [SerializeField] private Renderer healthRenderer = new Renderer();

    private float targetDissolveValue = 1f;
    private float currentDissolveValue = 1f;

    private void OnEnable() => health.OnHealthChnaged += HandleHealthChanged;
    private void OnDisable() => health.OnHealthChnaged -= HandleHealthChanged;

    // Update is called once per frame
    void Update()
    {
        currentDissolveValue = Mathf.Lerp(currentDissolveValue, targetDissolveValue, 2f * Time.deltaTime);

        //foreach (Renderer renderer in healthRenderers)
        //{
        //    healthRenderers[0].material.SetFloat("_Health", currentDissolveValue);
        //}

        healthRenderer.material.SetFloat("_Health", currentDissolveValue);
    }

    private void HandleHealthChanged(int health, int maxHealth)
    {
        targetDissolveValue = (float)health / maxHealth;
    }
}
