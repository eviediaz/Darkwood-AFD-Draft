using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSMUsil;
using System;
using UnityEngine.UI;
using System.Drawing;
using UnityEngine.AI;

public class Estado_Camaras : Estado
{

    public List<scriptAreaSensible> lstAreaSensibleCamara;
    public float contadorTiempo = 0;        //La variable que hara de cronometro
    public int indexCamara = 0;             //Variable que controla la posicion en la lista de camaras
    public int intervaloCambioCamara = 4;   //Cada cuantos segundos se hara el cambio de camaras
    public float tiempoMaximoEstado = 10f;

    public override void Start(BichoScript bicho)
    {
        lstAreaSensibleCamara = new List<scriptAreaSensible>();

        this.nombre = "Estado del Bicho en Camaras";
        bicho.risaSFX.Play();

        //Al inicio se crea una lista con todas las camaras que se encuentren en el escenario
        foreach (var objDetector in GameObject.FindGameObjectsWithTag("CamaraVillano"))
        {
            lstAreaSensibleCamara.Add(objDetector.GetComponent<scriptAreaSensible>());
        }
    }

    public override Estado Update(BichoScript bicho)
    {
        bicho.transform.Rotate( Vector3.forward * 100f * Time.deltaTime );

        //Se va aumentando el contador interno con las variables de Time.deltaTime
        float _deltaTime = Time.deltaTime;
        contadorTiempo += _deltaTime;
        tiempoMaximoEstado -= _deltaTime;

        //Si ha pasado un tiempo mayor al intervalo asignado, regresa el contador a 0 y pasa al siguiente index
        if ( contadorTiempo >= intervaloCambioCamara )
        {
            contadorTiempo = 0;
            indexCamara = (indexCamara == lstAreaSensibleCamara.Count - 1) ? indexCamara = 0 : indexCamara += 1; //Regresa a cero si el index es el ultimo, y aumenta 1 si hay mas

        }

        //Finalmente se cambia el texto con los datos esenciales
        this.textoPrueba = this.nombre
                     + "\n Tiempo en camaras: " + contadorTiempo
                     + "\n Camara actual: " + indexCamara;

        //Si ya paso el tiempo maximo para estar en este estado
        if      ( tiempoMaximoEstado < 0 )
        {
            return new Estado_Buscar();
        }
        //Si se ubica al jugador en una zona
        else if (lstAreaSensibleCamara[indexCamara].veAlJugador() )
        {
            return new Estado_IrUbicacion(lstAreaSensibleCamara[indexCamara].transform.position) ;
        }
        else
        {
            return null;
        }
    }
}

public class Estado_Buscar : Estado
{
    public override void Start(BichoScript bicho)
    {
        this.nombre = "Estado del bicho en Buscar";
    }
    public override Estado Update(BichoScript bicho)
    {
        
        
        return null;
    }
}

public class Estado_IrUbicacion : Estado
{

    public Vector3 habitacionDireccion;

    public Estado_IrUbicacion(Vector3 ubicacionV3)
    {
        habitacionDireccion.Set( ubicacionV3.x, ubicacionV3.y, ubicacionV3.z);
    }

    public override void Start(BichoScript bicho)
    {
        this.nombre = "Entrada del Bicho en Ir a Ubicacion";
    }

    public override Estado Update(BichoScript bicho)
    {
        //bicho.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);

        bicho.agentNavMesh.SetDestination(habitacionDireccion);


        this.textoPrueba = this.nombre
                     + "\n Yendo a: " + habitacionDireccion.ToString();

        if (false)
        {
            return new Estado_Camaras();
        }
        else
        {
            return null;
        }
    }
}


public class BichoScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Estado estadoActual;
    public float velocidad = 5;
    public Text textoEstado;
    public AudioSource risaSFX;
    public NavMeshAgent agentNavMesh;

    void Start()
    {   
        agentNavMesh = GetComponent<NavMeshAgent>();
        agentNavMesh.updateRotation = false;
        agentNavMesh.updateUpAxis = false;

        estadoActual = new Estado_Camaras();
        estadoActual.Start(this);
    }

    // Update is called once per frame
    void Update()
    {

        Estado nuevoEstado;

        nuevoEstado = estadoActual.Update(this);
        if (nuevoEstado != null)
        {
            estadoActual = nuevoEstado;
            estadoActual.Start(this);
        }

        textoEstado.text = estadoActual.GetType() + "\n" + estadoActual.textoPrueba;

    }
}
