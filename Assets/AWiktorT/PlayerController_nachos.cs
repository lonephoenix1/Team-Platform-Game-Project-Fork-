using UnityEngine;

public class PlayerController_nachos: MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Debug.Log("Move input: " + move); 
        Vector3 newPosition = transform.position + new Vector3(move, 0, 0);

        newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f);
        transform.position = newPosition;
    }
}
