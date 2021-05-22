using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDissolve : MonoBehaviour
{
    [SerializeField] private Renderer _cardRenderer = new Renderer();

    [SerializeField] float timeElapsed = 0f;
    [SerializeField] float lerpDuration = 3.5f;
    [SerializeField] float startValue = 0f;
    [SerializeField] float endValue = 0.5f;
    [SerializeField] float valueToLerp;
    ParticleSystem DissolvePS;

    private void Start()
    {
        DissolvePS = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        Spawn();
    }
    //public void BeginDissolve()
    //{
    //    Debug.Log("coroutine starting");
    //    StartCoroutine(Dissolve());
    //}

    //IEnumerator Dissolve()
    //{
    //    while (timeElapsed < lerpDuration)
    //    {
    //        //Debug.Log("running lerp");
    //        valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
    //        _cardRenderer.material.SetFloat("_Dissolve", (1f - valueToLerp ));
    //        timeElapsed += Time.deltaTime;

    //        yield return null;
    //    }     
    //    valueToLerp = endValue;
    //    DissolvePS.Play();  
    //}

    public void Spawn()
    {
        timeElapsed = 0f;
        StartCoroutine(SpawnEffect());
    }
    IEnumerator SpawnEffect()
    {
        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            _cardRenderer.material.SetFloat("_Dissolve", valueToLerp);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valueToLerp = endValue;
    }

    public void Dissolve()
    {
        timeElapsed = 0f;
        StartCoroutine(DissolveEffect());
    }

    IEnumerator DissolveEffect()
    {
        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            _cardRenderer.material.SetFloat("_Dissolve", (1-valueToLerp));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valueToLerp = endValue;
    }
}
