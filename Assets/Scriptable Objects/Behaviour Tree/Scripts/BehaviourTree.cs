using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BTState 
{ 
    Running,
    Paused,
    Stopped
}

public class BehaviourTree : MonoBehaviour
{
    [SerializeField] private BehaviourNode _topNode = null;
    public GameObject Owner => _owner;
    public BTBlackboard BTBoard { get => _btBoard; set => _btBoard = value; }
    
    private GameObject _owner;
    private BTState _currentState;
    private BTBlackboard _btBoard;
    [SerializeField] CharacterRegistry _characterRegistry;

    private void OnEnable()
    {
        _owner = this.gameObject;
        _currentState = BTState.Stopped;
        _btBoard = new BTBlackboard();
        _btBoard.SetValue("Owner", _owner);
        _btBoard.SetValue("PlayerGO", _characterRegistry.Player);
        Debug.Log("player is " + _characterRegistry.Player.name);
    }

    public void BeginTree()
    {
        if(_topNode != null)
        {
            _topNode.Initialize(this);
            _currentState = BTState.Running;            
        }
    }

    void Update()
    {
        if(_currentState == BTState.Running) 
        {
            if(_topNode.Evaluate() != NodeState.Running)
            {
                _topNode.Initialize(this);
            }
        }
    }
}
