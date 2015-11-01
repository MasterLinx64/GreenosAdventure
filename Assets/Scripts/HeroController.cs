// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeroController.cs" company="Westerdals Oslo ACT">
// <author>
//  Sindri Jóelsson
// </author>
//  Copyright (c) 2015 All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PG2101
{
    using UnityEngine;

    public class HeroController : MonoBehaviour
	{
        // HeroController skal håndtere sine egne variabler og ikke gi
        // andre scripts tilgang til de, men vi har behov for å tweake
        // verdiene i Inspector derfor markerer vi variabelen (fielden)
        // med [SerializeField]
        [SerializeField]
	    private float _horizontalForce = 300f;

        [SerializeField]
	    private float _horizontalMaxSpeed = 10f;

        [SerializeField]
        private float _jumpingForce = 200f;

        [SerializeField]
        private GameObject _groundCheckObject;

        // Disse variablene bestemmer "tilstanden" til kontrolleren
        // og derfor er det ikke noe vits i å gjøre dem synelig
        // i Inspector (med mindre vi skal teste dem mens vi utvikler koden)
	    private bool flipped;

        private bool _facingRight = true;

        private float _horizontalInput;

        // Jeg velger å legge til underscore på private variabler for å
        // diffrensiere dem fra "local" verdier (for eks. en variabel som
        // er deklarert inn i en metode). 
        // Men dette er bare en kodeformatering som jeg synes er grei, du kan bruke din egen så lenge du er konsistent.
        private bool _isJumping;

        // Kjøres en gang, når objektet blir instansiert av Unity
        // Mer om Awake() (og Start()) her: http://unity3d.com/learn/tutorials/modules/beginner/scripting/awake-and-start
        private void Awake()
        {
            // Hvis denne referansevariabelen ikke inneholder en referanse til et objekt (som vi trenger, ellers vill ikke koden funke riktig)
            // forteller vi det til utvikler med å logge en feil.
            if(_groundCheckObject == null)
            {
                Debug.LogError("No ground checking object assigned, it must be assigned using the inpector.", this);
            }
        }

        // Kjører før frame
        private void Update()
        {
            // Vi henter input i Update fordi bestanden til input blir oppdatert da
            // hvis vi gjør det i FixedUpdate kan det hende at vi "mister" input.
            // Mer om Update vs FixedUpdate her: http://unity3d.com/learn/tutorials/modules/beginner/scripting/update-and-fixedupdate
            _horizontalInput = Input.GetAxis("Horizontal");
            _isJumping = Input.GetAxis("Jump") > 0.0f;
        }

        // Kjører 50 ganger i sekundet
        private void FixedUpdate()
        {
            // Hent in input, det vil være et tall mellom -1.0 og 1.0
            _horizontalInput = Input.GetAxis("Horizontal");

            // Hvis input ikke er 0 (d.v.s. bruker trykker til venstre eller høyre)
            if (!Mathf.Approximately(_horizontalInput, 0f))
            {
                // "Til høyre" er en positiv forflyttning langs x-aksen, så hvis
                // input er positivt skal characteren snu mot høyre, ellers ikke.
                _facingRight = _horizontalInput > 0.0f;

                // Flip characteren hvis den skal snu mot høyre men er allerede flipped
                // Observer at her sjekker vi om disse to bools er like, d.v.s. at koden vil også kjøre hvis
                // character er ikke facing right samtidig som den ikke er flipped (d.v.s. begge bools er false)
                // dette er det samme som å bruke if(facingRight && flipped || !facingRight && !flipped)
                if(_facingRight == flipped)
                {
                    // Vi har dratt ut koden her i egen metode fordi det er ikke åpenbart hvordan den funker når
                    // man leser den, derfor enkapsulerer vi den for å gjøre koden mer leselig.
                    // Vi kunne ha gjort dette med flere deler av koden.
                    Flip();
                }

                // Legg den horizontale flyttekraften til rigidbody-en
                // (som da flytter på objektet for oss i følge fysikksystemets regler/instillinger)
                GetComponent<Rigidbody2D>().AddForce(new Vector2(_horizontalInput * _horizontalForce, 0f));

                // Hvis den absolutte verdien av den horizontale delen av rigidbody sin hastighet er høyere en maksfarten
                // (Altså objektet beveger seg for kjappt til venstre eller høyre...)
                if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > _horizontalMaxSpeed)
                {
                    // Så sett farten ned til en ny fart der vi setter den horisontale delen til maksverdien (siden den var over den)
                    // Vi bruker Math.Sign for å hente ut fortegnet (retningen) fra den originale farten sånn at retningen blir riktig
                    GetComponent<Rigidbody2D>().velocity = new Vector2(_horizontalMaxSpeed * Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x), GetComponent<Rigidbody2D>().velocity.y);
                }
            }

            // Skyt en linje nedover fra våres posisjon til posisjonen til groundcheck objektet som
            // kolliderer bare med objekter på layeren "Ground", lagre resultatet i variabelen "isOnGround"
            RaycastHit2D groundHitResult = Physics2D.Linecast(
                        transform.position,
                        _groundCheckObject.transform.position,
                        1 << LayerMask.NameToLayer("Ground"));

            // RaycastHit2D har en implisiv "til boolean" konversjons-operator definert som konverterer til true hvis den kolliderte, ellers false.
            // Visual Studio: Prøv å markere RaycastHit2D og trykk på F12, alternativet høyre-klikk -> "Go to definition"
            if (groundHitResult)
            {
                if (_isJumping)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _jumpingForce));
                }
            }

        }

        // Spegler karakteren om y-aksen.
	    private void Flip()
	    {
            // Det vi gjør her er å flippe fortegnet til x-aksen på skaleringen av transformen, effekten
            // av dette er at den vi bli speglet om y-aksen.
	        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            // Vi vil også ta vare på tilstanden, om den er flipped eller ikke, her setter vi tilstanden til det motsatte verdien den hadde.
            // https://msdn.microsoft.com/en-us/library/f2kd6eb2.aspx
            flipped = !flipped;
	    }
	}
}
