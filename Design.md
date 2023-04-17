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

One of the more complicated algorithms in the program is the backtracking algorithm. This is used to fully solve an unsolved sudoku board in a bruteforce manner. This algorithm is very important for the generation of sudoku puzzles. The algorithm is in the form of a "recursive depth-first tree traversal". It will put down the first tile it can immediately, then again, again etc. until it cannot go any longer. It will then reverse one square and try the second tile, and so forth. This means it goes as deep as it can, then begins to add some breadth to its search. 

This algorithm has a lot of ways in which it can be optimised. For example, making the algorithm fill in the tiles with the least candidates first will overall reduce the processing time, as it means less branches at the beginning (which are more significant than having less branches near the end of the tree). The use of analytical solving for very simplistic cases, such as there only being one candidate in any given tile, is also much faster than backtracking. This can be ran first to significantly speed up the backtracking process (as it reduces the number of tiles, which is the exponential factor in the time it takes to solve a board)

## Generation Algorithm

Another algorithm employed is one used to generate the sudoku puzzles themselves. These are generated with varying difficulty. This process begins by generating a completed sudoku puzzle, which is done by placing down random valid tiles in random spots on a board, and then using the backtracker to check if they can be solved. When the backtracker solves them, the function GenerateTemplate() returns this full board. The function GetBoard() then takes this full board, and completes the following process: remove a random tile from the board, check that it is still *analytically* solvable. If it isn't, then replace the tile. This process is repeated more times for higher difficulty settings.

The RestrictedRandomNext function is strange. This is an algorithm that works similarly to Random.Next, but only takes an upper limit instead of both lower and upper limits. However, it takes a set of numbers which are the only numbers it can choose from: for example, an upper limit of 10 but having a restriction set of {4,6,7,11} will lead it to produce a random number from the set {4,6,7}. The SetIsRestrictive bool allows the function to instead treat the RestrictionSet as the ones that *aren't* allowed. In this case, the previous example would generate the numbers {0,1,2,3,5,8,9}. The offset simply allows the function to be offset by a certain amount, increasing both 0 and the ExclusiveMax.

