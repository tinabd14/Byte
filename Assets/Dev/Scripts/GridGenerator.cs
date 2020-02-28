using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject grid;
    [SerializeField] private int row;
    [SerializeField] private int col;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Material specialCubeMaterial;
    [SerializeField] private int specialCubeIndex;


    public static List<GameObject> cubes;


    private void Start()
    {
        cubes = new List<GameObject>();
        GenerateGrid();
        MarkSpecialCube();
    }


    private void GenerateGrid()
    {
        float space = 0.1f;

        grid.transform.position = new Vector3(grid.transform.position.x - col / 2, grid.transform.position.y, grid.transform.position.z);

        for (int i = 0; i < row; i++)
        {
            float offsetZ = i * (cubePrefab.transform.localScale.z + space);
            for (int j = 0; j < col; j++)
            {
                float offsetX = j * (cubePrefab.transform.localScale.x + space);
                GameObject cube = Instantiate(cubePrefab, grid.transform);
                cube.transform.SetParent(grid.transform);
                Vector3 position = new Vector3(grid.transform.position.x + offsetX, grid.transform.position.y, grid.transform.position.z + offsetZ);
                cube.transform.position = position;
                cubes.Add(cube);
            }
        }

    }

    private void MarkSpecialCube()
    {
        if (cubes[specialCubeIndex].GetComponent<MeshRenderer>() == null)
        {
            cubes[specialCubeIndex].transform.GetChild(0).GetComponent<MeshRenderer>().material = specialCubeMaterial;

        }
        else
        {
            cubes[specialCubeIndex].GetComponent<MeshRenderer>().material = specialCubeMaterial;
        }
        cubes[specialCubeIndex].tag = "Special Cube";
    }
}
