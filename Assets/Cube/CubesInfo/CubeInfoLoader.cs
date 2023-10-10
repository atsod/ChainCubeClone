using System;
using UnityEngine;

public class CubeInfoLoader : MonoBehaviour
{
    public static CubeInfoLoader Instance { private set; get; }

    public CubeInfo[] CubesInfo { private set; get; }
    
    public int MaxCubeValue { private set; get; }
    public int CurrentMaxCubeValue { set; get; }

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
        }

        CubesInfo = Resources.LoadAll<CubeInfo>("");
        SortCubesArray();

        MaxCubeValue = 2048;
        CurrentMaxCubeValue = 2;
    }

    private void SortCubesArray()
    {
        Array.Sort(CubesInfo, 
            delegate (CubeInfo x, CubeInfo y) 
            { 
                return x.Value.CompareTo(y.Value); 
            });
    }
}
