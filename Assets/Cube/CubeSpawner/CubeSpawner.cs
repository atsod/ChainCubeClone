using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance { private set; get; }
    
    private Vector3 _spawnControllableCubePosition;

    private float _spawnControllableCubeDelay;
    private float _spawnDoubledCubeDelay;

    private bool _isCubeDoubled;

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
        
        _spawnControllableCubePosition = new Vector3(2.72f, 2.789f, -4.57f);

        _spawnControllableCubeDelay = 0f;
        _spawnDoubledCubeDelay = 0f;
    }

    private void Start()
    {
        SpawnControllableCube();
        _spawnControllableCubeDelay = 2.5f;
    }

    private void OnEnable() => CubeMovement.OnCubeReleased += SpawnControllableCube;   
    
    private void OnDisable() => CubeMovement.OnCubeReleased -= SpawnControllableCube;

    public void SpawnDoubledCube(Vector3 position, int value)
    {
        int doubledCubeIndex = (int)Mathf.Log(value, 2) - 1;
        
        _isCubeDoubled = true;

        StartCoroutine(InstantiateCubeInDelay(
            position, 
            doubledCubeIndex, 
            _spawnDoubledCubeDelay));
    }

    private void SpawnControllableCube()
    {
        int maxValueCubeIndex = (int)Mathf.Log(CubeInfoLoader.Instance.CurrentMaxCubeValue, 2) - 1;
        int randomIndex = Random.Range(0, maxValueCubeIndex);

        StartCoroutine(InstantiateCubeInDelay(
            _spawnControllableCubePosition, 
            randomIndex, 
            _spawnControllableCubeDelay));
    }

    private IEnumerator InstantiateCubeInDelay(Vector3 position, int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        CubeInfo info = CubeInfoLoader.Instance.CubesInfo[index];
        GameObject prefab = info.Prefab;
        int value = info.Value;

        GameObject cubeObject = Instantiate(prefab, position, Quaternion.identity);

        cubeObject.GetComponent<Cube>().Value = value;
        
        SetCubeObjectSettings(cubeObject);
    }

    private void SetCubeObjectSettings(GameObject cubeObject)
    {
        CubeMovement cubeMovement = cubeObject.GetComponent<CubeMovement>();
        Cube cube = cubeObject.GetComponent<Cube>();

        if (_isCubeDoubled)
        {
            cubeMovement.CubeRigidbody.useGravity = true;
            AddFirstDoubledCubeImpulse(cubeMovement.CubeRigidbody);

            cubeMovement.enabled = false;

            _isCubeDoubled = false;
        }
        else
        {
            cube.IsCubeControllable = true;

            cubeMovement.CubeRigidbody.useGravity = false;
        }
    }
     
    private void AddFirstDoubledCubeImpulse(Rigidbody rigidbody)
    {
        float xRandom = (float)Random.Range(-100, 100) / 100;
        float zRandom = (float)Random.Range(-30, 100) / 100;

        float impulseSpeed = (float)Random.Range(60, 100) / 10;

        Vector3 direction = new (xRandom, 1f, zRandom);

        rigidbody.AddForce(impulseSpeed * direction, ForceMode.Impulse);
    }
}
