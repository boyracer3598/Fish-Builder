using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public const float CellSize = 1f;
    [SerializeField] private BuildingData buildingData1;
    [SerializeField] private BuildingData buildingData2;
    [SerializeField] private BuildingData buildingData3;
    [SerializeField] private BuildingPreview previewPrefab;
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private BuildingGrid buildingGrid;
    private BuildingPreview preview;

    private void Update()
    {
        Vector3 mousePos = GetMousePosition();

        if (preview != null)
        {
            HandlePreview(mousePos);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                preview = CreatePreview(buildingData1, mousePos);
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                preview = CreatePreview(buildingData2, mousePos);
            }else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                preview = CreatePreview(buildingData3, mousePos);
            }
        }
    }

    private void HandlePreview(Vector3 mouseWorldPosition)
    {
        preview.transform.position = mouseWorldPosition;
        List<Vector3> buildingPositions = preview.BuildingModel.GetAllBuildingPosition();
        bool canBuild = grid.canBuil
    }
        
    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

    private BuildingPreview CreatePreview(BuildingData data, Vector3 position)
    {
        BuildingPreview buildingPreview = Instantiate(previewPrefab, position, Quaternion.identity);
        buildingPreview.Setup(data);
        return buildingPreview;
    }
    
}
