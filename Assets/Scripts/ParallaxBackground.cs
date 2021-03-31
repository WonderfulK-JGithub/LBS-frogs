using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    //kod för parallax effekt på bakgrunden - KJ



    [SerializeField] float parallaxEffectX = 0;
    [SerializeField] float parallaxEffectY = 0;
    [SerializeField] Transform cam = null;
    float startPosX;
    float startPosY;


    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    
    void FixedUpdate()
    {
        //hur långt åt x leden bakgrunden ska vara - KJ
        float distanceX = cam.position.x * parallaxEffectX;
        float distanceY = cam.position.y * parallaxEffectY;

        //ger bakgrunden anpassad position
        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);
    }
}
