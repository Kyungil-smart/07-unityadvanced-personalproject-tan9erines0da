using UnityEngine;

[CreateAssetMenu(fileName = "SilverCoinLogic", menuName = "Scriptable Objects/Treasure/SilverCoinPickup")]
public class SilverCoinLogic : TreasureProcessor
{
    public IntEventChannelSO coinValue;
    public IntEventChannelSO coinCount;
    public IntEventChannelSO silverCoinCount;
    
    public override void Pickup(TreasureDataSO data)
    {
        coinValue.OnEventRaised(data.coin);
        coinCount.OnEventRaised(1);
        silverCoinCount.OnEventRaised(1);
    }
}
