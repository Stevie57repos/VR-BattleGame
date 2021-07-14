using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_BT : MonoBehaviour
{
    private BehaviourTree _behaviourTree;
    bool isDead = false;
    EnemyRewardController _enemyRewardController;
    public GameManagerEventChannelSO BattleFinish;
    void Start()
    {
        _behaviourTree = GetComponent<BehaviourTree>();
        _behaviourTree.BeginTree();
    }
    public void EnemyDeath()
    {
        Animator enemyAnimator = GetComponent<Animator>();
        enemyAnimator.enabled = false;
        _behaviourTree.enabled = false;
        isDead = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isDead)
        {
            BattleFinish.RaiseEvent();
            _enemyRewardController = GetComponent<EnemyRewardController>();
            _enemyRewardController.TriggerRewards();
            Debug.Log("collision with ground detected");
            Debug.Log("collided with" + collision.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
