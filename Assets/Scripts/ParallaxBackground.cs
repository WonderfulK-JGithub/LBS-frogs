using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    //kod för parallax effekt på bakgrunden - KJ
    
    [SerializeField] float parallaxEffect = 0;
    [SerializeField] Transform cam = null;
    float startPos;

    
    void Start()
    {
        startPos = transform.position.x;
    }

    
    void FixedUpdate()
    {
        //hur långt åt x leden bakgrunden ska vara - KJ
        float distance = cam.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
