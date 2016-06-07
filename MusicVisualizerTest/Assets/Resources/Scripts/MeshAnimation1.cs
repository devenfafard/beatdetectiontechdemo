using UnityEngine;
using System.Collections;

public class MeshAnimation1 : MonoBehaviour
{
    private Vector3[] cleanVerts;
    private BeatDetectionEngine audioSource = null;
    private Mesh mesh;
    private float scale = 0.0f;
    private float waveSpeed = 1f;
    private float waveHeight = 2f;
    private int direction = 0;
    void Awake()
    {
        if (!mesh)
            mesh = GetComponent<MeshFilter>().mesh;
        cleanVerts = mesh.vertices;
        audioSource = GameObject.FindObjectOfType<BeatDetectionEngine>();
    }

    void FixedUpdate()
    {
        direction = Random.Range(0, 2);
    }

    void Update()
    {
        Vector3[] vertices = mesh.vertices;
        scale = audioSource.frequencyData;
        
        for (int i = 0; i < vertices.Length; i++)
        {          

            if (direction == 0)
            {
                float x = (vertices[i].x * scale) + (Time.time * waveSpeed);
                float z = (vertices[i].z * scale) + (Time.time * waveSpeed);
                vertices[i].y = cleanVerts[i].y + (Mathf.PerlinNoise(x, z) - 0.5f) * waveHeight;
            }
            if (direction == 1)
            {
                float y = (vertices[i].y * scale) + (Time.time * waveSpeed);
                float z = (vertices[i].z * scale) + (Time.time * waveSpeed);
                vertices[i].x = cleanVerts[i].x + (Mathf.PerlinNoise(y, z) - 0.5f) * waveHeight;
            }
            if (direction == 2)
            {
                float x = (vertices[i].x * scale) + (Time.time * waveSpeed);
                float y = (vertices[i].y * scale) + (Time.time * waveSpeed);
                vertices[i].z = cleanVerts[i].z + (Mathf.PerlinNoise(x, y) - 0.5f) * waveHeight;
            }
        }

        mesh.vertices = vertices;
    }
}
