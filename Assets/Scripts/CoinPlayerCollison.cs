// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoinScript.cs" company="Trollpants Game Studios AS">
//  Copyright (c) 2015 All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PG2101
{

    using UnityEngine;

    public class CoinPlayerCollison : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (collider2d.gameObject.CompareTag("Player"))
            {
                StaticTextReference.CoinsPickedUp++;
                Destroy(gameObject);
            }
        }
    }
}
