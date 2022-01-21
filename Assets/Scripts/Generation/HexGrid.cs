using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;

    public Text cellLabelPrefab;

    Canvas gridCanvas;

    HexCell[] cells;

    HexMesh hexMesh;

    PathLine pathLine;

    HexCoordinate startPoint, endPoint;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        pathLine = GetComponent<PathLine>();

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    private void Start()
    {
        hexMesh.Triangulate(cells);
        var path = PathFinding.CreatePath(new HexCoordinate(0, 0), new HexCoordinate(-1, 2));
        foreach (var item in path)
        {
            Debug.Log(item);
        }
        pathLine.CreateLine(path);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            HandleInput();

        if (Input.GetMouseButton(1))
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                var position = transform.InverseTransformPoint(hit.point);
                HexCoordinate coordinate = HexCoordinate.FromPosition(position);
                endPoint = coordinate;
                pathLine.CreateLine(PathFinding.CreatePath(startPoint, endPoint));
            }
        }
        
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
            TouchCell(hit.point);
    }

    void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinate coordinate = HexCoordinate.FromPosition(position);
        Debug.Log("touched at: " + coordinate.ToString());
        startPoint = coordinate;
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 pos;
        pos.x = (x + z * 0.5f - z / 2) * (HexMetricks.innerRadius * 2);
        pos.y = 0;
        pos.z = z * (HexMetricks.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = pos;
        cell.coordinate = HexCoordinate.FromOffsetCoordinates(x, z);

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, true);
        label.rectTransform.anchoredPosition = new Vector3(pos.x, pos.z, 0);
        label.rectTransform.localRotation = Quaternion.Euler(Vector3.zero);
        label.text = cell.coordinate.ToStringOnSeparateLines();
    }
}
