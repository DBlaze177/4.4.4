using UnityEngine;
using UnityEngine.InputSystem;

public class RTSCamera : MonoBehaviour
{


    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float edgeThreshold = 20f; //grosor del borde de la cámara
    [SerializeField] private bool useLimits = false;
    [SerializeField] private Vector2 minLimits = new Vector2(-50f, -50f);
    [SerializeField] private Vector2 maxLimits = new Vector2(50f, 50f);



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        Vector2 mousePos = Input.mousePosition;

        //Aquí compruebo los bordes horizontales
        if (mousePos.x <= edgeThreshold && mousePos.x >= 0)
        {
            direction.x = -1f;
        }
        else if (mousePos.x >= Screen.width - edgeThreshold && mousePos.x <= Screen.width)
        {
            direction.x = 1f;
        }
        //Ahora compruebo los bordes verticales
        if (mousePos.y <= edgeThreshold && mousePos.y >= 0)
        {
            direction.z = -1f;
        }
        else if (mousePos.y >= Screen.height - edgeThreshold && mousePos.y <= Screen.height)
        {
            direction.z = 1f;
        }

        //normalizo para que el movimiento diagonal no sea más rápido, lo de siempre.
        if (direction != Vector3.zero)
        {
            direction.Normalize();  

            Vector3 targetPosition = transform.position + direction * (moveSpeed * Time.deltaTime);

            if (useLimits)
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x, minLimits.x, maxLimits.x);
                targetPosition.z = Mathf.Clamp(targetPosition.z, minLimits.y, maxLimits.y);
            }
            transform.position = targetPosition;


        }


    }
}
