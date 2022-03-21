# C# Sudoku Application Analysis
## AQA NEA Computer Science Programming Project

**Summary** The sudoku application will provide systems for the analytical and bruteforce solution of sudoku puzzles; random generation of sudoku puzzles with varying difficulty; the use of analytical solving to provide hints to the player; a graphical interface for interacting with these systems, and a library for sudoku solving and generation. This document delineates specific requirements and recommendations for the criteria of this application. It does not include the specific design of the program itself.

# 1. Sudoku Library

The library will act as the "engine" of the program - it will provide functionality for solving and generating sudoku puzzles given a particular format, but will not provide immediately user-comprehensible output. The presentation of the sudoku program, and the way in which the program displays the information, is up to the developer using the library. The following subsections will describe the composite functionality of the library: solving and generation. 

## 1.1 Sudoku Solving

The solving of a sudoku is a relatively trivial problem, especially with bruteforce methods. However, a requirement of the solution is provide a hint: bruteforce solution is not conducive to providing a logical, human-intelligible hint to a stuck player. Therefore, the solution must use both analytical and bruteforce functionality, along with providing a reason for the move in the case of the analytical solving. Bruteforce solving is still required in cases where the sudoku is solvable but not human-solvable, and is sped up by using the functionality of the analytical solution as heuristics.

### 1.1.1 General Requirements

This list describes **general** requirements of sudoku solving. Requirements specific to analytical or bruteforce implementation are not listed here, rather just the requirements of their inputs and outputs.

- *The analytical solution must be able to return a single-tile, validated move.* The analytical solution, unlike the bruteforce one, can provide a linear solution to the problem. This must be utilised to provide a uniquely analytical function of only solving one more tile. Solving only one tile is crucial to the provision of hints to stuck human players.
- *The single-tile move analytical solution must provide a reason.* The solution needs to provide a reason for why it has picked that number for that tile. This reason will be used to supply hints to the player. The reason does not necessarily need to be human-understandable (i.e. it may use an ID system). The reason must provide the tiles which led it to the conclusion, for developer-specific implementation of graphical display for hints.
- *The bruteforce solution must provide a completely solved puzzle, given the puzzle is solvable.* The bruteforce solution can not provide a reasonable chain of linear moves, and as such it must provide a complete solution rather than a step-by-step. The bruteforce solution is preferred for cases of simply solving the solution without any regard for providing hints or being human-intelligible.

### 1.1.2 Analytical Solving Requirements

- *The analytical solution must be able to provide a technique for at least most human techniques.* This will allow hints to be useful for human players looking to develop their abilities in sudoku by showing them precisely where particular techniques can be used. This allows the hints to properly fulfil their purpose as an assisted learning tool for the player. Specifically, "most" is defined as 90% in terms of frequency.
- *The analytical solution must carry out full workings in a timely manner.* Sudoku solvers can be very fast, but we must account for the fact that our system will additionally be returning hints and using extensive human-like technique. To account for this, a reasonable time requirement is 500ms. This is excessive for a program simply concerned with the solution of the sudoku, but not for one with additional features such as hints and GUI.

### 1.1.3 Bruteforce Solving Requirements

- *The bruteforce solution must use heuristics for optimisation.* This includes techniques such as using small analytical methods for pruning, and ordering of branches in a computationally reductive manner - for example, trying squares that have the least possibilities first will reduce the overall number of computations the bruteforce solver must complete. 
- *The bruteforce solution must use memory stores for optimisation.* Usage of caches and various memory stores of things already computed is very effective in bruteforce solutions to reduce the number of computations that need to be done by simply not repeating any. This method is generally slightly more memory intensive, however this is not an issue on any modern computer where the additional memory usage is in a few megabytes (a trivial amount), and instead saves on the bottlenecking factor in sudoku solving, which is processing.

## 1.2 Sudoku Generation

The generation of a new sudoku puzzle for a user to solve is significantly more computationally expensive than sudoku solving. Unlike solving, there is no singular algorithm which can be used for simple generation, and it rather relies on pure bruteforce using solving to validate - or far more likely, to invalidate - each attempt. However, there is ways in which to "squeeze blood out of a stone" - that is to say, create as many puzzles out of as little computation as possible. Good ways to optimise this were described the top answer in [*this StackOverflow post*](https://gamedev.stackexchange.com/questions/56149/how-can-i-generate-sudoku-puzzles) by badweasel. More detail on how this will performed in my program will be given in the design documentation.
