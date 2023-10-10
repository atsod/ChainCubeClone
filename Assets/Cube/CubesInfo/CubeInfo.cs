using UnityEngine;

[CreateAssetMenu(fileName = "CubeInfo", menuName = "Cube2048/New Cube2048")]
public class CubeInfo : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _value;

    public GameObject Prefab => _prefab;
    public int Value => _value;
}
