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
        generatedCoin.transform.position = transform.position;
    }

    private int _get_random_coin()
    {
        var rand = Random.Range(0, 100);
        if (rand>95)
        {
            return 9;
        }else if (rand>85)
        {
            return 8;
        }else if (rand>70)
        {
            return Random.Range(6, 8);
        } else if(rand>50)
        {
            return Random.Range(4, 6);
        }
        else
        {
            return Random.Range(0, 4);
        }
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
