using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    public GameObject target;

    public bool atacando;
    public Button BotonPlay;
    public Vector3 posicionInicial;
    public bool isplay;
    private bool isStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("DogPlayer");
        isplay = false;
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isplay)
        {
            Comportamiento_Enemigo();   
        }else{
            BotonPlay.onClick.AddListener(Comportamiento_Enemigo_inicio);
        }
    }

    public void Comportamiento_Enemigo_inicio(){
        transform.position = posicionInicial;
        isplay = true;
        Comportamiento_Enemigo();
    }

    public void Comportamiento_Enemigo(){

        if (isStopped)
        {
            return;
        }

        if(Vector3.Distance(transform.position, target.transform.position) > 5){
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if(cronometro >= 4){
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }   

            switch(rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                
                case 1: 
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
                
            }
        }else{
            if(Vector3.Distance(transform.position, target.transform.position) > 2 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;

                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                ani.SetBool("walk", false);
                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                ani.SetBool("attack", false);
                Debug.Log("Persiguiendo");
            }
            else{
                Debug.Log(atacando);
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("attack", true);
                atacando = true;
            }
           
        }
    }
     public void StopZombie(float duration)
    {
        if (!isStopped)
        {
            StartCoroutine(StopZombieCoroutine(duration));
        }
    }

    private IEnumerator StopZombieCoroutine(float duration)
    {
        isStopped = true;
        ani.SetBool("walk", false);
        ani.SetBool("run", false);
        ani.SetBool("attack", false);
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }

    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
        Debug.Log("Finalizo la animacion de ataque");
    }
}
