using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public const float CellSize = 1f;
    [SerializeField] private BuildingData buildingData1;
    [SerializeField] private BuildingData buildingData2;
    [SerializeField] private BuildingData buildingData3;
    [SerializeField] private BuildingPreview previewPrefab;
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private BuildingGrid grid;
    private BuildingPreview preview;
    InputAction selectBuilding1;
    InputAction selectBuilding2;
    InputAction selectBuilding3;
    InputAction rotateBuilding; 
    InputAction buildInput;

    void Start()
    {
        selectBuilding1 = InputSystem.actions["SelectBuilding1"];
        selectBuilding2 = InputSystem.actions["SelectBuilding2"];
        selectBuilding3 = InputSystem.actions["SelectBuilding3"];
        rotateBuilding = InputSystem.actions["rotateBuilding"];
        buildInput = InputSystem.actions["build"];
    }
    
    
    private void Update()
    {
        Vector3 mousePos = GetMousePosition();

        if (preview != null)
        {
            HandlePreview(mousePos);
        }
        
        if (selectBuilding1.IsPressed())
        {
            if (preview != null)
            {
                Destroy(preview.gameObject); 
            }
            preview = CreatePreview(buildingData1, mousePos);
        }else if (selectBuilding2.IsPressed())
        {
            if (preview != null)
            {
                Destroy(preview.gameObject);
            }
            preview = CreatePreview(buildingData2, mousePos);
        }else if (selectBuilding3.IsPressed())
        {
            print("press 3");
            if (preview != null)
            {
                Destroy(preview.gameObject);
            }
            preview = CreatePreview(buildingData3, mousePos);
        }
    }

    private void HandlePreview(Vector3 mouseWorldPosition)
    {
        preview.transform.position = mouseWorldPosition;
        List<Vector3> buildPositions = preview.BuildingModel.GetAllBuildingPosition();
        bool canBuild = grid.CanBuild(buildPositions);
        if (canBuild)
        {
            preview.transform.position = GetSnappedCenterPosition(buildPositions);
            preview.ChangeState(BuildingPreview.BuildingPreviewState.POSITIVE);
            if (buildInput.IsPressed())
            {
                PlaceBuilding(buildPositions);
            }
        }
        else
        {
            preview.ChangeState(BuildingPreview.BuildingPreviewState.NEGATIVE);
        }
        if (rotateBuilding.IsPressed())
        {
            preview.Rotate(90);
        }
    }
    
    private void PlaceBuilding(List<Vector3> buildingPositions)
    {
        Building building = Instantiate(buildingPrefab, preview.transform.position, Quaternion.identity);
        building.Setup(preview.Data, preview.BuildingModel.Rotation);
        grid.SetBuilding(building, buildingPositions);
        Destroy(preview.gameObject);
        preview = null;
    }

    private Vector3 GetSnappedCenterPosition(List<Vector3> allBuildingPositions)
    {
        List<int> xs = allBuildingPositions.Select(p => Mathf.FloorToInt(p.x)).ToList();
        List<int> zs = allBuildingPositions.Select(p => Mathf.FloorToInt(p.z)).ToList();
        float centerX = (xs.Min() + xs.Max()) / 2f + CellSize / 2f;
        float centerZ = (zs.Min() + zs.Max()) / 2f + CellSize / 2f;
        return new(centerX,0,centerZ);

    }
    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y,0));
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
