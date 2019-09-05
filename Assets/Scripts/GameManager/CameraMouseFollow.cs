using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseFollow : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5f;
    public float smoothing = 2f;
    public float horizontalSpeed = 2f;

    Vector2 md;
    GameObject character;

    private void Start()
    {
        character = transform.parent.gameObject;

    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            SetRotation();

            transform.localRotation = Quaternion.AngleAxis(mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }

        character.transform.position += new Vector3(0, 0, -Input.GetAxisRaw("Vertical") * Time.deltaTime * horizontalSpeed);
        
    }

    private void SetRotation()
    {
        md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, -90, 90);
    }
}
