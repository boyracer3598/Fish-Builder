using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    private BuildingGridCell[,] grid;

    private void Start() {
        grid = new BuildingGridCell[width, height];
        for (int x=0; x<grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++) {
                grid[x, y] = new();
            }
    }

    

}

    public class BuildingGridCell
    {
        private Building building;

        public void SetBuilding(Building building) { this.building = building; }

        public bool IsEmpty() { return building == null; }


    }
