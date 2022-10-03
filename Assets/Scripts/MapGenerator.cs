using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int coinsCount, spikesCount;
    public int row, col;
    public float ratioX, ratioY;

    public GameObject spikePrefab, coinPrefab;

    public List<Vector2> positions = new List<Vector2>();
    public List<GameObject> coins = new List<GameObject>();

    void Start()
    {
        Calculate();
        Generate(spikesCount, spikePrefab);
        Generate(coinsCount, coinPrefab, coins);
    }

    void Calculate()
    {
        float aspect = (float) Screen.width / Screen.height;
        float Height = Camera.main.orthographicSize * 2;
        float Width = Height * aspect;

        float offsetWidth = Width * ratioX;
        float offsetHeight = Height * ratioY;

        float stepVertical = (Width - offsetWidth * 2) / (col - 1);
        float stepHorizontal = (Height - offsetHeight * 2) / (row - 1);
        Vector2 startPoint = new Vector2(Width / -2 + offsetWidth, Height / 2 - offsetHeight);

        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                var pointPosition = startPoint + Vector2.down * stepHorizontal * y + Vector2.right * stepVertical * x;
                if (pointPosition != Vector2.zero)
                    positions.Add(pointPosition);
            }
        }
    }

    void Generate(int count, GameObject prefab, List<GameObject> list = null)
    {
        for (int i = 0; i < count; i++)
        {
            if (positions.Count == 0)
                break;

            int randomIndex = Random.Range(0, positions.Count);
            GameObject temp = Instantiate(prefab, positions[randomIndex], Quaternion.identity, this.transform);
            if (list != null)
                list.Add(temp);
            positions.RemoveAt(randomIndex);
        }
    }
}
