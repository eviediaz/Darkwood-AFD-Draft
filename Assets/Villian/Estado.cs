using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FSMUsil
{

    public class Estado
    {
        public string nombre;
        public string textoPrueba;

        public virtual void Start(BichoScript bicho) 
        {
            Console.WriteLine("Start!");
            textoPrueba = "Start!";
        }

        public virtual Estado Update(BichoScript bicho)
        {
            Console.WriteLine("Update del Bicho!");
            textoPrueba = "Update del Bicho!";
            return new Estado();
        }
    }

}