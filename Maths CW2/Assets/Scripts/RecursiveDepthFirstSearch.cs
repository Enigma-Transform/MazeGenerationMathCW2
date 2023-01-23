using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code is developed using the help of a tutorial Master Procedural Maze & Dungeon Generation by Penny De Byl, Michael Bridges. 

public class AreaLocationB
{
    public int x;
    public int z;

    public AreaLocationB(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}
public class RecursiveDepthFirstSearch : MonoBehaviour
{
    //Creating a list to store the direction in which the crawler should move next.
    public List<AreaLocationB> direction = new List<AreaLocationB>()
    {
        new AreaLocationB(1,0),
        new AreaLocationB(0,1),
        new AreaLocationB(-1,0),
        new AreaLocationB(0,-1)
    }; public GameObject[] Cube;
    public int width, depth;

    public int[,] area;
    public int scale = 6;
    public void buttonPress()
    {

        InitArea();
        Generate(Random.Range(1, width), Random.Range(1, depth));
        DrawArea();
    }

    //Responsible for generation the maze using RDFS
    //Takes 2 parameters x,z for the depth and width respectivley
    public void Generate(int x, int z)
    {
        for(int i = 0; i < 10; i++)
        {
            if (CountSquareNeighbours(x, z) >= 2)
            {
                return;
            }
            area[x, z] = 0;
        }
        //Calling the function RandomDirection
        RandomDirection(x, z);
    }

    //Selects a random number and then selects a random direction in which the crawler should move to next.
    void RandomDirection(int x, int z)
    {
        //Generating a random number between 0 and 5, 5 is exclusive so it generates random number from 0 to 4.
        int randomNumber = Random.Range(0, 5);

        switch (randomNumber)
        {
            case 0:
                //Recursivley calling the generate function to visit all the unvisited neighbours of the cell
                Generate(x + 1, z);
                Generate(x - 1, z);
                Generate(x, z + 1);
                Generate(x, z - 1);
                break;
            case 1:
                Generate(x - 1, z);
                Generate(x + 1, z);
                Generate(x, z + 1);
                Generate(x, z - 1);
                break;
            case 2:
                Generate(x, z + 1);
                Generate(x, z - 1);
                Generate(x + 1, z);
                Generate(x - 1, z);
                break;
            case 3:
                Generate(x, z - 1);
                Generate(x, z + 1);
                Generate(x + 1, z);
                Generate(x - 1, z);
                break;
            case 4:
                Generate(x + 1, z);
                Generate(x, z - 1);
                Generate(x, z + 1);
                Generate(x - 1, z);
                break;
            default:
                break;
        }
    }
    //Responsibe for drawing the maze with the given depth and width
    public void DrawArea()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (area[z, x] == 1)
                {
                    Vector3 pos = new Vector3(x*scale, 0, z*scale);
                    GameObject walls = Instantiate(Cube[Random.Range(0,Cube.Length)], pos, Quaternion.identity);
                    walls.transform.localScale = new Vector3(scale, scale, scale);

                    walls.transform.position = pos;
                }


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
                area[x, z] = 1; 
            }
        }
    }
    //This function returns an int and keeps track of the next 4 neighboring cells Top,down,left,right.
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
