using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Maze : MonoBehaviour
{
	[System.Serializable]
	public class Cell{
		public bool visited;
		public GameObject north; //1
		public GameObject east; //2
		public GameObject west;  //3
		public GameObject south; //4

	}
	public GameObject wall;
	public float wallLength=1.0f;
	public int xSize=5;
	public int ySize=5;
	private Vector3 initialPos;
	private GameObject wallHolder;
	private Cell[] cells;
	private int currentCell=0;
	private int totalCells;
	private int visitedCells=0;
	private bool startedBuilding=false;
	private int currentNeighbour=0;
	private List<int> lastCells;
	private int backingUp=0;
	private int wallToBreak=0;
	private Vector3 offSetVector;
	[SerializeField] private GameObject player;
	[SerializeField] private Canvas canvas;
	[SerializeField] private GameObject plane;
	private InputField wallLengthInput;


    // Start is called before the first frame update
    void Start()
    {
        //CreateWalls();
    }

    void CreateWalls(){
   
      wallHolder = new GameObject();
      wallHolder.name= "Maze";

      initialPos= new Vector3 ((-xSize/2)+ wallLength/2, 0.0f,(-ySize/2)+wallLength/2);
      Vector3 myPos=initialPos;
      GameObject tempWall;

      //For the x axis

      for (int i=0;i<ySize;i++){
      	for(int j=0;j<=xSize;j++){
      		myPos=new Vector3(initialPos.x +(j*wallLength)-wallLength/2,0.0f,initialPos.z+ (i*wallLength)-wallLength/2);
      		tempWall=Instantiate(wall,myPos,Quaternion.identity) as GameObject;
      		tempWall.transform.parent=wallHolder.transform;
      		tempWall.transform.localScale= new Vector3(0.5f,1f,wallLength);
      	}
      }

      //For the y axis

      for (int i=0;i<=ySize;i++){
      	for(int j=0;j<xSize;j++){
      		myPos=new Vector3(initialPos.x +(j*wallLength),0.0f,initialPos.z+ (i*wallLength)-wallLength);
      		tempWall= Instantiate(wall,myPos,Quaternion.Euler(0.0f,90.0f,0.0f)) as GameObject;
      		tempWall.transform.parent=wallHolder.transform;
      		tempWall.transform.localScale= new Vector3(0.5f,1f,wallLength);

      	}
      }
      CreateCells();

    }
    void CreateCells(){
    	lastCells=new List<int>();
    	lastCells.Clear();
    	totalCells=xSize*ySize;
    	GameObject[] allWalls;
    	int children = wallHolder.transform.childCount;
    	allWalls=new GameObject[children];
    	cells= new Cell[xSize*ySize];
    	int eastWestProcess=0;
    	int childProcess=0;
    	int termcCount=0;

    	// Get All the children
    	for(int i=0;i<children;i++){
    		allWalls[i]=wallHolder.transform.GetChild(i).gameObject;
    	}
    	// Assigns walls to the cells
    	for(int cellprocess=0;cellprocess<cells.Length;cellprocess++){

    		if (termcCount==xSize){
    			eastWestProcess++;
    			termcCount=0;
    		} 
    		cells[cellprocess]=new Cell();
    		cells[cellprocess].east= allWalls[eastWestProcess];
    		cells[cellprocess].south=allWalls[childProcess+(xSize+1)*ySize];
    		if(cellprocess==0){
    			Vector3 vects= new Vector3 (1.64f, 0.0f, -0.3f);// Play with these for the perfect spawn
    			Debug.Log(cells[cellprocess].east.GetComponent<Transform>().position);
    			offSetVector = new Vector3(cells[cellprocess].east.GetComponent<Transform>().position.x + vects.x,
    				cells[cellprocess].east.GetComponent<Transform>().position.y,cells[cellprocess].east.GetComponent<Transform>().position.z-vects.z);
    			player.transform.position=offSetVector;
    			//Debug.Log(offSetVector);
    		}
    			eastWestProcess++;

    		termcCount++;
    		childProcess++;
    		cells[cellprocess].west=allWalls[eastWestProcess];
    		cells[cellprocess].north=allWalls[(childProcess+(xSize+1)*ySize)+xSize-1];
    	}
    	CreateMaze();
    }

    void CreateMaze(){
    	while(visitedCells<totalCells){
    		if(startedBuilding){
    			GiveMeNeighbour();
    			if(cells[currentNeighbour].visited==false && cells[currentCell].visited==true){
    				BreakWall();
    				cells[currentNeighbour].visited=true;
    				visitedCells++;
    				lastCells.Add(currentCell);
    				currentCell=currentNeighbour;
    				if (lastCells.Count>0){
    					backingUp=lastCells.Count-1;
    				}
    			}
    		}
    		else{
    			currentCell=Random.Range(0,totalCells);
    			cells[currentCell].visited=true;
    			visitedCells++;
    			startedBuilding=true;
    		}


    	}
    	Debug.Log("Finished");
    	
    }

    void BreakWall(){
    	switch(wallToBreak){
    		case 1: Destroy(cells[currentCell].north); break;
    		case 2: Destroy(cells[currentCell].east); break;
    		case 3: Destroy(cells[currentCell].west); break;
    		case 4: Destroy(cells[currentCell].south); break;


    	}

    }

    void GiveMeNeighbour(){
    	
    	int length = 0;
    	int[] neighbours=new int[4];
    	int[] connectingWall=new int[4];
    	int check=0;
    	check=((currentCell+1)/xSize);
    	check-=1;
    	check*=xSize;
    	check+=xSize;

    	//west
    	if(currentCell+1<totalCells && (currentCell+1)!=check){
    		if(cells[currentCell+1].visited==false){
    			neighbours[length]=currentCell+1;
    			connectingWall[length]=3;
    			length++;
    		}
    	}

    	//east
    	if(currentCell-1>=0 && currentCell!=check){
    		if(cells[currentCell-1].visited==false){
    			neighbours[length]=currentCell-1;
    			connectingWall[length]=2;
    			length++;
    		}
    	}

    	//north
    	if(currentCell +xSize< totalCells){
    		if(cells[currentCell+xSize].visited==false){
    			neighbours[length]=currentCell+xSize;
    			connectingWall[length]=1;
    			length++;
    		}
    	}



    	//south
    	if(currentCell -xSize>= 0){
    		if(cells[currentCell -xSize].visited==false){
    			neighbours[length]=currentCell-xSize;
    			connectingWall[length]=4;
    			length++;
    		}
    	}

    	if(length!=0){
    		int theChosenOne=Random.Range(0,length);
    		currentNeighbour=neighbours[theChosenOne];
    		wallToBreak=connectingWall[theChosenOne];
    	} 
    	else { 
    		if(backingUp>0){
    			currentCell=lastCells[backingUp];
    			backingUp--;
    		}

    	}



      }

      public void SetWallLength()
      {
      	wallLengthInput = canvas.transform.GetChild(1).GetComponent<InputField>();
      	wallLength =int.Parse(wallLengthInput.text);
      }

       public void SetXSize()
      {
      	wallLengthInput = canvas.transform.GetChild(2).GetComponent<InputField>();
      	xSize =int.Parse(wallLengthInput.text);
      }

       public void SetYSize()
      {
      	wallLengthInput = canvas.transform.GetChild(3).GetComponent<InputField>();
      	ySize =int.Parse(wallLengthInput.text);
      }


      public void StartGenerateMaze()
      {
      	Destroy(wallHolder);
      	plane.transform.localScale = new Vector3(xSize, plane.transform.localScale.y, ySize);
      	CreateWalls();
      	canvas.gameObject.SetActive(false);
      }

    // Update is called once per frame
    void Update()
    {
        
    }
}
