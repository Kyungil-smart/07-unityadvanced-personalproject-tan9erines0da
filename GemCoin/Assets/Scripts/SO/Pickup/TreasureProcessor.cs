using UnityEngine;

public abstract class TreasureProcessor : ScriptableObject
{
    public abstract void Pickup(TreasureDataSO data);
}
