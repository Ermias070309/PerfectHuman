using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public Transform[] parallaxLayers; // Alla bakgrundslager
    public float[] parallaxMultipliers; // Multiplikatorer f�r lagrens hastighet

    private Transform cameraTransform; // Kamerans transform
    private Vector3 lastCameraPosition; // Kamerans senaste position
    private float[] layerWidths; // Bredder f�r varje lager

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        // Initiera bredden p� varje lager
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
        // Ber�kna kamerans r�relse
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Flytta bakgrundslagren baserat p� kamerans r�relse
        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            if (parallaxLayers[i] != null)
            {
                // Parallax-r�relse
                parallaxLayers[i].position += new Vector3(deltaMovement.x * parallaxMultipliers[i], 0, 0);

                // Kontrollera om lagret beh�ver flyttas (loopa om det �r utanf�r kamerans vy)
                float cameraLeftEdge = cameraTransform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
                float cameraRightEdge = cameraTransform.position.x + Camera.main.orthographicSize * Camera.main.aspect;

                float layerLeftEdge = parallaxLayers[i].position.x - (layerWidths[i] / 2);
                float layerRightEdge = parallaxLayers[i].position.x + (layerWidths[i] / 2);

                // Flytta lagret till h�ger om det �r f�r l�ngt till v�nster
                if (layerRightEdge < cameraLeftEdge)
                {
                    parallaxLayers[i].position += new Vector3(layerWidths[i], 0, 0);
                }

                // Flytta lagret till v�nster om det �r f�r l�ngt till h�ger
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
