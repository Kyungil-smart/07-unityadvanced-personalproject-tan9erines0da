using UnityEngine;

[CreateAssetMenu(fileName = "SilverCoinLogic", menuName = "Scriptable Objects/Treasure/SilverCoinPickup")]
public class SilverCoinLogic : TreasureProcessor
{
    public IntEventChannelSO coinValue;
    public IntEventChannelSO coinCount;
    public IntEventChannelSO silverCoinCount;
    
    public override void Pickup(TreasureDataSO data)
    {
        Debug.Log($"data coin{data.coin}");
        coinValue?.RaiseEvent(data.coin);
        coinCount?.RaiseEvent(1);
        silverCoinCount?.RaiseEvent(1);
    }
}
