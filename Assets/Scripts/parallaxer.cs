using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxer : MonoBehaviour
{

    public GameObject prefab;
    private List<GameObject> _prefabs;
    public float spawnRate;
    private float spawnTimer;
    public float shiftSpeed;
    private GameManager _game;
    public Vector2 spawnPos;

    private void Start()
    {
        _game = GameManager.Instance;
        _prefabs = new List<GameObject>();
        spawnTimer = 0;
    }

    private void FixedUpdate()
    {
        spawnTimer += Time.smoothDeltaTime;
        if (!(spawnTimer > spawnRate)) return;
        _prefabs.Add(Instantiate(prefab, spawnPos, Quaternion.identity));
        spawnTimer = 0;
    }

    private void OnEnable()
    {
        GameManager.onGameOverConfirmed += onGameOverConfirmed;
    }

    private void OnDisable()
    {
        GameManager.onGameOverConfirmed -= onGameOverConfirmed;
    }

    private void onGameOverConfirmed()
    {
        foreach (var _prefab in _prefabs)
        {
            Destroy(_prefab.gameObject);
        }
    }
        
    private void Update()
    {
        if (_game.GameOver) return;
        Shift();
    }

    private void Shift()
    {
        foreach (var _prefab in _prefabs)
        {
            if (_prefab == null) continue;
            _prefab.transform.position += -Vector3.right * shiftSpeed * Time.smoothDeltaTime;
            CheckDisposeObject(_prefab);
        }
    }
    
    private void CheckDisposeObject(GameObject _prefab)
    {
        if (!(_prefab.transform.position.x < -spawnPos.x)) return;
        Destroy(_prefab.gameObject);
    }
}
