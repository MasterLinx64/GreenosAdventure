// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlatformSpawner.cs" company="Westerdals Oslo ACT">
// <author>
//  Sindri Jóelsson
// </author>
//  Copyright (c) 2015 All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PG2101
{
    using UnityEngine;

    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _platformPrefab;

        private float _platformWidth;

        [SerializeField]
	    private int _numberOfPlatforms = 10;

        [SerializeField]
        private float _altitudeVariation = 2f;

        [SerializeField]
        private float _gapVariation = 2f;

        public void Awake()
        {
            // En enkel måte å hente bredden på en platform er å hente bredden på Collideren til den.
            _platformWidth = _platformPrefab.GetComponent<Collider2D>().bounds.size.x;

            // Når vi skal spawne flere platformer bortover i en retning må
            // vi ta vare på hvor langt vi er "kommet" så langt.
            // Dette vil være en kombinasjon av platformBredde * hvorMangePlatformerSåLangt + platformMellomrom * hvorMangePlatformerSåLangt
            var horizontalAccumulativeDelta = 0f;

            /*
             * Hva er dette delta?
             * 
             * Delta eller symbolet Δ er oft brukt i matte når man snakker om "endring", "veksling" eller "forskjell",
             * 'delta' er den første bokstaven i det greske ordet "διαφορά" (diaphorá) som betyr "forskjell".
             * 
             */

            for (int i = 0; i < _numberOfPlatforms; i++)
            {
                var y = Random.Range(-_altitudeVariation, _altitudeVariation);

                var currentGapWidth = Random.Range(0f, _gapVariation);

                horizontalAccumulativeDelta += _platformWidth + currentGapWidth;

                var x = horizontalAccumulativeDelta;

                var currentPlatformPosition = new Vector2(x, y);

                var currentPlatform = GameObject.Instantiate(_platformPrefab, currentPlatformPosition, Quaternion.identity) as GameObject ;

                // http://docs.unity3d.com/ScriptReference/Transform.SetParent.html
                // Vi vil ikke at posisjonen skal endre seg når vi childer det nye platform objektet til
                // dette objektet. (Vi gjør dette kun for å holde hierarkiet ryddigt.)
                currentPlatform.transform.SetParent(this.transform);
            }
        }
    }
}
