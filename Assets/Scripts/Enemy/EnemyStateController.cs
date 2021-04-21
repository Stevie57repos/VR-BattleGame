using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypeSelected { None, Attack, SpellDamage, Defend, SpellHeal}

public class EnemyStateController : MonoBehaviour
{
    public CardTypeSelected _cardType;
    public EnemyCharacter _enemyCharacter;
    // set intial state in inspector
    public EnemyState currentState;
    public EnemyManager enemyManager;
    public EnemyProjectileHandler enemyProjectileHandler;
    public EnemyState maintainState;
    public GameObject playerTarget;

    Dictionary<string, CardTypeSelected> CardTypeDictionary = new Dictionary<string, CardTypeSelected>();

    private void Awake()
    {
        _enemyCharacter = GetComponent<EnemyCharacter>();
        enemyProjectileHandler = GetComponent<EnemyProjectileHandler>();
        LoadDictionary();
    }

    private void LoadDictionary()
    {
        CardTypeDictionary.Add("Attack", CardTypeSelected.Attack);
        CardTypeDictionary.Add("Defend", CardTypeSelected.Defend);
        CardTypeDictionary.Add("Spell", CardTypeSelected.SpellDamage);
        CardTypeDictionary.Add("Draw", CardTypeSelected.SpellHeal);
    }

    void Start()
    {
        enemyProjectileHandler.EnemyProjectilePool = enemyManager.EnemyProjectilePool;
        currentState.EnterState(this);
        _cardType = CardTypeSelected.None;
    }

    void Update()
    {
        currentState.UpdateStateActions(this);
    }
    public void TransitionToState(EnemyState nextState)
    {
        if(nextState != maintainState)
        {
            currentState = nextState;
            currentState?.EnterState(this);
        }
    }

    public void EnemyProjectileActionUpdate(Action[] actionsList, EnemyStateController controller)
    {
        StartCoroutine(EnemyActionCoroutine(actionsList, controller));
    }

    IEnumerator EnemyActionCoroutine(Action[] updateActionsList, EnemyStateController controller)
    {
        Debug.Log($"IEnumberator Start has begun");
        yield return new WaitForSeconds(3);

        for (int i = 0; i < updateActionsList.Length; i++)
        {
            updateActionsList[i].Act(controller);

            Debug.Log($"should stop before next action is implemented");
            yield return new WaitForSeconds(5);
        }
        Debug.Log($"IEnumberator Start has finished");
    }

    public void EventCardSelected()
    {
        _cardType = CardTypeDictionary[GameEventsHub.CardSelected.CardTypeString];
    }
}
