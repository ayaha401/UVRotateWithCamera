using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPos;

    [SerializeField]
    private float rotationSpeed = 50.0f;

    [SerializeField]
    private bool sinMove = false;

    [SerializeField]
    private float width = 10.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if(!sinMove)
        {
            this.transform.RotateAround(targetPos, Vector3.up, rotationSpeed * Time.deltaTime);
            transform.LookAt(targetPos);
        }
        else
        {
            float xPos = Mathf.Sin(Time.time) * width;
            this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
        }
    }
}
