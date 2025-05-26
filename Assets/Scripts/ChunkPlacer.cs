using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    public Transform Player; //положение игрока
    public Chunk[] ChunkPrefabs; //префабы заранее придуманых чанков
    public Chunk FirstChunk; //Первый чанк с которого игрок начинает игру
    Vector3 offset = new Vector3(25, 0, 0); //расстояние через которое спавнится след чанк
    public int currentDifficulty; //сложность на данный момент уровня
    public int nextDifficultyThreshold; //координата Х следующего повышения сложности
    public List<Chunk> spawnedChunks = new List<Chunk>(); //Список заспавленных чанков
    void Start()
    {
        spawnedChunks.Add(FirstChunk); //добавляем первый чанк в список заспавленных чанков
        //currentDifficulty = 1; //текущая сложность установлена на 1
        nextDifficultyThreshold = -75; //повышение сложности по координате Х=75
        //SpawnChunk(); //Спавним три чанка с начала уровня, 
        //SpawnChunk(); //чтобы перед игроком был уровень 
        //SpawnChunk(); //и он не видел генерацию игры
        
    }
    public void SpawnChunk() //спавним чанк
    {
        Chunk newChunk = Instantiate(GetRandomChunk());
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].transform.position - offset;
        AssignBonusesToChunk(newChunk); 
        spawnedChunks.Add(newChunk);
        if (spawnedChunks[0].transform.position.x - Player.position.x > 25f) //если игрок удалился от первого чана в списке, то удалить самый старый
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }//если игрок удалился от первого чана в списке, то удалить самый старый
        if ((Player.position.x <= nextDifficultyThreshold) && (currentDifficulty < 8)) //повысить сложность и следующую контрольную точку
        {
            currentDifficulty += 1;
            nextDifficultyThreshold -= 125;
        }//повысить сложность и следующую контрольную точку
    }
    Chunk GetRandomChunk() //Получение случайного чанка в зависимости от сложности
    {
        // Собираем все чанки, подходящие по сложности
        List<Chunk> suitableChunks = new List<Chunk>();

        foreach (Chunk chunk in ChunkPrefabs)
        {
            if (chunk.difficulty <= currentDifficulty)
            {
                suitableChunks.Add(chunk);
            }
        }
        // Возвращаем случайный из подходящих
        return suitableChunks[Random.Range(0, suitableChunks.Count)];
    }
    void AssignBonusesToChunk(Chunk chunk) 
    {
        // 4 и 8 — вообще нет стен, пропускаем
        if (chunk.wallnum == 4 || chunk.wallnum == 8)
            return;

        // Обработка обычной стены Bonus Wall
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

        // Обработка стены WalkingBonus
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

        string tag = isPositive ? BonusHelper.GetRandomPositiveBonusTag() : BonusHelper.GetRandomNegativeBonusTag(); //устанавливается тег в зависимости от его цвета
        wall.tag = tag;

        // Обновляем текст на ребёнке
        Transform textObj = wall.Find("Text (TMP)");
        if (textObj != null)
        {
            var tmp = textObj.GetComponent<TMPro.TextMeshPro>();
            if (tmp != null)
            {
                tmp.text = BonusHelper.GetTextFromTag(tag); //на стенку вставляется текст соответствующий установленому тегу
            }
        }
    }
}   