using UnityEngine;
using System.Collections;
using UnityEditor;

public class Protoprimitives : EditorWindow
{
    string[] m_ShapeTypes = { "Box", "Cone", "Plane", "Sphere", "Torus", "Tube", "LinearStair", "CurvedStair", "SpiralStair"};
    enum EShapeTypes
    {
        BOX,
        CONE,
        PLANE,
        SPHERE,
        TORUS,
        TUBE,
        LINEAR_STAIR,
        CURVED_STAIR,
        SPIRAL_STAIR,
        NB_TYPES,
    }
    int m_ShapeIndex = 0;
    Shape shape;
    Shape currentShape;
    Vector3 pos = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    Vector3 scale = Vector3.one;
    Material mat;

    #region Parameters
    //Box
    float length = 1f;
    float width = 1f;
    float height = 1f;

    //Cone
    float bottomRadius = 0.5f;
    float topRadius = 0f;
    int nbSides = 18;
    int nbHeightSeg = 1;

    float radius = 0.5f;

    float torusHoleRadius = 0.3f;
    float torusOutRadius = 1f;

    float tubeBottomHoleRadius = 0.5f;
    float tubeBottomOutRadius = 0.15f;
    float tubeTopHoleRadius = 0.5f;
    float tubeTopOutRadius = 0.15f;

    //Curved stairs
    float innerRadius = 2f;
    float stepHeight = 0.2f;
    float stepWidth = 2f;
    float angleOfCurve = 90f;
    int numSteps = 10;
    float addToFirstStep = 0;
    bool counterClockwise = false;

    //Linear stair
    float stepLength = 0.3f;
    int stepCount = 10;

