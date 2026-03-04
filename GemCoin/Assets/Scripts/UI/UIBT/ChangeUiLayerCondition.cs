using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "ChangeUILayer", story: "[TargetUI] is [NotEqual] to [CurrentUI] [and] [IsTransitons] is [False]", category: "Conditions", id: "aee0edd5912a3ade328c15833b18d7c0")]
public partial class ChangeUiLayerCondition : Condition
{
    [SerializeReference] public BlackboardVariable<HubUIState> TargetUI;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> NotEqual;
    [SerializeReference] public BlackboardVariable<HubUIState> CurrentUI;
    [Comparison(comparisonType: ComparisonType.Boolean)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> And;
    [SerializeReference] public BlackboardVariable<bool> IsTransitons;
    [SerializeReference] public BlackboardVariable<bool> False;

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
