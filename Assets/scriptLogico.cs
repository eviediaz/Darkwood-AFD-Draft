using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class scriptLogico : MonoBehaviour
{

    public Text infoPantalla;
    public List<scriptAreaSensible> lstAreaSensibleCamara;

    private string msgPantalla = "";

    private float contadorTiempo = 0f;
    public int intervaloCambioCamara = 3;
    public int indexCamara = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach ( var objDetector in GameObject.FindGameObjectsWithTag("CamaraVillano"))
        {
            lstAreaSensibleCamara.Add(objDetector.GetComponent<scriptAreaSensible>());
        }

    }

    // Update is called once per frame
    void Update()
    {


        msgPantalla = (int)contadorTiempo + " " + " Camara: " + indexCamara + " " + lstAreaSensibleCamara[indexCamara].veAlJugador().ToString();

        contadorTiempo += Time.deltaTime;
        if (contadorTiempo >= intervaloCambioCamara)
        {
            
            contadorTiempo = 0f;

            indexCamara = (indexCamara == lstAreaSensibleCamara.Count-1) ? indexCamara = 0 : indexCamara += 1; //Regresa a cero si el index es el ultimo, y aumenta 1 si hay mas

        }
        

        if (lstAreaSensibleCamara[indexCamara].veAlJugador())
        {
            msgPantalla += "HOla";
        }
        else
        {
            msgPantalla += "Nada";
        }
        infoPantalla.text = msgPantalla;
    }
}
