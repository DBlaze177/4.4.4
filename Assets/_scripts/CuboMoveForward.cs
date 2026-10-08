using UnityEngine;
using UnityEngine.InputSystem;

public class CuboMoveForward : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distanciaCarriles = 3f;
    [SerializeField] private float velocidadCambioCarril = 5f;

    private int currentLane = 0; //entendemos como -1 línea izquierda; 0, línea central y 1, línea derecha.
    


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime; //avance continuo en eje Z

        if (Keyboard.current != null)
        {
            var keyboard = Keyboard.current;
            if ((keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame) && currentLane > -1)
            {
                currentLane--;
            }

            else if ((keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)&& currentLane < 1) 
            {
                currentLane++;
            }


        }





        //Para cambiar de carril:
        float targetX = currentLane * distanciaCarriles;
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position, 
            targetPosition, 
            velocidadCambioCarril * Time.deltaTime
        );



    }
}
