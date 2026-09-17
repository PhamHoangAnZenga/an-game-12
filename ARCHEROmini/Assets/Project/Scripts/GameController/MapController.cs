using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapController : MySingleton<MapController>
{
    [Header("Cloud")]
    [SerializeField] Cloud[] _cloudPrefabs;
    [SerializeField] float _cloudTime;

    [SerializeField] float _cloudHeight;

    [SerializeField] float _startLine;
    [SerializeField] float _endLine;

    [SerializeField] float _MostLeft;
    [SerializeField] float _MostRight;


    [Header("Stone")]
    [SerializeField] GameObject[] _stonePrefabs;
    [SerializeField] Vector3 _botLeft;
    [SerializeField] Vector3 _topRight;
    [SerializeField] int _maxStone;

    float _timer;
    List<GameObject> _stones;
    List<Cloud> _clouds;

    protected override void Awake()
    {
        base.Awake();
        _stones = new();
        _clouds = new();
    }
    
    public void MapInit()
    {
        foreach (var cloud in _clouds) Destroy(cloud.gameObject);
        _clouds = new();
        foreach (var stone in _stones) Destroy(stone);
        _stones = new();

        _timer = 0;
        for (int i = 0; i < _maxStone; ++i)
        {
            Vector3 postion = new(Random.Range(_botLeft.x, _topRight.x), -1, Random.Range(_botLeft.z, _topRight.z));
            GameObject stone = Instantiate(_stonePrefabs[Random.Range(0, _stonePrefabs.Count())], postion, Quaternion.identity);
            _stones.Add(stone);
           
            Vector3 spawnPosition = new Vector3(Random.Range(_MostLeft, _MostRight), _cloudHeight, Random.Range(_startLine, _endLine));
            Cloud cloud = Instantiate(_cloudPrefabs[Random.Range(0, _cloudPrefabs.Count())], spawnPosition, Quaternion.identity);
            cloud.ID = _clouds.Count;
            _clouds.Add(cloud);
            cloud.OnClear += RemoveCloud;


            cloud.Init(_cloudTime, _endLine);
        }        
    }

    void Update()
    {
        if (_timer < Time.time)
        {
            _timer = Time.time + _cloudTime;

            Cloud cloudPrefab = _cloudPrefabs[Random.Range(0, _cloudPrefabs.Count())];

            Vector3 spawnPosition = new Vector3(Random.Range(_MostLeft, _MostRight), _cloudHeight, _startLine);
            Cloud cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);

            cloud.ID = _clouds.Count;
            _clouds.Add(cloud);
            cloud.OnClear += RemoveCloud;


            cloud.Init(_cloudTime, _endLine);
        }
    }
    
    void RemoveCloud(int id)
    {
        _clouds[id] = _clouds[_clouds.Count() - 1];
        _clouds.RemoveAt(_clouds.Count() - 1);
    }
}
