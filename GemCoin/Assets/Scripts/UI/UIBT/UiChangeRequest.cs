using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/UIChangeRequest")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "UIChangeRequest", message: "Change Target UI", category: "Events", id: "5553773e68f7d08efbb94323ce760b9c")]
public sealed partial class UiChangeRequest : EventChannel { }

