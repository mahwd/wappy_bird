using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All
public class coin_randomizer : MonoBehaviour
{

    public List<GameObject> coins;
    private static List<GameObject> coinPrefabs = new List<GameObject>();
    private GameObject generatedCoin;
    public Vector2 defaultSpawnPos;
        
    
    
    
    private void Start()
    {
        generatedCoin = Instantiate(coins[_get_random_coin()]) as GameObject;
        coin_randomizer.coinPrefabs.Add(generatedCoin);
        Debug.Log(coin_randomizer.coinPrefabs.Count);
        generatedCoin.transform.position = transform.position;
    }

    private int _get_random_coin()
    {
        return Random.Range(0, coins.Count);
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
        foreach (var coinPrefab in coinPrefabs)
        {
            Destroy(coinPrefab.gameObject);
        }
    }

    private void FixedUpdate()
    {
    }

    private void LateUpdate()
    {
        if (generatedCoin != null) generatedCoin.transform.position = transform.position;
    }
}
