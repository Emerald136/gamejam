using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject floorBlockPrefab;  // Префаб блока пола
    public GameObject wallBlockPrefab;   // Префаб стены
    public int roomWidth = 10;           // Ширина комнаты (в блоках)
    public int roomHeight = 5;           // Высота комнаты (в блоках)
    public float blockSize = 1f;         // Размер блока (размер спрайта)

    void Start()
    {
        GenerateRoom();
    }

    void GenerateRoom()
    {
        // Создание пола
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                Vector3 position = new Vector3(x * blockSize, y * blockSize, 0);
                Instantiate(floorBlockPrefab, position, Quaternion.identity, transform);
            }
        }

        // Создание границ (стен)
        for (int x = -1; x <= roomWidth; x++) // Горизонтальные стены
        {
            Vector3 topWallPosition = new Vector3(x * blockSize, roomHeight * blockSize, 0); // Верхняя стена
            Vector3 bottomWallPosition = new Vector3(x * blockSize, -blockSize, 0);          // Нижняя стена
            Instantiate(wallBlockPrefab, topWallPosition, Quaternion.identity, transform);
            Instantiate(wallBlockPrefab, bottomWallPosition, Quaternion.identity, transform);
        }

        for (int y = 0; y < roomHeight; y++) // Вертикальные стены
        {
            Vector3 leftWallPosition = new Vector3(-blockSize, y * blockSize, 0);            // Левая стена
            Vector3 rightWallPosition = new Vector3(roomWidth * blockSize, y * blockSize, 0); // Правая стена
            Instantiate(wallBlockPrefab, leftWallPosition, Quaternion.identity, transform);
            Instantiate(wallBlockPrefab, rightWallPosition, Quaternion.identity, transform);
        }
    }
}
