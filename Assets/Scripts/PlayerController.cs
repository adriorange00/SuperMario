using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------------------------------
    // M�TODO PARA LECTURA Y APLICACI�N DEL MOVIMIENTO HORIZONTAL
    // ----------------------------------------------------------
    // Campo privado pero visible desde el inspector para multiplicar
    // la velocidad de movimiento
    [SerializeField] float motionVelocity = 1f;

    // Campo privado pero visible desde el inspector para multiplicar
    // la fuerza de salto
    [SerializeField] float fuerzaSalto = 1f;

    [SerializeField] float lengthLine = 0f;

    [SerializeField] float offsetRaycast = 0f;

    // Obtenemos la referencia al sistema de part�culas
    [SerializeField] ParticleSystem jumpParticles;

    // Variable privada para el control de saltos
    bool saltar = true;

    private void Start() {
        AudioManager.instance.musicSource.loop = true;
        AudioManager.instance.PlayMusic("MainTheme");
    }

    // Update se llama una vez por cada frame
    private void Update() {
        // Lectura del eje de entrada horizontal
        float xMotion = Input.GetAxisRaw("Horizontal");

        // Volteamos el render del sprite en base al movimiento del gameObject
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (xMotion < 0) sr.flipX = true;
        else if (xMotion > 0) sr.flipX = false;

        // Referencia al Rigidbody2D del gameObject que contiene este script
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Se genera un nuevo vector con la velocidad a aplicar
        Vector2 speedVector = new Vector2(xMotion * motionVelocity, rb.linearVelocityY);
        // Se aplica la velocidad a las f�sicas del gameObject que contiene el script
        GetComponent<Rigidbody2D>().linearVelocity = speedVector;

        // Detectamos la pulsaci�n de la tecla (una vez por cada pulsaci�n)
        if (Input.GetKeyDown(KeyCode.Space) && saltar) {
            AudioManager.instance.PlaySFX("Jump");
            //Impedimos saltos m�ltiples
            saltar = false;

            // Aplicamos la fuerza f�sica en la coordenada vertical
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

            // Iniciamos la producci�n de part�culas
            jumpParticles.Play();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            SCManager.instance.LoadScene("WorldMap");
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            SCManager.instance.UnloadSceneAsync("WorldMap");
        }

        //Raycast

        // Obtenemos el tama�o vertical del collider para configurar la coordenada de generaci�n
        // OffsetRaycast es muy importante para que el RayCast no se genere dentro del propio collider
        // del personaje (si se generara dentro tendr�amos problemas de detecci�n ObjetoQueLoGenera VS ObjetoConElQueColisiona)
        float colliderSizeY = gameObject.GetComponent<Collider2D>().bounds.size.y + offsetRaycast;
        Vector2 raycastOrigin = new Vector2(gameObject.transform.position.x, transform.position.y - (colliderSizeY / 2));

        // Dibujamos una l�nea de color negro hacia abajo que parta desde la posici�n calculada anteriormente
        // y que termine en donde indique el segundo par�metro del m�todo DrawLine()
        Debug.DrawLine(raycastOrigin, raycastOrigin - (new Vector2(0f, lengthLine)), Color.black);

        // Generamos el RayCast coincidente con el DrawLine (los par�metros cambian un poco).
        // El primer par�metro es el origen (eso no cambia), el segundo par�metro es la direcci�n
        // (en DrawLine era el punto de finalizaci�n), el tercer par�metro es la longitud del rayo
        RaycastHit2D raycastHit2D = Physics2D.Raycast(raycastOrigin, Vector2.down, lengthLine);

        // Preguntamos si el RayCast est� colisionando con el Tilemap de colisiones
        // asegur�ndonos de que dicho Tilemap tiene una etiqueta correctamente configurada
        if (raycastHit2D.collider != null) {
            if (raycastHit2D.collider.gameObject.CompareTag("Ground")) {
                saltar = true; // Si configuramos True aqu�, solo podr� saltar si est� en el suelo
            }
            saltar = true; // Si configuramos True aqu�, podr� saltar siempre que est� sobre un collider
            if (GetComponent<Rigidbody2D>().linearVelocityX != 0) SetAnimation("running");
            else SetAnimation("idle"); // Si est� sobre una superficie pero no se mueve
        }
        else { 
            saltar = false;
            SetAnimation("jumping");
        } // Si no est� colisionando con nada tampoco podr� saltar
    }

    // -----------------------------------------------------------------------------
    // M�todo que desactiva todos los par�metros del Animator y activa uno concreto
    // -----------------------------------------------------------------------------
    void SetAnimation(string name) {

        // Obtenemos todos los par�metros del Animator
        AnimatorControllerParameter[] parametros = GetComponent<Animator>().parameters;

        // Recorremos todos los par�metros y los ponemos a false
        foreach (var item in parametros) GetComponent<Animator>().SetBool(item.name, false);

        // Activamos el pasado por par�metro
        GetComponent<Animator>().SetBool(name, true);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision != null) {
            if (collision.CompareTag("Coin")) {
                AudioManager.instance.PlaySFX("CollectCoin");
                Destroy(collision.gameObject);

                GameManager.monedas++;
                GameObject.FindGameObjectWithTag("Monedas").GetComponent<TextMeshProUGUI>().text = "Monedas: " + GameManager.monedas.ToString();
            }
        }
    }

    // M�todo que detecta las colisiones
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision != null) {
            if (collision.collider.CompareTag("Goomba")) {
                AudioManager.instance.PlaySFX("Hit");
                AudioManager.instance.musicSource.loop = false;
                AudioManager.instance.PlayMusic("LostALife");                
                GameManager.monedas = 0;
                SCManager.instance.LoadScene("GameOver");
            }
        }
    }
}
