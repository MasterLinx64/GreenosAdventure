// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaticTextReference.cs" company="Trollpants Game Studios AS">
//  Copyright (c) 2015 All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PG2101
{
    using UnityEngine;
    using UnityEngine.UI;

    public class StaticTextReference : MonoBehaviour
	{

	    private static Text _coinsText = null;

	    private static int _coinsPickedUp = 0;

        public static int CoinsPickedUp
        {
            get
            {
                return _coinsPickedUp;
            }

            set
            {
                _coinsPickedUp = value;
                _coinsText.text = "Coins " + _coinsPickedUp;
            }
        }


        private void Awake()
        {
            if (_coinsText == null)
            {
                _coinsText = GetComponent<Text>();
            }
            else
            {
                Debug.LogWarning("Text already assigned!", this);
            }
        }

        private void OnDestroy()
        {
            _coinsText = null;
        }
	}
}
