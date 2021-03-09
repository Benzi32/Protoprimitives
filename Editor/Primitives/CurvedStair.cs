using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedStair : Shape
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

        int vertices_length = 20 * numSteps + 4;
        int normals_length = 20 * numSteps + 4;
        int triangles_length = 30 * numSteps + 6;
        int uv_length = 20 * numSteps + 4;
        int step;

        Vector3[] vertices = new Vector3[vertices_length];
        int[] triangles = new int[triangles_length];
        Vector3[] normales = new Vector3[normals_length];
        Vector2[] uvs = new Vector2[uv_length];
        Vector3 coord_0;
        Vector3 coord_1;
        Vector3 coord_2 = new Vector3(0, 0, 0);
        Vector3 coord_3 = new Vector3(0, 0, 0);

        float degrees;
        Vector3 pos;
        Vector3 pos_start;
        Vector3 pos_end;
        float dist;
        float dist_inner;

        step = 20;
        for (int i = 0, count = 0; i < (numSteps * step) - 4; i += step, count++)
        {
            if (counterClockwise)
            {
                degrees = 180 + ((angleOfCurve / numSteps) * count);
            }
            else
            {
                degrees = 180 - ((angleOfCurve / numSteps) * count);
            }

            pos = getStartPosVectorFromAngle(degrees);
            pos_start = pos * innerRadius;
            pos_end = pos_start + (pos * stepWidth);

            if (counterClockwise)
            {
                coord_0 = new Vector3(pos_start.x, addToFirstStep + stepHeight + stepHeight * count, pos_start.z);
                coord_1 = new Vector3(pos_end.x, addToFirstStep + stepHeight + stepHeight * count, pos_end.z);
            }
            else
            {
                coord_0 = new Vector3(pos_end.x, addToFirstStep + stepHeight + stepHeight * count, pos_end.z);
                coord_1 = new Vector3(pos_start.x, addToFirstStep + stepHeight + stepHeight * count, pos_start.z);
            }

            vertices[i + 0] = coord_0;
            vertices[i + 1] = coord_1;

            if (counterClockwise)
            {
                degrees = 180 + ((angleOfCurve / numSteps) * (count + 1));
            }
            else
            {
                degrees = 180 - ((angleOfCurve / numSteps) * (count + 1));
            }

            pos = getStartPosVectorFromAngle(degrees);
            pos_start = pos * innerRadius;
            pos_end = pos_start + (pos * stepWidth);

            if (counterClockwise)
            {
                coord_2 = new Vector3(pos_start.x, addToFirstStep + stepHeight + stepHeight * count, pos_start.z);
                coord_3 = new Vector3(pos_end.x, addToFirstStep + stepHeight + stepHeight * count, pos_end.z);
            }
            else
            {
                coord_2 = new Vector3(pos_end.x, addToFirstStep + stepHeight + stepHeight * count, pos_end.z);
                coord_3 = new Vector3(pos_start.x, addToFirstStep + stepHeight + stepHeight * count, pos_start.z);
            }
        #region Vertices
            vertices[i + 2] = coord_2;
            vertices[i + 3] = coord_3;

            // front
            vertices[i + 4] = new Vector3(coord_0.x, stepHeight * count, coord_0.z);
            vertices[i + 5] = new Vector3(coord_1.x, stepHeight * count, coord_1.z);
            vertices[i + 6] = new Vector3(coord_0.x, addToFirstStep + stepHeight + (stepHeight * count), coord_0.z);
            vertices[i + 7] = new Vector3(coord_1.x, addToFirstStep + stepHeight + (stepHeight * count), coord_1.z);

            // right side
            vertices[i + 8] = new Vector3(coord_1.x, 0, coord_1.z);
            vertices[i + 9] = new Vector3(coord_3.x, 0, coord_3.z);
            vertices[i + 10] = new Vector3(coord_1.x, coord_1.y, coord_1.z);
            vertices[i + 11] = new Vector3(coord_3.x, coord_3.y, coord_3.z);

            // left side
            vertices[i + 12] = new Vector3(coord_2.x, 0, coord_2.z);
            vertices[i + 13] = new Vector3(coord_0.x, 0, coord_0.z);
            vertices[i + 14] = new Vector3(coord_2.x, coord_2.y, coord_2.z);
            vertices[i + 15] = new Vector3(coord_0.x, coord_0.y, coord_0.z);

            // bottom
            vertices[i + 16] = new Vector3(coord_2.x, 0, coord_2.z);
            vertices[i + 17] = new Vector3(coord_3.x, 0, coord_3.z);
            vertices[i + 18] = new Vector3(coord_0.x, 0, coord_0.z);
            vertices[i + 19] = new Vector3(coord_1.x, 0, coord_1.z);
        }

        // back
        vertices[vertices_length - 4] = new Vector3(coord_3.x, 0, coord_3.z);
        vertices[vertices_length - 3] = new Vector3(coord_2.x, 0, coord_2.z);
        vertices[vertices_length - 2] = new Vector3(coord_3.x, coord_3.y, coord_3.z);
        vertices[vertices_length - 1] = new Vector3(coord_2.x, coord_2.y, coord_2.z);
        #endregion
        #region Triangles
        // set triangle array
        step = 6;
        for (int i = 0, count = 0; i < triangles.Length; i += step, count += 1)
        {
            //  Lower left triangle.
            triangles[i] = 0 + (count * 4);
            triangles[i + 1] = 2 + (count * 4);
            triangles[i + 2] = 1 + (count * 4);

            //  Upper right triangle.
            triangles[i + 3] = 2 + (count * 4);
            triangles[i + 4] = 3 + (count * 4);
            triangles[i + 5] = 1 + (count * 4);
        }
        #endregion
        #region Normales
        step = 20;
        for (int i = 0; i < (normales.Length - 4); i += step)
        {
            // top
            normales[i + 0] = -Vector3.down;
            normales[i + 1] = -Vector3.down;
            normales[i + 2] = -Vector3.down;
            normales[i + 3] = -Vector3.down;

            // front
            normales[i + 4] = -Vector3.forward;
            normales[i + 5] = -Vector3.forward;
            normales[i + 6] = -Vector3.forward;
            normales[i + 7] = -Vector3.forward;

            // right
            normales[i + 8] = Vector3.right;
            normales[i + 9] = Vector3.right;
            normales[i + 10] = Vector3.right;
            normales[i + 11] = Vector3.right;

            // left
            normales[i + 12] = Vector3.left;
            normales[i + 13] = Vector3.left;
            normales[i + 14] = Vector3.left;
            normales[i + 15] = Vector3.left;

            // bottom
            normales[i + 16] = Vector3.down;
            normales[i + 18] = Vector3.down;
            normales[i + 19] = Vector3.down;
            normales[i + 17] = Vector3.down;
        }

        normales[normals_length - 4] = -Vector3.forward;
        normales[normals_length - 3] = -Vector3.forward;
        normales[normals_length - 2] = -Vector3.forward;
        normales[normals_length - 1] = -Vector3.forward;
        #endregion
        #region UVS
        step = 20;
        for (int i = 0, count = 0; i < vertices.Length - 4; i += step, count += 1)
        {
            dist = Vector3.Distance(vertices[i], vertices[i + 2]);
            dist_inner = Vector3.Distance(vertices[i + 1], vertices[i + 3]);

            // top
            uvs[0 + i] = new Vector2(vertices[0].x, vertices[0].z);
            uvs[1 + i] = new Vector2(vertices[1].x, vertices[1].z + 0.25f);
            uvs[2 + i] = new Vector2(vertices[0].x, vertices[2].z);
            uvs[3 + i] = new Vector2(vertices[1].x, vertices[3].z + 0.25f);

            // front
            uvs[4 + i] = new Vector2(0, 0);
            uvs[5 + i] = new Vector2(stepWidth, 0);
            uvs[6 + i] = new Vector2(0, addToFirstStep + stepHeight);
            uvs[7 + i] = new Vector2(stepWidth, addToFirstStep + stepHeight);

            // right
            uvs[8 + i] = new Vector2(0, vertices[i + 8].y);
            uvs[9 + i] = new Vector2(dist_inner, vertices[i + 9].y);
            uvs[10 + i] = new Vector2(0, vertices[i + 10].y);
            uvs[11 + i] = new Vector2(dist_inner, vertices[i + 11].y);

            // left
            uvs[12 + i] = new Vector2(dist, vertices[12 + i].y);
            uvs[13 + i] = new Vector2(0, vertices[13 + i].y);
            uvs[14 + i] = new Vector2(dist, vertices[14 + i].y);
            uvs[15 + i] = new Vector2(0, vertices[15 + i].y);

            // bottom
            uvs[16 + i] = new Vector2(vertices[0].x, vertices[0].z);
            uvs[17 + i] = new Vector2(vertices[1].x, vertices[1].z + 0.25f);
            uvs[18 + i] = new Vector2(vertices[0].x, vertices[2].z);
            uvs[19 + i] = new Vector2(vertices[1].x, vertices[3].z + 0.25f);
        }

        dist = Vector3.Distance(vertices[vertices_length - 1], vertices[vertices_length - 2]);

        // back
        uvs[uv_length - 4] = new Vector2(0, vertices[vertices_length - 4].y);
        uvs[uv_length - 3] = new Vector2(dist, vertices[vertices_length - 3].y);
        uvs[uv_length - 2] = new Vector2(0, vertices[vertices_length - 2].y);
        uvs[uv_length - 1] = new Vector2(dist, vertices[vertices_length - 1].y);

        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.Optimize();
        return mesh;
    }

    private Vector3 getStartPosVectorFromAngle(float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians);
        float z = Mathf.Sin(radians);

        return new Vector3(x, 0, z);
    }
}
