using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRegisterHandler : MonoBehaviour
{
    [SerializeField] CharacterRegistry _characterRegistry;
    private string _objectTag;

    private void OnEnable()
    {
        RegisterThisObject(this.gameObject);
    }

    private void OnDisable()
    {
        DeregisterThisObject(this.gameObject);
    }

    void RegisterThisObject(GameObject GO)
    {
        _objectTag = GO.tag.ToString();
        if (_objectTag == "Player")
            _characterRegistry.Player = this.gameObject;
        else
            _characterRegistry.CurrentEnemy = this.gameObject;
    }

    void DeregisterThisObject(GameObject GO)
    {
        _objectTag = GO.tag.ToString();
        if (_objectTag == "Player")
            _characterRegistry.Player = null;
        else
            _characterRegistry.CurrentEnemy = null;
    }
}
