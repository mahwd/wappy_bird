using UnityEngine;
// ReSharper disable All

public class parallax : MonoBehaviour {
	
	private class PoolObject
	{
		public Transform transform;
		public bool inUse;

		public PoolObject(Transform t){ transform = t; }

		public void Use()
		{
			inUse = true;
		}
		
		public void Dispose()
		{
			inUse = false;
		}
	}

	[System.Serializable]
	public struct YSpawnRange
	{
		public float min;
		public float max;
	}
	
	public GameObject prefab;
	public int poolSize;
	public float shiftSpeed;
	public float spawnRate;

	public YSpawnRange _ySpawnRange;
	public Vector3 defaultSpawnPos;
	public bool spawnImmediate;
	public Vector3 spawnImmediatePos;
	public Vector2 targetAspectRatio;
	private float _targetAspect;
	private float _spawnTimer;

	private PoolObject[] _poolObjects;

	private GameManager _game;

	
	private void Awake()
	{
		Configure();
	}

	
	private void Start()
	{
		_game = GameManager.Instance;
	}

	
	private void OnEnable()
	{
		GameManager.onGameOverConfirmed += onGameOverConfirmed;
	}

	
	private void OnDisable()
	{
		GameManager.onGameOverConfirmed += onGameOverConfirmed;		
	}

	
	private void onGameOverConfirmed()
	{
		for (var i = 0; i < _poolObjects.Length; i++)
		{
			_poolObjects[i].Dispose();
			_poolObjects[i].transform.position = Vector3.one*1000;
		}
		if (spawnImmediate)
		{
			SpawnImmediate();
		}
	}

	private void Update()
	{
		if (_game.GameOver) return;
		Shift();
		_spawnTimer += Time.deltaTime;
		if (!(_spawnTimer > spawnRate)) return;
		Spawn();
		_spawnTimer = 0;
	}

	
	private void Configure()
	{
		_targetAspect = targetAspectRatio.x / targetAspectRatio.y;
		_poolObjects = new PoolObject[poolSize];
		for (var i = 0; i < _poolObjects.Length; i++)
		{
			GameObject go = Instantiate(prefab) as GameObject;
			Transform t = go.transform;
			t.SetParent(transform);
			t.position = Vector3.one * 1000;
			_poolObjects[i] = new PoolObject(t);
		}

		if (spawnImmediate)
		{
			SpawnImmediate();
		}
	}

	private void Spawn()
	{
		Transform t = GetPoolObject();
		if (t==null) return;
		Vector3 pos = Vector3.zero;
		pos.x = defaultSpawnPos.x;
		pos.y = Random.Range(_ySpawnRange.min, _ySpawnRange.max);
		t.position = pos;
	}
	
	private void SpawnImmediate()
	{
		Transform t = GetPoolObject();
		if (t==null) return;
		Vector3 pos = Vector3.zero;
		pos.x = defaultSpawnPos.x;
		pos.y = Random.Range(_ySpawnRange.min, _ySpawnRange.max);
		t.position = pos;
		Spawn();
	}
	private void Shift()
	{
		foreach (PoolObject _object in _poolObjects)
		{
			_object.transform.position += -Vector3.right*shiftSpeed*Time.deltaTime ;
			CheckDisposeObject(_object);
		}
	}
	
	
	private void CheckDisposeObject(PoolObject poolObject)
	{
		if (!(poolObject.transform.position.x < -defaultSpawnPos.x)) return;
		Debug.Log("disposed");
		poolObject.Dispose();
		poolObject.transform.position = Vector3.one * 1000;
	}	

	private Transform GetPoolObject()
	{
		foreach (var poolObject in _poolObjects)
		{
			if (poolObject.inUse) continue;
			poolObject.Use();
			return poolObject.transform;
		}
		return null; 
	}
}

