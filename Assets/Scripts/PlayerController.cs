using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f; // Velocidad de movimiento del personaje
    public float jumpForce = 5f; // Fuerza de salto del personaje
    private Rigidbody rb; // Referencia al Rigidbody del personaje
    private Animator animator; // Referencia al componente Animator
    public Button BotonPlay;
    public bool isplay;


    void Start ()
    {
        rb = GetComponent<Rigidbody>(); // Obtener la referencia al Rigidbody del personaje
        animator = GetComponent<Animator>();// Obtener la referencia al componente Animator
        isplay = false;
    }

    void Update ()
    {
        if (isplay)
        {
        
            // Obtener la entrada del teclado para las teclas de flecha
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            // Definir la dirección de movimiento basada en las teclas presionadas
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            if (movementDirection != Vector3.zero)
            {
                // Rotar el personaje cuando se presiona "A" o la flecha izquierda
                if (Input.GetKeyDown(KeyCode.A) || horizontalInput < 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 180, 0f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                // Rotar el personaje cuando se presiona la flecha derecha
                else if (horizontalInput > 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 0, 0f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }

                // Mover el personaje hacia adelante cuando se presiona "W" o la flecha arriba
                if (Input.GetKey(KeyCode.W) || verticalInput > 0)
                {
                    transform.rotation = Quaternion.Euler(0f, -90, 0f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                // Mover el personaje hacia atrás cuando se presiona la flecha abajo
                else if (Input.GetKey(KeyCode.S) || verticalInput < 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 90, 0f);

                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }

                animator.SetBool("RunForward", true);
                
            }
            else
            {
                animator.SetBool("RunForward", false);
            }

            if (Input.GetMouseButtonDown(0)) // 0 corresponde al clic izquierdo del mouse
            {
                animator.SetTrigger("Attack01"); // Activa la animación de ataque
            }

            if (Input.GetMouseButtonDown(1)) // 0 corresponde al clic derecho del mouse
            {
                animator.SetTrigger("Defend01"); // Activa la animación de ataque
            }

            // Verificar si se presiona la tecla de espacio para saltar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Verificar si el personaje está en el suelo (puedes ajustar esto según tus necesidades)
                if (Mathf.Abs(rb.velocity.y) < 0.01f)
                {
                    // Aplicar fuerza de salto al Rigidbody
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    // Establecer la animación de salto
                    // animator.SetTrigger("Jump");
                }
            }
            
               
        }else{
            BotonPlay.onClick.AddListener(botonPlayOnClick);
        }

            
    }

    public void botonPlayOnClick(){
        isplay = true;
    }
}