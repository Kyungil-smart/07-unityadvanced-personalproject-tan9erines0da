using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Slide Action", story: "Slide [InOut]", category: "Action", id: "0f91781d22de8e77fc032b35d83b34a8")]
public partial class SlideAction : Action
{
    [SerializeReference] public BlackboardVariable<string> InOut;

    protected override Status OnStart()
    {
        TransitionManager.Instance.PlayTransition(InOut);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

