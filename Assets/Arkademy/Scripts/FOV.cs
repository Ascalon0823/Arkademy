using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;


public class FOV : MonoBehaviour
{
    public float distance;
    [Range(0f, 360f)] public float rangeInDgr;
    [SerializeField] private Material fovMat;
    [SerializeField] [Range(0.03f,1f)]private float resolution;
    [SerializeField] private LayerMask blocker;
    private Mesh _mesh;

    private void Start()
    {
        var mf = GetComponent<MeshFilter>();
        if (mf == null) mf = gameObject.AddComponent<MeshFilter>();
        _mesh = new Mesh();
        mf.sharedMesh = _mesh;
        var mr = GetComponent<MeshRenderer>();
        if (mr == null) mr = gameObject.AddComponent<MeshRenderer>();
        mr.receiveShadows = false;
        mr.shadowCastingMode = ShadowCastingMode.Off;
        mr.sharedMaterial = fovMat;
        _verts  = new List<Vector3>();
        _faces = new List<int>();
    }

    private void LateUpdate()
    {
        UpdateFOV();
    }

    private List<Vector3> _verts;
    private List<int> _faces;
    private void UpdateFOV()
    {
        int step = Mathf.RoundToInt(resolution * rangeInDgr);
        float perStepAngleInDgr = rangeInDgr / step;
        _verts.Clear();
        _verts.Add(Vector3.zero);
        var center = transform.position;
        for (int i = 0; i <=step; i++)
        {
            float angle = transform.eulerAngles.y - rangeInDgr / 2f + perStepAngleInDgr * i;
            var dir = Utils.DirFromAngle(angle);
            var point = Physics.Raycast(center, dir, out var hit, distance, blocker)?
                hit.point : center + dir.normalized* distance;
            _verts.Add(transform.InverseTransformPoint(point));
        }
        _mesh.SetVertices(_verts);
        if (_faces.Count() != step * 3)
        {
            _faces.Clear();
            for (int i = 1; i < _verts.Count()-1; i++)
            {
                _faces.Add(0);
                _faces.Add(i);
                _faces.Add(i+1);
            }
            _mesh.SetIndices(_faces,MeshTopology.Triangles,0);
            _mesh.RecalculateNormals();
        }
    }
}