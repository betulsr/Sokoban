using System;
using static System.Console;

namespace Bme121
{
    static class Sokoban
    {      
        static int curRow ; // Find the row coordinate of the user
        static int curColumn ; // Find the column  coordinate of the user
        public static bool hasWon= false; // is hasWon = True, means Win 
        static int numberOfTarget; //Calculate the number of goals (Winning point)
        
        static int[ , ] map = new int[8,8] //Create a Sokoban map
        {
           {0,0,1,1,1,0,0,0},
           {0,0,1,4,1,0,0,0},
           {0,0,1,2,1,1,1,1},
           {1,1,1,0,0,2,4,1},
           {1,4,0,2,3,1,1,1},
           {1,1,1,1,2,1,0,0},
           {0,0,0,1,4,1,0,0},
           {0,0,0,1,1,1,0,0} 
           
           
        };
        
        // Draw the Sokoban map
        public static void Draw()
        {
            
            string pd = "\u0023"; // pound sign (Wall)
            string at = "\u0040"; // at sign (Player)
            string bO = "\u004F"; // capital O (boxes being pushed)
            string pl = "\u002B"; // plus sign(target locations)
            string sp = " ";     // space (None)  
            string bX = "\u0058"; // capital X (a box successfully pushed to its target location.)
            Clear();
            for (int i=0; i<map.GetLength(0);i++)
            {
                for (int j=0; j<map.GetLength(1);j++)
                {
                    if(map[i,j] == 1) Write(" "+ pd);                    
                    if(map[i,j] == 3) Write(" "+ at);
                    if(map[i,j] == 2) Write(" "+ bO);
                    if(map[i,j] == 4) Write(" "+ pl);
                    if(map[i,j] == 0) Write(" "+ sp);
                    if(map[i,j] == 5) Write(" "+ bX);
                    
                }
                WriteLine();
            }
        }
        
        //Find the user current location
        public static void FindUser()
        {
            for (int i=0; i<map.GetLength(0);i++)
            {
                for (int j=0; j<map.GetLength(1);j++)
                {
                    if(map[i,j]==3) 
                    {
                        curRow    = i;
                        curColumn = j;
                    }
                    
                }
            }
        }
        
        //When the user move up
        public static void Up()
        {    
            FindUser(); //Find user
            //Check whether the next coordinate is space(if Yes, do sth)
            if(map[curRow-1,curColumn]==0)     
            {
                map[curRow-1,curColumn]=3;
                map[curRow,curColumn]=0;
            }
            
            //Check whether the next coordinate is box and 
            //whether there is a target location behind the box (If Yes, do sth)
            if(map[curRow-1,curColumn]==2 && map[curRow-2,curColumn] == 4)
            {
                map[curRow,curColumn] = 0;
                map[curRow-1,curColumn] = 3;
                map[curRow-2,curColumn] = 5;
            }
            //Check whether the next coordinate is box and 
            //whether there is a space behind the box (If Yes, do sth)
            if(map[curRow-1,curColumn]==0 && map[curRow-1,curColumn] == 2)
            {
                map[curRow,curColumn] = 0;
                map[curRow,curColumn-1] = 3;
                map[curRow,curColumn-2] = 2;
            }      
           //update the map
           Draw();
           //check if wins
           WinPoint(); 
        }
        
        
         public static void Down()
        {    
            FindUser();
            if(map[curRow+1,curColumn]==0)
            {
                map[curRow+1,curColumn]=3;
                map[curRow,curColumn]=0;
            }
            if(map[curRow+1,curColumn]==2 && map[curRow+2,curColumn] == 4)
            {
                map[curRow,curColumn] = 0;
                map[curRow+1,curColumn] = 3;
                map[curRow+2,curColumn] = 5;
            }
            if(map[curRow+1,curColumn]==0 && map[curRow+1,curColumn] == 2)
            {
                map[curRow,curColumn] = 0;
                map[curRow,curColumn] = 3;
                map[curRow,curColumn] = 2;
            }      
           
           Draw(); 
           WinPoint();
        }
        
        public static void Left()
        {   
            FindUser();           
            if(map[curRow,curColumn-1]==0)
            {
                map[curRow,curColumn-1]=3;
                map[curRow,curColumn]=0;
            }
            if(map[curRow,curColumn-1]==2 && map[curRow,curColumn-2] == 4)
            {
                map[curRow,curColumn] = 0;
                map[curRow,curColumn-1] = 3;
                map[curRow,curColumn-2] = 5;
            }
            if(map[curRow,curColumn-2]==0 && map[curRow,curColumn-1] == 2)
            {
                map[curRow,curColumn] = 0;
                map[curRow,curColumn-1] = 3;
                map[curRow,curColumn-2] = 2;
            }           
           Draw();
           WinPoint(); 
        }
        public static void Right()
        {   
            FindUser();           
            if(map[curRow,curColumn+1]==0)
            {
                map[curRow,curColumn+1]=3;
                map[curRow,curColumn]=0;
            }
            if(map[curRow,curColumn+1]==2 && map[curRow,curColumn+2] == 4)
            {
                map[curRow,curColumn] = 0;
                map[curRow,curColumn+1] = 3;
                map[curRow,curColumn+2] = 5;
            }
            if(map[curRow,curColumn+2]==0 && map[curRow,curColumn+1] == 2)
            {
                map[curRow,curColumn] = 0;
                map[curRow,curColumn+1] = 3;
                map[curRow,curColumn+2] = 2;
            }           
           Draw(); 
           WinPoint();
        }
        
        //Calculate the number of target to know the winning points
        public static void CountTarget()
        {
            
            for (int i=0; i<map.GetLength(0);i++)
            {
                for (int j=0; j<map.GetLength(1);j++)
                {
                    if(map[i,j]==4) 
                    {
                        numberOfTarget++;
                    }                    
                }
            }
           
        }
        
        //number of boxes are placed in the right location 
        //Need to calculate the number of Capital X in each round 
        public static void WinPoint()
        {
            int numberOfCorrectLocation = 0;
            for (int i=0; i<map.GetLength(0);i++)
            {
                for (int j=0; j<map.GetLength(1);j++)
                {
                    if(map[i,j]==5) 
                    {
                        numberOfCorrectLocation++;
                    }
                    
                }
            } 
            //when the number of capital X equal to the number of target location
            //win!
            if(numberOfCorrectLocation==numberOfTarget)
            {
                hasWon = true;
            }
        }
    }
    
    static class Program
    {
        
        static void Main( )
        {   
            
            Sokoban.CountTarget();
            Sokoban.FindUser();
            Sokoban.Draw();
            
            //input the direction from user
            while(!Sokoban.hasWon)
            {
                WriteLine("Please input u,d,r or l to move the user(@)");
                WriteLine("u for moving up    ; d for moving down; ");
                WriteLine("r for moving right ; l for moving left; ");
                string input = (ReadLine());
                switch(input)
                {
                    case "u":                    
                        Sokoban.Up();
                        break;
                                        
                    case "d": 
                        Sokoban.Down();
                        break; 
                    case "r": 
                        Sokoban.Right();
                        break;
                    
                    case "l": 
                        Sokoban.Left();
                        break;
                }
            
            }
            WriteLine("Congratulations!!!You win.");
        }
    }
}
