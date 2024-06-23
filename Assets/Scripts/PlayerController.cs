using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f; // Velocidad de movimiento del personaje
    private Animator animator; // Referencia al componente Animator
    public Button BotonPlay;
    public bool isplay;
    public TMP_InputField nameProfile;
    public TMP_Dropdown team;
    public TMP_Text NameDog;
    public TMP_Text NameTeamDog;

    // Variables Barra Salud ---------------------
    [Header("Barra Salud")]
    public float Salud = 100;
    public float SaludMaxima = 100;

    public Image BarraSalud;
    public Text TextoSalud;

    [Header("Canvas Muerto")]
    public GameObject Muerto;
    public GameObject Victoria;

    [Header("Arma")]
    public BoxCollider armaBoxCol;
    // public int hp = 50;
    public int dañoArma = 5;
    public Animator anim;

    [Header("Puntos")]
    public GameObject point1;
    public GameObject point2;
    public Animator openDoor1;
    public Animator openDoor2;

    void Start ()
    {
        animator = GetComponent<Animator>();
        isplay = false;
        nameProfile.onValueChanged.AddListener(isNameDog);
        team.onValueChanged.AddListener(isTeamDog);

        DesactivarCollidersArmas();
    }

    private void isNameDog(string textNameDog)
    {
        NameDog.text = textNameDog;
    }
    private void isTeamDog(int indexTeamDog)
    {

        string typeTeam = "";
        
         switch (indexTeamDog)
        {
            case 0:
                typeTeam = "Red";
                break;
            case 1:
                typeTeam = "Blue";
                break;
            case 2:
                typeTeam = "Yellow";
                break;
        }
        NameTeamDog.text = "Team "+typeTeam;
    }

    void Update ()
    {
        if (isplay)
        {
            ActualizarSalud();
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
            
               
        }else{
            BotonPlay.onClick.AddListener(botonPlayOnClick);
        }

            
    }

    public void botonPlayOnClick(){
        isplay = true;
    }

    void ActualizarSalud()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "+ " + Salud.ToString("f0");
    }

     public void ActivarCollidersArmas()
    {
        armaBoxCol.enabled = true;
    }

    public void DesactivarCollidersArmas()
    {
        armaBoxCol.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "ManoEnemiga")
        {
            if(anim != null)
            {
                anim.Play("GetHit");
            }
            Salud -= dañoArma;
        }

        if(Salud <= 0)
        {
            if(gameObject != null) {
                Destroy(gameObject);
            }
            Instantiate(Muerto);
            Salud = 0;
        }

        if(other.gameObject.tag == "point1") {
            Destroy(point1);
            openDoor1.SetTrigger("DoorATrigger");
        }

        if(other.gameObject.tag == "point2") {
            Destroy(point2);
            openDoor2.SetTrigger("DoorATrigger");
        }

        if(other.gameObject.tag == "point3") {
            Instantiate(Victoria);
        }
    }
}
