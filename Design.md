# C# Sudoku Application Design

# 1. Introduction

As the sudoku library is intended to be a standalone feature, i.e. usable by other developers to create their own graphical implementation on top of it, it is essentially that the results returned from the library is a full compilation - it should *not* return partial data in small packets that are built bespoke for my graphical implementation. For that reason, specific stores of information should be included in separate classes, the objects of which will be the return values of the library. The data structure of moves, rows/columns/blocks, tiles, and the board should specifically be standardised.

# 2. Standard Data Structures

## 2.1 Move

*This data structure should represent the information about any move returned from the analytical part of the sudoku engine.*

- Reference to the tile that is being changed
- New value of the referenced tile
- Array of references to the segments that are relevant to the reasoning behind the move. 
- Given reason why this move is logically consistent (as enum)

## 2.2 Segment

*This data structure should represent the information about any segment, i.e. rows, columns & blocks. All segments are semantically the same; information about their type is not necessary.*

- List of references to tiles contained within segment
- List of values already taken for the segment

## 2.3 Tile

*This data structure should represent the information about any given tile on the sudoku board.*

- Value in the tile (nullable)
- Array of references to the segments which contain the tile

## 2.4 Board

*This data structure should represent all information about the entire sudoku board being used; of n length where<br>n % 3 = 0*

- List of tiles (0-based; across then down)
- List of segments (27 long in standard sudoku; 9 rows, 9 columns, 9 blocks)


## Backtracking Algorithm

One of the more complicated algorithms in the program is the backtracking algorithm. This is used to fully solve an unsolved sudoku board in a bruteforce manner. The algorithm is very important for the generation of sudoku puzzles. The algorithm is in the form of a "recursive depth-first tree traversal". It will put down the first tile it can immediately, then again, again etc. until it cannot go any longer. It will then reverse one square and try the second tile, and so forth. This means it goes as deep as it can, then begins to add some breadth to its search. 

This algorithm has a lot of ways in which it can be optimised. For example, making the algorithm fill in the tiles with the least candidates first will overall reduce the processing time, as it means less branches at the beginning (which are more significant than having less branches near the end of the tree). The use of analytical solving for very simplistic cases, such as there only being one candidate in any given tile, is also much faster than backtracking. This can be ran first to significantly speed up the backtracking process (as it reduces the number of tiles, which is the exponential factor in the time it takes to solve a board)

## Generation Algorithm

Another algorithm employed is one used to generate the sudoku puzzles themselves. These are generated with varying difficulty. This process begins by generating a completed sudoku puzzle, which is done by placing down random valid tiles in random spots on a board, and then using the backtracker to check if they can be solved. When the backtracker solves them, the function GenerateTemplate() returns this full board. The function GetBoard() then takes this full board, and completes the following process: remove a random tile from the board, check that it is still *analytically* solvable. If it isn't, then replace the tile. This process is repeated more times for higher difficulty settings.

The RestrictedRandomNext function is strange. This is an algorithm that works similarly to Random.Next, but only takes an upper limit instead of both lower and upper limits. However, it takes a set of numbers which are the only numbers it can choose from: for example, an upper limit of 10 but having a restriction set of {4,6,7,11} will lead it to produce a random number from the set {4,6,7}. The SetIsRestrictive bool allows the function to instead treat the RestrictionSet as the ones that *aren't* allowed. In this case, the previous example would generate the numbers {0,1,2,3,5,8,9}. The offset simply allows the function to be offset by a certain amount, increasing both 0 and the ExclusiveMax.

## Analytical Algorithms

A core part of the process of providing hints to the player is to provide anaytical solving capabilities. Analytical solving means the computer solves the puzzle roughly like a human would, instead of very computer oriented like backtracking. There are two main methods to solve tiles analytically; working out the candidates of each tile is a prequisite for essentially all analytical methods, so this is something that is done expressly. One method involves simply searching to see if any tile only has one candidate, and if so, selecting this one. This is often sufficient for easy and normal puzzles by itself. Another method is to check each segment of a puzzle, and if a candidate only appears once then that candidate must be a real value. This is described previously in more detail in the analysis.

The analytical algorithms allow the program to return hints to the player. This is because it can find a single move, and then identify which of the two main methods it used to do so, along with where the tile was etc. This means that the function returns a Move data type, which includes not only the value of a tile, but also the reason for that value being at that position. This can be then be used to display a hint using a dictionary.

## Non-Logic Algorithms

GenerateGraphicalCandidates takes a bool which allows it to toggle between pushing the graphical representation (the textboxes that the player uses) into the internal representation, and the internal representation into the graphical representation. For most uses, the first one is used, as it's intended what the user enters is moved into a list that can be more easily analysed. However, when solving the board, the dataflow should be reversed.
If the dataflow is from the graphical to internal, then it first checks whether the input is candidates or a real value. It does this by checking if the string length == 1, which will only be true if the tile has a value, not candidates. It then uses this to determine whether to push the data into the candidates or real value section of the internal representation. 
The function then goes through each tile and makes a string that is pushed to the textbox. This string is either the candidates available for a given tile, or the value itself. The function needs to change the font size for this to occur properly. 

BoardDragDrop allows a user to drag a *.txt file into the project. This *.txt file should contain a sudoku board with no spaces and using new lines to identify new rows in the board. BoardDragDrop handles first the receiving of the data input, then it checks that every input is not a letter or symbol and that the data as a whole is 81 long. It then pushes the new board into the dataflow.
