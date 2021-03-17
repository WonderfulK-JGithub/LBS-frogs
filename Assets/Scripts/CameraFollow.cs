using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    //Den här offset är viktig för att spelarens z-axel är på 0, medans cameran är på -10
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    [SerializeField] float minY = 0;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow ()
    {
        //Vi tar positionen av spelaren + offset.
        Vector3 targetPosition = player.position + offset;
        //Den här gör att kameran väntar en liten stund innan den flyttar sig till spelaren. 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = targetPosition;

        //Ser till att kameran inte går under y positionen som sätts i variabeln minY
        float yCord = Mathf.Clamp(transform.position.y, minY, 1000);

        transform.position = new Vector3(targetPosition.x, yCord,targetPosition.z);
    }
}
