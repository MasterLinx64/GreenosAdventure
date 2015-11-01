// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameReset.cs" company="Trollpants Game Studios AS">
//  Copyright (c) 2015 All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PG2101
{
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class GameReset : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerObject;

        private void Awake()
        {
            if(_playerObject == null)
            {
                Debug.LogError("Missing player object to track, it needs to be assigned in the inspector.", this);
            }
        }

        private void Update()
        {
            // Vi vill følge spiller horisontalt
            transform.position = new Vector3(
                _playerObject.transform.position.x,
                transform.position.y,
                transform.position.z);
        }

        // Når spiller entrer denne kollideren laster vi inn brettet på nytt.
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Her burde vi sjekke for eksempel på other før
            // vi laster brette på nytt med siden player
            // er det eneste objektet i scenen trenger vi ikke
            // å bry oss om det.
            Application.LoadLevel(Application.loadedLevel);
        }
        

    }
}
