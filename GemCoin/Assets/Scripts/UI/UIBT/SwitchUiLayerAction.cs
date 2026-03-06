using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "SwitchUILayerAction", 
    story: "Switch to Target [UI] Layer", 
    category: "Action", 
    id: "d4a18dffa4c4adc997a522a112ce45a5")]
public partial class SwitchUiLayerAction : Action
{
    [SerializeReference] public BlackboardVariable<HubUIState> UI;
    protected override Status OnStart()
    {
        // 임시 수정 필요----------
        var manager = GameObject.FindObjectOfType<MainHubUIManger>();
        //---------------------
        if (manager != null)
        {
            manager.SetLayerActive( UI.Value);
            return Status.Success;
        }
        return Status.Failure;
    }

    // protected override Status OnUpdate()
    // {
    //     return Status.Success;
    // }

    protected override void OnEnd()
    {
    }
}

