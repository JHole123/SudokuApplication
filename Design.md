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