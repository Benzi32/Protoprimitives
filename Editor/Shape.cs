using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape
{
    public MeshFilter meshfilter;
    public Mesh mesh;
    public MeshRenderer renderer;
    public GameObject shapeGO;
    public Material mat;

    public abstract Mesh CreateMesh();

    #region Parameters
    public virtual float length { get;set;}
    public virtual float width { get;set;}
    public virtual float height { get;set;}

    public virtual float bottomRadius { get; set; }
    public virtual float topRadius { get; set; }
    public virtual int nbSides { get; set; }
    public virtual int nbHeightSeg { get; set; }
    public virtual float radius { get; set; }
    public virtual float torusHoleRadius { get; set; }
    public virtual float torusOutRadius { get; set; }

    public virtual float tubeBottomHoleRadius { get; set; }
    public virtual float tubeBottomOutRadius { get; set; }
    public virtual float tubeTopHoleRadius { get; set; }
    public virtual float tubeTopOutRadius { get; set; }

    //CurvedStairs
    public virtual float innerRadius { get; set; }
    public virtual float stepHeight { get; set; }
    public virtual float stepWidth { get; set; }
    public virtual float angleOfCurve { get; set; }
    public virtual int numSteps { get; set; }
    public virtual float addToFirstStep { get; set; }
    public virtual bool counterClockwise { get; set; }

    //Linear Stair
    public virtual float stepLength { get; set; }
    public virtual int stepCount { get; set; }

    //Spiral stair
    public virtual float stepThickness { get; set; }
    public virtual int numStepsPer360 { get; set; }
    public virtual bool slopedCeiling { get; set; }
    public virtual bool slopedFloor { get; set; }


    #endregion
}
