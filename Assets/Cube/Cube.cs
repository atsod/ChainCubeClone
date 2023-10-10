using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static event Action OnMaxCubeValueReached;
    public static event Action OnPlayerLose;

    public int Id { set; get; }
    public int Value { set; get; }

    public bool IsCubeControllable { set; get; }
    private bool _isFirstTimeLoseTriggerEntered;

    private void Awake()
    {
        Id = gameObject.GetInstanceID();
        Value = 2;

        _isFirstTimeLoseTriggerEntered = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var anotherCube = collision.gameObject.GetComponent<Cube>();

        if (anotherCube != null)
        {
            if(anotherCube.Value == Value)
            {
                CheckMaxCubeValue(Value * 2);

                if(anotherCube.Id != Id)
                {
                    CubeSpawner.Instance.SpawnDoubledCube(anotherCube.transform.position, Value * 2);
                }

                Id = anotherCube.Id;

                Destroy(collision.gameObject);
            }
        }
    }

    private void CheckMaxCubeValue(int value)
    {
        if(CubeInfoLoader.Instance.MaxCubeValue == value)
        {
            OnMaxCubeValueReached?.Invoke();
        }

        if(value > CubeInfoLoader.Instance.CurrentMaxCubeValue)
        {
            CubeInfoLoader.Instance.CurrentMaxCubeValue = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Lose>() != null)
        {
            if(_isFirstTimeLoseTriggerEntered && IsCubeControllable)
            {
                _isFirstTimeLoseTriggerEntered = false;
            }
            else
            {
                OnPlayerLose?.Invoke();
            }
        }
    }
}
