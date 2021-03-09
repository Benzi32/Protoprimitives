using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearStair : Shape
{
    public override Mesh CreateMesh()
    {
        mesh = new Mesh();
        meshfilter = new MeshFilter();
        renderer = new MeshRenderer();
        meshfilter = shapeGO.AddComponent<MeshFilter>();
        shapeGO.GetComponent<MeshFilter>().sharedMesh = mesh;
        renderer = shapeGO.AddComponent<MeshRenderer>();
        shapeGO.GetComponent<MeshRenderer>().sharedMaterial = mat;

        int vertices_length = 16 * (stepCount + 2);
        int normals_length = 16 * (stepCount + 2);
        int triangles_length = 24 * (stepCount + 2);
        int uv_length = 16 * (stepCount + 2);

        Vector3[] vertices = new Vector3[vertices_length];
        int[] triangles = new int[triangles_length];
        Vector3[] normales = new Vector3[normals_length];
        Vector2[] uvs = new Vector2[uv_length];

        int step = 16;
        for (int i = 0; i < (stepCount * step); i += step)
        {
            // front
            vertices[i + 0] = new Vector3(0, (i * stepHeight) / step, (i * stepLength) / step);
            vertices[i + 1] = new Vector3(stepWidth, (i * stepHeight) / step, (i * stepLength) / step);
            vertices[i + 2] = new Vector3(0, stepHeight + (i * stepHeight) / step, (i * stepLength) / step);
            vertices[i + 3] = new Vector3(stepWidth, stepHeight + (i * stepHeight) / step, (i * stepLength) / step);

            // right
            vertices[i + 4] = new Vector3(stepWidth, (i * stepHeight) / step, (i * stepLength) / step);
            vertices[i + 5] = new Vector3(stepWidth, (i * stepHeight) / step, stepLength * stepCount);
            vertices[i + 6] = new Vector3(stepWidth, stepHeight + (i * stepHeight) / step, (i * stepLength) / step);
            vertices[i + 7] = new Vector3(stepWidth, stepHeight + (i * stepHeight) / step, stepLength * stepCount);

            // left
            vertices[i + 8] = new Vector3(0, (i * stepHeight) / step, stepLength * stepCount);
            vertices[i + 9] = new Vector3(0, (i * stepHeight) / step, (i * stepLength) / step);
            vertices[i + 10] = new Vector3(0, stepHeight + (i * stepHeight) / step, stepLength * stepCount);
            vertices[i + 11] = new Vector3(0, stepHeight + (i * stepHeight) / step, (i * stepLength) / step);

            // top
            vertices[i + 12] = new Vector3(0, stepHeight + (i * stepHeight) / step, (i * stepLength) / step);
            vertices[i + 13] = new Vector3(stepWidth, stepHeight + (i * stepHeight) / step, (i * stepLength) / step);
            vertices[i + 14] = new Vector3(0, stepHeight + (i * stepHeight) / step, stepLength + (i * stepLength) / step);
            vertices[i + 15] = new Vector3(stepWidth, stepHeight + (i * stepHeight) / step, stepLength + (i * stepLength) / step);
        }

        // bottom
        vertices[vertices_length - 8] = new Vector3(stepWidth, 0, 0);
        vertices[vertices_length - 7] = new Vector3(0, 0, 0);
        vertices[vertices_length - 6] = new Vector3(stepWidth, 0, stepLength * stepCount);
        vertices[vertices_length - 5] = new Vector3(0, 0, stepLength * stepCount);

        // back
        vertices[vertices_length - 4] = new Vector3(stepWidth, 0, stepLength * stepCount);
        vertices[vertices_length - 3] = new Vector3(0, 0, stepLength * stepCount);
        vertices[vertices_length - 2] = new Vector3(stepWidth, stepHeight * stepCount, stepLength * stepCount);
        vertices[vertices_length - 1] = new Vector3(0, stepHeight * stepCount, stepLength * stepCount);

        // set triangle array
        step = 6;
        for (int i = 0; i < triangles.Length; i += step)
        {
            //  Lower left triangle.
            triangles[i] = 0 + (i * 4) / step;
            triangles[i + 1] = 2 + (i * 4) / step;
            triangles[i + 2] = 1 + (i * 4) / step;

            //  Upper right triangle.
            triangles[i + 3] = 2 + (i * 4) / step;
            triangles[i + 4] = 3 + (i * 4) / step;
            triangles[i + 5] = 1 + (i * 4) / step;
        }

        step = 16;
        for (int i = 0; i < normales.Length; i += step)
        {
            normales[i + 0] = -Vector3.forward;
            normales[i + 1] = -Vector3.forward;
            normales[i + 2] = -Vector3.forward;
            normales[i + 3] = -Vector3.forward;

            normales[i + 4] = Vector3.right;
            normales[i + 5] = Vector3.right;
            normales[i + 6] = Vector3.right;
            normales[i + 7] = Vector3.right;

            normales[i + 8] = Vector3.left;
            normales[i + 9] = Vector3.left;
            normales[i + 10] = Vector3.left;
            normales[i + 11] = Vector3.left;

            normales[i + 12] = -Vector3.down;
            normales[i + 13] = -Vector3.down;
            normales[i + 14] = -Vector3.down;
            normales[i + 15] = -Vector3.down;
        }

        normales[normals_length - 8] = -Vector3.up;
        normales[normals_length - 7] = -Vector3.up;
        normales[normals_length - 6] = -Vector3.up;
        normales[normals_length - 5] = -Vector3.up;

        normales[normals_length - 4] = -Vector3.back;
        normales[normals_length - 3] = -Vector3.back;
        normales[normals_length - 2] = -Vector3.back;
        normales[normals_length - 1] = -Vector3.back;

        step = 16;
        for (int i = 0, count = 0; i < (stepCount * step); i += step, count += 1)
        {
            // front
            uvs[0 + i] = new Vector2(0, 0);
            uvs[1 + i] = new Vector2(stepWidth, 0);
            uvs[2 + i] = new Vector2(0, stepHeight);
            uvs[3 + i] = new Vector2(stepWidth, stepHeight);

            // right
            uvs[4 + i] = new Vector2(vertices[i + 4].z, vertices[i + 4].y);
            uvs[5 + i] = new Vector2(vertices[i + 5].z, vertices[i + 5].y);
            uvs[6 + i] = new Vector2(vertices[i + 6].z, vertices[i + 6].y);
            uvs[7 + i] = new Vector2(vertices[i + 7].z, vertices[i + 7].y);

            // left
            uvs[8 + i] = new Vector2(vertices[i + 8].z, vertices[i + 8].y);
            uvs[9 + i] = new Vector2(vertices[i + 9].z, vertices[i + 9].y);
            uvs[10 + i] = new Vector2(vertices[i + 10].z, vertices[i + 10].y);
            uvs[11 + i] = new Vector2(vertices[i + 11].z, vertices[i + 11].y);

            // top
            uvs[12 + i] = new Vector2(0, 0);
            uvs[13 + i] = new Vector2(stepWidth, 0);
            uvs[14 + i] = new Vector2(0, stepLength);
            uvs[15 + i] = new Vector2(stepWidth, stepLength);
        }

        // bottom
        uvs[uv_length - 8] = new Vector2(vertices[vertices_length - 8].x, vertices[vertices_length - 8].z);
        uvs[uv_length - 7] = new Vector2(vertices[vertices_length - 7].x, vertices[vertices_length - 7].z);
        uvs[uv_length - 6] = new Vector2(vertices[vertices_length - 6].x, vertices[vertices_length - 6].z);
        uvs[uv_length - 5] = new Vector2(vertices[vertices_length - 5].x, vertices[vertices_length - 5].z);

        // back
        uvs[uv_length - 4] = new Vector2(vertices[vertices_length - 4].x, vertices[vertices_length - 4].y);
        uvs[uv_length - 3] = new Vector2(vertices[vertices_length - 3].x, vertices[vertices_length - 3].y);
        uvs[uv_length - 2] = new Vector2(vertices[vertices_length - 2].x, vertices[vertices_length - 2].y);
        uvs[uv_length - 1] = new Vector2(vertices[vertices_length - 1].x, vertices[vertices_length - 1].y);


        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.Optimize();
        return mesh;
    }
}
