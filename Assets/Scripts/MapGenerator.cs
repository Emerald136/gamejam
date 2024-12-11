using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("��������� �����")]
    public int width = 10; // ������ �����
    public int height = 10; // ������ �����

    [Header("�������")]
    public GameObject[] floorTiles; // ������ �������� ����
    public GameObject[] decorationTiles; // ������ �������� ���������
    public GameObject capsule; // ������ �������

    [Header("����� ������")]
    [Range(0, 1)] public float decorationSpawnChance = 0.2f; // ���� ��������� ���������
    [Range(0, 1)] public float capsuleSpawnChance = 0.1f; // ���� ��������� �������

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // ������� ���, ������� ��������� ������ �� �������
                GameObject floor = floorTiles[Random.Range(0, floorTiles.Length)];
                Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity);

                // �������� ������� ���������
                if (Random.value < decorationSpawnChance)
                {
                    GameObject decoration = decorationTiles[Random.Range(0, decorationTiles.Length)];
                    Instantiate(decoration, new Vector3(x, y, 0), Quaternion.identity);
                }

                // �������� ������� �������
                if (Random.value < capsuleSpawnChance)
                {
                    Instantiate(capsule, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }
}
