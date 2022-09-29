using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public Transform laserEyes;
    public float laserDuration = 0.2f;
    public Color laserColorStart = Color.blue;
    public Color laserColorEnd = Color.red;
    public Material laserMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            RaycastHit hitInfo;
            if (Physics.Raycast(laserEyes.position, transform.forward, out hitInfo, 500)) {
                DrawLine(laserEyes.position, hitInfo.point, laserColorStart, laserColorEnd, laserMaterial, laserDuration);
                print($"Hit an object: {hitInfo.collider}");
            }
            else
            {
                print("Did not hit an object :(");
            }
        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color startColor, Color endColor, Material material, float duration)
    {
        GameObject laser = new GameObject();
        laser.transform.position = start;
        laser.AddComponent<LineRenderer>();
        LineRenderer line = laser.GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetColors(startColor, endColor);
        line.SetWidth(0.1f, 0.1f);
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        Destroy(laser, duration);
    }
}
