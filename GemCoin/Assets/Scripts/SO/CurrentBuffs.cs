using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentBuffs", menuName = "Scriptable Objects/CurrentBuffs")]
public class CurrentBuffs : ScriptableObject
{
    [SerializeField] private List<BaseBuffSO> _ownedBuffs = new();
    public List<BaseBuffSO> OwnedBuffs => _ownedBuffs;
    public int ownedBuffCount { get; private set; } 

    public void AddBuff(BaseBuffSO newBuff)
    {
        if (!_ownedBuffs.Contains(newBuff))
        {
            _ownedBuffs.Add(newBuff);
            newBuff.currentStack = 1;
            ownedBuffCount++;
        }
        // 중복 획득시 스택 증가
        else
        {
            newBuff.currentStack++;
            ownedBuffCount++;
        }
        
    }

    public void ClearBuffs() // 세션 시작 시 호출 필요
    {
        foreach (var buff in _ownedBuffs)
        {
            buff.Init();
        }
        _ownedBuffs.Clear();
        ownedBuffCount = 0;
    }
}
