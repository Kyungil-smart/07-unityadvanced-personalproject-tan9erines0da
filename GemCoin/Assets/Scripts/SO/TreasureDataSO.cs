using UnityEngine;

[CreateAssetMenu(fileName = "TreasureDataSO", menuName = "Scriptable Objects/TreasureDataSO")]
public class TreasureDataSO : ScriptableObject
{
    public string treasureName;
    
    [Header("Values")]
    public int score;
    public int coin;
    
    [Header("Visual")]
    public Sprite image;
    public Vector3 localScale = Vector3.one;
    public float size = 0.33f;
    
    [Header("Magnet Settings")]
    public float magnetSpeed = 10f;       // 끌려오는 속도
    public float acceleration = 2f;       // 시간이 갈수록 빨라지는 가속도
}
