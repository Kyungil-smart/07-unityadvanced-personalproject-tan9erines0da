using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/ESC_Pressed")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "ESC_Pressed", message: "On CancelInput", category: "Events", id: "5686615139d85543b76457cd6cbcc635")]
public sealed partial class EscPressed : EventChannel { }

