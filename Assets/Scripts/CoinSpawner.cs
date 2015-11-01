// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoinSpawner.cs" company="Trollpants Game Studios AS">
//  Copyright (c) 2015 All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PG2101
{
    using UnityEngine;

    public class CoinSpawner : MonoBehaviour
	{
        [SerializeField]
	    private GameObject _coinPrefab;

        [SerializeField]
	    private GameObject[] _spawnPoints;

        private void Awake()
        {
            var randomIndex = Random.Range(0, _spawnPoints.Length - 1);

            var coin = Instantiate(_coinPrefab, _spawnPoints[randomIndex].transform.position, Quaternion.identity) as GameObject;

            coin.transform.SetParent(this.transform, true);
        }
	}
}
