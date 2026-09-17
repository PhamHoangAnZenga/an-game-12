using System.Linq;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] Cloud[] _cloudPrefabs;
    [SerializeField] float _cloudTime;


    [SerializeField] float _cloudHeight;

    [SerializeField] float _startLine;
    [SerializeField] float _endLine;

    [SerializeField] float _MostLeft;
    [SerializeField] float _MostRight;

    float _timer;

    void Start()
    {
        _timer = 0;
    }
    
    void Update()
    {        
        if(_timer < Time.time)
        {
            _timer = Time.time + _cloudTime;

            Cloud cloudPrefab = _cloudPrefabs[Random.Range(0, _cloudPrefabs.Count())];

            Vector3 spawnPosition = new Vector3(Random.Range(_MostLeft, _MostRight), _cloudHeight, _startLine);
            Cloud cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);

            cloud.Init(_cloudTime, _endLine);
        }
    }
}
