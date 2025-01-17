using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public Transform[] parallaxLayers; // Alla bakgrundslager
    public float[] parallaxMultipliers; // Multiplikatorer för lagrens hastighet

    private Transform cameraTransform; // Kamerans transform
    private Vector3 lastCameraPosition; // Kamerans senaste position
    private float[] layerWidths; // Bredder för varje lager

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        // Initiera bredden på varje lager
        layerWidths = new float[parallaxLayers.Length];
        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            if (parallaxLayers[i] != null)
            {
                SpriteRenderer sr = parallaxLayers[i].GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    layerWidths[i] = sr.bounds.size.x; // Bredden av lagrets sprite
                }
                else
                {
                    Debug.LogWarning($"ParallaxLayer {i} saknar SpriteRenderer!");
                }
            }
        }
    }

    private void Update()
    {
        // Beräkna kamerans rörelse
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Flytta bakgrundslagren baserat på kamerans rörelse
        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            if (parallaxLayers[i] != null)
            {
                // Parallax-rörelse
                parallaxLayers[i].position += new Vector3(deltaMovement.x * parallaxMultipliers[i], 0, 0);

                // Kontrollera om lagret behöver flyttas (loopa om det är utanför kamerans vy)
                float cameraLeftEdge = cameraTransform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
                float cameraRightEdge = cameraTransform.position.x + Camera.main.orthographicSize * Camera.main.aspect;

                float layerLeftEdge = parallaxLayers[i].position.x - (layerWidths[i] / 2);
                float layerRightEdge = parallaxLayers[i].position.x + (layerWidths[i] / 2);

                // Flytta lagret till höger om det är för långt till vänster
                if (layerRightEdge < cameraLeftEdge)
                {
                    parallaxLayers[i].position += new Vector3(layerWidths[i], 0, 0);
                }

                // Flytta lagret till vänster om det är för långt till höger
                else if (layerLeftEdge > cameraRightEdge)
                {
                    parallaxLayers[i].position -= new Vector3(layerWidths[i], 0, 0);
                }
            }
        }

        // Uppdatera kamerans senaste position
        lastCameraPosition = cameraTransform.position;
    }
}
