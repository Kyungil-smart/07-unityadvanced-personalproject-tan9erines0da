using UnityEngine;

public class RuntimeSession : MonoBehaviour
{
    // 달리는 동안 필요한 데이터를 관리하는 컴포넌트
    [Header("Broadcast Channels")]
    [SerializeField] private IntEventChannelSO _coinUpdateChannel;
    [SerializeField] private IntEventChannelSO _hpUpdateChannel;
    [SerializeField] private IntEventChannelSO _scoreUpdateChannel;
    
    [Header("Listen Channels")]

    // 실시간 데이터 (Local)
    private int _currentCoin;
    private int _currentHP;
    private int _currentScore;

    // 코인을 추가하는 로직
    public void AddCoin(int amount)
    {
        _currentCoin += amount;
        _coinUpdateChannel.RaiseEvent(_currentCoin);
    }
    
    // 충돌 시 체력 감소 로직
    public void TakeDamage(int damage)
    {
        _currentHP = Mathf.Max(0, _currentHP - damage);
        _hpUpdateChannel.RaiseEvent(_currentHP);
    }
    
    // 점수를 갱신하는 로직
    public void AddScore(int amount)
    {
        _currentScore += amount;
        _scoreUpdateChannel.RaiseEvent(_currentScore);
    }
}
