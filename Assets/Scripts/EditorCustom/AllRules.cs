using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class AllRules : MonoBehaviour, IRulesMediator
{
    [SerializeField, InterfaceType(typeof(IRule))]
    private Object[] rules;
    private List<IRule> Rules
    {
        get
        {
            var localRule = new List<IRule>();
            foreach (var rule in rules)
            {
                localRule.Add(rule as IRule);
            }
            return localRule;
        }
    }

    private void Start()
    {
        foreach (var rule in Rules)
        {
            rule.Config(this);
        }
    }
}