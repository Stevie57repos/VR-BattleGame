using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBlackboard 
{
    private Dictionary<string, object> _blackboard = new Dictionary<string, object>();

    public T GetValue<T>(string key)
    {
        if(_blackboard.TryGetValue(key, out object obj))
        {
            T value = (T)obj;
            return value;
        }
        else
        {
            return default(T);
        }
    }

    public bool Contains(string key)
    {
        return _blackboard.ContainsKey(key);
    }

    public void SetValue(string key, object obj)
    {
        _blackboard[key] = obj;
    }
}
