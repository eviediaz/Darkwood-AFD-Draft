using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptAreaSensible : MonoBehaviour
{

    public BoxCollider2D colliderArea;
    public SpriteRenderer compSpriteRenderer;
    public CircleCollider2D colliderJugador;

    private void Start()
    {
        colliderJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (colliderArea.IsTouching(colliderJugador))
        {
            compSpriteRenderer.color = Color.red;
        }
        else
        {
            compSpriteRenderer.color = Color.white;
        }
    }

    public bool veAlJugador()
    {
        return colliderArea.IsTouching(colliderJugador);
    }

}
