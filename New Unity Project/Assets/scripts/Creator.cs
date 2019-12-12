using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public bool checkbox; // Variable booleana
    private Color[] colors = new Color[6]; // Array de colores

    void Start()
    {
        // Los colores determinados para el array
        colors[0] = Color.yellow;
        colors[1] = Color.blue;
        colors[2] = Color.red;
        colors[3] = Color.green;
        colors[4] = Color.cyan;
        colors[5] = Color.white;
    }

    void Update()
    {
        if (checkbox) // Si checkbox (booleano) se activa se ejecuta lo que sigue
        {
            checkbox = false;
            StartCoroutine(CreateSpheres());

        }
    }

    private IEnumerator CreateSpheres()
    {
        int width = Random.Range(3, 13); // Selecciona al azar el eje X
        int height = Random.Range(3, 13); // Selecciona al azar el eje Y

        // Define las posiciones y la creación de los GameObjects
        for (int i = 0; i < width; i++)
        {
            GameObject prevSphere = null; // Crea prevSphere como GameObject y se le asigna null
            for (int j = 0; j < height; j++)
            {
                GameObject createdSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); // Crea esferas primitivas
                createdSphere.GetComponent<Transform>().position = new Vector3(i * 1.0f, j * 1.0f, 0); // Asigna la posición a las esferas
                int randomColor = Random.Range(0, colors.Length); // Busca el índice al azar
                var actualColor = colors[randomColor]; // Determina el color en la posición seleccionada
                createdSphere.GetComponent<Renderer>().material.color = actualColor; // Asigna el color a la esfera creada
                yield return new WaitForSeconds(0.1f); // Crea las esferas de forma consecutiva
                if (prevSphere != null) // Si prevSphere no es nulo se ejecuta lo que sigue
                {
                    var prevColor = prevSphere.GetComponent<MeshRenderer>().material.color;// Selecciona el siguiente color
                    if (SameColor(actualColor, prevColor)) // Si ambos colores son iguales se ejecuta lo que sigue
                    {
                        createdSphere.GetComponent<MeshRenderer>().material.color = Color.black;
                        prevSphere.GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }
                prevSphere = createdSphere; // Cambia a la esfera anterior
            }
        }
    }

    // Verifica si ambos colores son iguales
    private bool SameColor(Color previous, Color selected)
    {
        return previous.Equals(selected);
    }
}
