using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveV2 : MonoBehaviour
{
    [SerializeField] private Renderer _cardRenderer = new Renderer();
    [SerializeField] float timeElapsed;
    [SerializeField] float lerpDuration;
    [SerializeField] float startValue;
    [SerializeField] float endValue;
    [SerializeField] float valueToLerp;

    public void BeginDissolve()
    {
        timeElapsed = 0f;
        lerpDuration = 1.75f;
        startValue = 5f;
        endValue = 0f;
        _cardRenderer = GetComponent<Renderer>();
        StartCoroutine(Dissolve());
        Debug.Log("begin dissolve called");
    }

    IEnumerator Dissolve()
    {
        while ( timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            _cardRenderer.material.SetFloat("_EdgeWidth", (valueToLerp - 1));
            timeElapsed += Time.fixedDeltaTime;
            yield return null;
        }
    }
}