    //Spiral stair 
    float stepThickness = 0.5f;
    int numStepsPer360 = 10;
    bool slopedCeiling = false;
    bool slopedFloor = false;
    #endregion
    //Add menu Protoprimitives to "Tools" menu
    [MenuItem("Tools/Protoprimitives")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        Protoprimitives window = (Protoprimitives)EditorWindow.GetWindow(typeof(Protoprimitives));
        window.Show();
    }
    void OnGUI()
    {
        //Label
        GUILayout.Label("Choose your shape", EditorStyles.boldLabel);
        //Liste des shapes
        m_ShapeIndex = EditorGUILayout.Popup("Shape", m_ShapeIndex, m_ShapeTypes);
        EditorGUILayout.Space();
        //Position
        GUILayout.Label("Position", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal("Box");
        pos = EditorGUILayout.Vector3Field("Position", pos);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal("Box");
        rotation = EditorGUILayout.Vector3Field("Rotation", rotation);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal("Box");
        scale = EditorGUILayout.Vector3Field("Scale", scale);
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        mat = (Material)EditorGUILayout.ObjectField("Material", mat, typeof(Material), false);
        mat = new Material(Shader.Find("Diffuse"));
        EditorGUILayout.EndHorizontal();
        //Check chosen shape and adapt window with shape parameters

        switch (m_ShapeIndex)
        {
            case (int)EShapeTypes.BOX:
                length = EditorGUILayout.FloatField("Length", length);
                width = EditorGUILayout.FloatField("Width", width);
                height = EditorGUILayout.FloatField("Height", height);
                break;
            case (int)EShapeTypes.CONE:
                 height = EditorGUILayout.FloatField("Height", height); 
                 bottomRadius = EditorGUILayout.FloatField("BottomRadius", bottomRadius);
                //topRadius = EditorGUILayout.FloatField("TopRadius", topRadius);
                nbSides = EditorGUILayout.IntField("nbSides", nbSides); 
                 nbHeightSeg = EditorGUILayout.IntField("nbHeightSeg", nbHeightSeg); ;
                break;
            case (int)EShapeTypes.PLANE:
                length = EditorGUILayout.FloatField("Length", length);
                width = EditorGUILayout.FloatField("Width", width);
                break;
            case (int)EShapeTypes.SPHERE:
                radius = EditorGUILayout.FloatField("Radius", radius);
                break;
            case (int)EShapeTypes.TORUS:
                torusHoleRadius = EditorGUILayout.FloatField("Torus Thickness", torusHoleRadius);
                torusOutRadius = EditorGUILayout.FloatField("Hole Radius", torusOutRadius);
                break;
            case (int)EShapeTypes.TUBE:
                tubeBottomHoleRadius = EditorGUILayout.FloatField("Bottom Hole Radius", tubeBottomHoleRadius);
                tubeBottomOutRadius = EditorGUILayout.FloatField("Bottom Thickness", tubeBottomOutRadius);
                tubeTopHoleRadius = EditorGUILayout.FloatField("Top Hole Radius", tubeTopHoleRadius);
                tubeTopOutRadius = EditorGUILayout.FloatField("Top Thickness", tubeTopOutRadius);
                break;
            case (int)EShapeTypes.LINEAR_STAIR:
                stepLength = EditorGUILayout.FloatField("Step Length", stepLength);
                stepHeight = EditorGUILayout.FloatField("Step Height", stepHeight);
                stepWidth = EditorGUILayout.FloatField("Step Width", stepWidth);
                stepCount = EditorGUILayout.IntField("Nb Steps", stepCount);
                break;
            case (int)EShapeTypes.CURVED_STAIR:
                innerRadius = EditorGUILayout.FloatField("Inner Radius", innerRadius);
                stepHeight = EditorGUILayout.FloatField("Step Height", stepHeight);
                stepWidth = EditorGUILayout.FloatField("Step Width", stepWidth);
                numSteps = EditorGUILayout.IntField("Nb Steps", numSteps);
                angleOfCurve = EditorGUILayout.FloatField("Angle of Curve", angleOfCurve);
                addToFirstStep = EditorGUILayout.FloatField("Add height to first step", addToFirstStep);
                counterClockwise = EditorGUILayout.Toggle("Counter Clockwise", counterClockwise);
                break;
            case (int)EShapeTypes.SPIRAL_STAIR:
                innerRadius = EditorGUILayout.FloatField("Inner Radius", innerRadius);
                stepHeight = EditorGUILayout.FloatField("Space Between Step", stepHeight);
                stepWidth = EditorGUILayout.FloatField("Step Width", stepWidth);
                stepThickness = EditorGUILayout.FloatField("Step Thickness", stepThickness);
                numStepsPer360 = EditorGUILayout.IntField("Nb Steps per 360°", numStepsPer360);
                numSteps = EditorGUILayout.IntField("Nb Steps", numSteps);
                slopedCeiling = EditorGUILayout.Toggle("Sloped Ceiling", slopedCeiling);
                slopedFloor = EditorGUILayout.Toggle("Sloped Floor", slopedFloor);
                counterClockwise = EditorGUILayout.Toggle("Counter Clockwise", counterClockwise);
                break;
            default:
                break;
        }
        //Shape preview with another shape deleted every time you update and if you create the shape
        if (GUILayout.Button("Update shape preview"))
        {
            switch (m_ShapeIndex)
            {
                case (int)EShapeTypes.BOX:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<Box>("Box");
                    break;
                case (int)EShapeTypes.CONE:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<Cone>("Cone");
                    break;
                case (int)EShapeTypes.PLANE:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<Plane>("Plane");
                    break;
                case (int)EShapeTypes.SPHERE:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<Sphere>("Sphere");
                    break;
                case (int)EShapeTypes.TORUS:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<Torus>("Torus");
                    break;
                case (int)EShapeTypes.TUBE:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<Tube>("Tube");
                    break;
                case (int)EShapeTypes.LINEAR_STAIR:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<LinearStair>("LinearStair");
                    break;
                case (int)EShapeTypes.CURVED_STAIR:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<CurvedStair>("CurvedStair");
                    break;
                case (int)EShapeTypes.SPIRAL_STAIR:
                    if (currentShape != null)
                    {
                        DestroyImmediate(currentShape.shapeGO);
                    }
                    CreatePrevizualisation<SpiralStair>("SpiralStair");
                    break;
                default:
                    break;
            }
        }
        //Call template function to create the requiered shape
        if (GUILayout.Button("Create Shape"))
        {
            switch (m_ShapeIndex)
            {
                case (int)EShapeTypes.BOX:
                    CreateShape<Box>("Box");
                    break;
                case (int)EShapeTypes.CONE:
                    CreateShape<Cone>("Cone");
                    break;
                case (int)EShapeTypes.PLANE:
                    CreateShape<Plane>("Plane");
                    break;
                case (int)EShapeTypes.SPHERE:
                    CreateShape<Sphere>("Sphere");
                    break;
                case (int)EShapeTypes.TORUS:
                    CreateShape<Torus>("Torus");
                    break;
                case (int)EShapeTypes.TUBE:
                    CreateShape<Tube>("Tube");
                    break;
                case (int)EShapeTypes.LINEAR_STAIR:
                    CreateShape<LinearStair>("LinearStair");
                    break;
                case (int)EShapeTypes.CURVED_STAIR:
                    CreateShape<CurvedStair>("CurvedStair");
                    break;
                case (int)EShapeTypes.SPIRAL_STAIR:
                    CreateShape<SpiralStair>("SpiralStair");
                    break;
                default:
                    break;
            }
        }
    }

    void OnDestroy()
    {
        if (currentShape != null)
        {
            DestroyImmediate(currentShape.shapeGO);
        }
    }

    public void CreatePrevizualisation<T>(string _objectName) where T : Shape, new()
    {
        currentShape = new T();
        currentShape.shapeGO = new GameObject(_objectName);
        currentShape.shapeGO.transform.position = pos;
        currentShape.shapeGO.transform.rotation = Quaternion.Euler(rotation);
        currentShape.shapeGO.transform.localScale = scale;
        currentShape.mat = mat;
        currentShape.length = length;
        currentShape.width = width;
        currentShape.height = height;
        currentShape.bottomRadius = bottomRadius;
        currentShape.topRadius = topRadius;
        currentShape.nbSides = nbSides;
        currentShape.nbHeightSeg = nbHeightSeg;
        currentShape.radius = radius;
        currentShape.torusHoleRadius = torusHoleRadius;
        currentShape.torusOutRadius = torusOutRadius;
        currentShape.tubeBottomHoleRadius = tubeBottomHoleRadius;
        currentShape.tubeBottomOutRadius = tubeBottomOutRadius;
        currentShape.tubeTopHoleRadius = tubeTopHoleRadius;
        currentShape.tubeTopOutRadius = tubeTopOutRadius;
        //Curved stairs
        currentShape.innerRadius = innerRadius;
        currentShape.stepHeight = stepHeight;
        currentShape.stepWidth = stepWidth;
        currentShape.numSteps = numSteps;
        currentShape.angleOfCurve = angleOfCurve;
        currentShape.addToFirstStep = addToFirstStep;
        currentShape.counterClockwise = counterClockwise;
        //Linear stair
        currentShape.stepLength = stepLength;
        currentShape.stepCount = stepCount;
        //Spiral stair
        currentShape.stepThickness = stepThickness;
        currentShape.numStepsPer360 = numStepsPer360;
        currentShape.slopedCeiling = slopedCeiling;
        currentShape.slopedFloor = slopedFloor;
        currentShape.CreateMesh();

    }

    public void CreateShape<T>(string _objectName) where T : Shape, new()
    {
        if (currentShape != null)
        {
            DestroyImmediate(currentShape.shapeGO);
        }
        shape = new T();
        shape.shapeGO = new GameObject(_objectName);
        shape.shapeGO.transform.position = pos;
        shape.shapeGO.transform.rotation = Quaternion.Euler(rotation);
        shape.shapeGO.transform.localScale = scale;
        shape.mat = mat;
        shape.length = length;
        shape.width = width;
        shape.height = height;
        shape.bottomRadius = bottomRadius;
        shape.topRadius = topRadius;
        shape.nbSides = nbSides;
        shape.nbHeightSeg = nbHeightSeg;
        shape.radius = radius;
        shape.torusHoleRadius = torusHoleRadius;
        shape.torusOutRadius = torusOutRadius;
        shape.tubeBottomHoleRadius = tubeBottomHoleRadius;
        shape.tubeBottomOutRadius = tubeBottomOutRadius;
        shape.tubeTopHoleRadius = tubeTopHoleRadius;
        shape.tubeTopOutRadius = tubeTopOutRadius;
        //Curved stairs
        shape.innerRadius = innerRadius;
        shape.stepHeight = stepHeight;
        shape.stepWidth = stepWidth;
        shape.numSteps = numSteps;
        shape.angleOfCurve = angleOfCurve;
        shape.addToFirstStep = addToFirstStep;
        shape.counterClockwise = counterClockwise;
        //Linear stair
        shape.stepLength = stepLength;
        shape.stepCount = stepCount;
        //Spiral stair
        shape.stepThickness = stepThickness;
        shape.numStepsPer360 = numStepsPer360;
        shape.slopedCeiling = slopedCeiling;
        shape.slopedFloor = slopedFloor;
        shape.CreateMesh();
    }
}