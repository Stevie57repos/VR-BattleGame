using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/TestAction1")]
public class TestAction : Action
{
    public int _projectileNumber;
    public int _timeBetweenProjectiles;
    public string _text;

    public override void Act(EnemyStateController controller)
    {
        Debug.Log(_text);
    }

    public int ProjectileNumber()
    {
        return _projectileNumber;
    }

    public int TimeBetweenProjectiles()
    {
        return _timeBetweenProjectiles;
    }

    public string Text()
    {
        return _text;
    }


}
