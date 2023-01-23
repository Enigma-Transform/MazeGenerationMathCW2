using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code is developed using the help of a tutorial Master Procedural Maze & Dungeon Generation by Penny De Byl, Michael Bridges. 

public class AreaLocationRG
{
    public int x;
    public int z;

    public AreaLocationRG(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}
public class RandomMazeGenerator : MonoBehaviour

{

    public List<AreaLocationRG> direction = new List<AreaLocationRG>()
    {
        new AreaLocationRG(1,0),
        new AreaLocationRG(0,1),
        new AreaLocationRG(-1,0),
        new AreaLocationRG(0,-1)
    };
    public GameObject Cube;
    public int width, depth;

    public int[,] area;
    [SerializeField]
    GameObject Player;
    public int scale=6;
    public int no;
    public void Start()
    {
        no = Random.Range(0, 2);
        InitArea();
        if (no == 0)
        {
            Generate();
            Debug.Log("PRIMS");

        }
        else if(no == 1)
        {
            Debug.Log("RDFS");

            GenerateRDFS(Random.Range(1, width), Random.Range(1, depth));
        }
        DrawArea();
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if(area[x, z] == 0)
                {
                    Player.transform.position = new Vector3(x * scale, 1, z * scale);
                    
                } //1wall 0= corridor
            }
        }
    }
    private void Update()
    {

    }
    public  void Generate()
    {
        int x = 2;
        int z = 2;
        area[x, z] = 0;
        List<AreaLocationRG> walls = new List<AreaLocationRG>();
        walls.Add(new AreaLocationRG(x + 1, z));
        walls.Add(new AreaLocationRG(x - 1, z));
        walls.Add(new AreaLocationRG(x, z + 1));
        walls.Add(new AreaLocationRG(x, z - 1));



        while (walls.Count > 0)
        {
            int randomWall = Random.Range(0, walls.Count);
            x = walls[randomWall].x;
            z = walls[randomWall].z;
            walls.RemoveAt(randomWall);
            if (CountSquareNeighbours(x, z) == 1)
            {
                area[x, z] = 0;
                walls.Add(new AreaLocationRG(x + 1, z));
                walls.Add(new AreaLocationRG(x - 1, z));
                walls.Add(new AreaLocationRG(x, z + 1));
                walls.Add(new AreaLocationRG(x, z - 1));
            }

    
        }
    }
    public void InitArea()
    {
        area = new int[depth, width];
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                area[x, z] = 1; //1wall 0= corridor
                
            }
        }
    }
    public void GenerateRDFS(int x, int z)
    {
        for (int i = 0; i < 10; i++)
        {
            if (CountSquareNeighbours(x, z) >= 2)
            {
                return;
            }
            area[x, z] = 0;
            //   direction.shuffle();
        }

        RandomDirection(x, z);
    }
    void RandomDirection(int x, int z)
    {
        int randomNumber = Random.Range(0, 5);

        switch (randomNumber)
        {
            case 0:
                GenerateRDFS(x + 1, z);
                GenerateRDFS(x - 1, z);
                GenerateRDFS(x, z + 1);
                GenerateRDFS(x, z - 1);
                break;
            case 1:
                GenerateRDFS(x - 1, z);
                GenerateRDFS(x + 1, z);
                GenerateRDFS(x, z + 1);
                GenerateRDFS(x, z - 1);
                break;
            case 2:
                GenerateRDFS(x, z + 1);
                GenerateRDFS(x, z - 1);
                GenerateRDFS(x + 1, z);
                GenerateRDFS(x - 1, z);
                break;
            case 3:
                GenerateRDFS(x, z - 1);
                GenerateRDFS(x, z + 1);
                GenerateRDFS(x + 1, z);
                GenerateRDFS(x - 1, z);
                break;
            case 4:
                GenerateRDFS(x + 1, z);
                GenerateRDFS(x, z - 1);
                GenerateRDFS(x, z + 1);
                GenerateRDFS(x - 1, z);
                break;
            default:
                break;
        }
    }
    public void DrawArea()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (area[z, x] == 1)
                {
                    Vector3 pos = new Vector3(x*scale, 0, z*scale);
                    GameObject walls = Instantiate(Cube, pos, Quaternion.identity);
                    walls.transform.localScale = new Vector3(scale, scale, scale);
                    walls.transform.position = pos;
                }


            }
        }

    }
    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
        {
            return 5;
        }
        if (area[x - 1, z] == 0)
        {
            count++;
        }
        if (area[x + 1, z] == 0)
        {
            count++;
        }
        if (area[x, z + 1] == 0)
        {
            count++;
        }
        if (area[x, z - 1] == 0)
        {
            count++;
        }
        return count;
    }


}
