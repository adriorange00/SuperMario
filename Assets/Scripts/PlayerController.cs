using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // ----------------------------------------------------------
    // MÉTODO PARA LECTURA Y APLICACIÓN DEL MOVIMIENTO HORIZONTAL
    // ----------------------------------------------------------
    // Campo privado pero visible desde el inspector para multiplicar
    // la velocidad de movimiento
    [SerializeField] float motionVelocity = 1f;

    // Campo privado pero visible desde el inspector para multiplicar
    // la fuerza de salto
    [SerializeField] float fuerzaSalto = 1f;

    [SerializeField] float lengthLine = 0f;

    [SerializeField] float offsetRaycast = 0f;

    // Obtenemos la referencia al sistema de partículas
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
        // Se aplica la velocidad a las físicas del gameObject que contiene el script
        GetComponent<Rigidbody2D>().linearVelocity = speedVector;

        // Detectamos la pulsación de la tecla (una vez por cada pulsación)
        if (Input.GetKeyDown(KeyCode.Space) && saltar) {
            AudioManager.instance.PlaySFX("Jump");
            //Impedimos saltos múltiples
            saltar = false;

            // Aplicamos la fuerza física en la coordenada vertical
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

            // Iniciamos la producción de partículas
            jumpParticles.Play();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            SCManager.instance.LoadScene("WorldMap");
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            SCManager.instance.UnloadSceneAsync("WorldMap");
        }

        //Raycast

        // Obtenemos el tamaño vertical del collider para configurar la coordenada de generación
        // OffsetRaycast es muy importante para que el RayCast no se genere dentro del propio collider
        // del personaje (si se generara dentro tendríamos problemas de detección ObjetoQueLoGenera VS ObjetoConElQueColisiona)
        float colliderSizeY = gameObject.GetComponent<Collider2D>().bounds.size.y + offsetRaycast;
        Vector2 raycastOrigin = new Vector2(gameObject.transform.position.x, transform.position.y - (colliderSizeY / 2));

        // Dibujamos una línea de color negro hacia abajo que parta desde la posición calculada anteriormente
        // y que termine en donde indique el segundo parámetro del método DrawLine()
        Debug.DrawLine(raycastOrigin, raycastOrigin - (new Vector2(0f, lengthLine)), Color.black);

        // Generamos el RayCast coincidente con el DrawLine (los parámetros cambian un poco).
        // El primer parámetro es el origen (eso no cambia), el segundo parámetro es la dirección
        // (en DrawLine era el punto de finalización), el tercer parámetro es la longitud del rayo
        RaycastHit2D raycastHit2D = Physics2D.Raycast(raycastOrigin, Vector2.down, lengthLine);

        // Preguntamos si el RayCast está colisionando con el Tilemap de colisiones
        // asegurándonos de que dicho Tilemap tiene una etiqueta correctamente configurada
        if (raycastHit2D.collider != null) {
            if (raycastHit2D.collider.gameObject.CompareTag("Ground")) {
                saltar = true; // Si configuramos True aquí, solo podrá saltar si está en el suelo
            }
            saltar = true; // Si configuramos True aquí, podrá saltar siempre que esté sobre un collider
            if (GetComponent<Rigidbody2D>().linearVelocityX != 0) SetAnimation("running");
            else SetAnimation("idle"); // Si está sobre una superficie pero no se mueve
        }
        else { 
            saltar = false;
            SetAnimation("jumping");
        } // Si no está colisionando con nada tampoco podrá saltar
    }

    // -----------------------------------------------------------------------------
    // Método que desactiva todos los parámetros del Animator y activa uno concreto
    // -----------------------------------------------------------------------------
    void SetAnimation(string name) {

        // Obtenemos todos los parámetros del Animator
        AnimatorControllerParameter[] parametros = GetComponent<Animator>().parameters;

        // Recorremos todos los parámetros y los ponemos a false
        foreach (var item in parametros) GetComponent<Animator>().SetBool(item.name, false);

        // Activamos el pasado por parámetro
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

    // Método que detecta las colisiones
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
