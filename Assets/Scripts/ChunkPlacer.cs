using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    public Transform Player; //��������� ������
    public Chunk[] ChunkPrefabs; //������� ������� ���������� ������
    public Chunk FirstChunk; //������ ���� � �������� ����� �������� ����
    Vector3 offset = new Vector3(25, 0, 0); //���������� ����� ������� ��������� ���� ����
    public int currentDifficulty; //��������� �� ������ ������ ������
    public int nextDifficultyThreshold; //���������� � ���������� ��������� ���������
    public List<Chunk> spawnedChunks = new List<Chunk>(); //������ ������������ ������
    void Start()
    {
        spawnedChunks.Add(FirstChunk); //��������� ������ ���� � ������ ������������ ������
        //currentDifficulty = 1; //������� ��������� ����������� �� 1
        nextDifficultyThreshold = -75; //��������� ��������� �� ���������� �=75
        //SpawnChunk(); //������� ��� ����� � ������ ������, 
        //SpawnChunk(); //����� ����� ������� ��� ������� 
        //SpawnChunk(); //� �� �� ����� ��������� ����
        
    }
    public void SpawnChunk() //������� ����
    {
        Chunk newChunk = Instantiate(GetRandomChunk());
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].transform.position - offset;
        AssignBonusesToChunk(newChunk); 
        spawnedChunks.Add(newChunk);
        if (spawnedChunks[0].transform.position.x - Player.position.x > 25f) //���� ����� �������� �� ������� ���� � ������, �� ������� ����� ������
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }//���� ����� �������� �� ������� ���� � ������, �� ������� ����� ������
        if ((Player.position.x <= nextDifficultyThreshold) && (currentDifficulty < 8)) //�������� ��������� � ��������� ����������� �����
        {
            currentDifficulty += 1;
            nextDifficultyThreshold -= 125;
        }//�������� ��������� � ��������� ����������� �����
    }
    Chunk GetRandomChunk() //��������� ���������� ����� � ����������� �� ���������
    {
        // �������� ��� �����, ���������� �� ���������
        List<Chunk> suitableChunks = new List<Chunk>();

        foreach (Chunk chunk in ChunkPrefabs)
        {
            if (chunk.difficulty <= currentDifficulty)
            {
                suitableChunks.Add(chunk);
            }
        }
        // ���������� ��������� �� ����������
        return suitableChunks[Random.Range(0, suitableChunks.Count)];
    }
    void AssignBonusesToChunk(Chunk chunk) 
    {
        // 4 � 8 � ������ ��� ����, ����������
        if (chunk.wallnum == 4 || chunk.wallnum == 8)
            return;

        // ��������� ������� ����� Bonus Wall
        if (chunk.wallnum >= 1 && chunk.wallnum <= 3 || chunk.wallnum == 5)
        {
            Transform bonusWall = chunk.transform.Find("Bonus Wall");
            if (bonusWall == null) return;

            Transform left = bonusWall.Find("Left");
            Transform right = bonusWall.Find("Right");

            switch (chunk.wallnum)
            {
                case 1: SetWallBonus(left, true); SetWallBonus(right, false); break;
                case 2: SetWallBonus(left, true); SetWallBonus(right, true); break;
                case 3: SetWallBonus(left, false); SetWallBonus(right, false); break;
                case 5: SetWallBonus(left, false); SetWallBonus(right, true); break;
            }
        }

        // ��������� ����� WalkingBonus
        else if (chunk.wallnum == 6 || chunk.wallnum == 7)
        {
            Transform walkingBonus = chunk.transform.Find("WalkingBonus");
            if (walkingBonus == null) return;

            Transform left = walkingBonus.Find("Left +");
            if (left == null) return;

            bool isPositive = chunk.wallnum == 6;
            SetWallBonus(left, isPositive);
        }
    }
    void SetWallBonus(Transform wall, bool isPositive) 
    {
        if (wall == null) return;

        string tag = isPositive ? BonusHelper.GetRandomPositiveBonusTag() : BonusHelper.GetRandomNegativeBonusTag(); //��������������� ��� � ����������� �� ��� �����
        wall.tag = tag;

        // ��������� ����� �� ������
        Transform textObj = wall.Find("Text (TMP)");
        if (textObj != null)
        {
            var tmp = textObj.GetComponent<TMPro.TextMeshPro>();
            if (tmp != null)
            {
                tmp.text = BonusHelper.GetTextFromTag(tag); //�� ������ ����������� ����� ��������������� ������������� ����
            }
        }
    }
}   