# Evaluation

## Overview

The project fulfills all of its main requirements, however after programming the project I have a few thoughts on the overall project:

- Use of winforms. Windows Forms seemed like a relatively easy framework to build a UI off, and this was for the most part correct. However, producing a functional sudoku board which included not just numbers that both the computer and user could fill but also the candidates for each tile was a significant struggle. There were many UI bugs that would not have occurred if the framework used was more applicable to creating a program such as this. As a result of this, the code often seems convuluted when it exists to patch relatively obscure errors that were significant to the end user. In addition, winforms uses automated code to produce its UI, which was not very useful when you want a great deal more control over the elements themselves. This means that a lot of the code I produced myself where a machine usually would have; there is a lot of seemingly boilerplate code which might have been avoided on a different framework.
- The analysis in places was more detailed than necessary:
  - The analysis of how to optimise the backtracker turned out to be completely unnecessary. Even without optimisation, it ran far faster than the goal of 500μs - which is much faster than any human can perceive anyway. This means it will remain very interactive without any kind of freeze for the end user.
  - A lot of the details on analytical methods for solving were also unnecessary - in testing my analytic solving against sudoku.com hard puzzles, simply sole candidate and cross hatch scanning was sufficient for nearly all of them. Therefore, I did not develop the analytical solving further than this.
  - The analysis on generating puzzle templates was also unnecessary. In my research, it seemed to be that generating puzzles takes a very long time - needing to run backtracking every time to generate a full puzzle. This turned out to be not true, and the generator could generate a puzzle without any sort of perceivable delay to the end user at any difficulty.
  
## Specific Objective Evaluation

### General Requirements

- **The bruteforce solution must provide a completely solved puzzle, given the puzzle is solvable.** This was a required objective, as it would be essential in generating puzzles for and be a useful tool for the end user. This objective has been entirely fulfilled; the function for doing so can be found in the BoardBacktracker.cs file.

- **The analytical solution muts be able to return a single-tile, validated move.** This was a required objective, as this is essential to providing hints to the user. While the usage of solving just one tile is not available to the end user, it is used to generate puzzles (and ensure that puzzles that are displayed are analytically solvable). This requirement was fulfilled entirely, and it can be found in the BoardAnalyser.cs file.

- **The single-tile move analytical solution must provide a reason.**. This objective was not required, but it was fulfilled. The hint data structure is what is returned by the board analyser, and this data structure contains a hint enumeration. This means that the full data structure is returned by the engine, and whoever makes the application layer can write their own custom hint for any given reason. I have written my own, but theoretically anybody could put their front end on top and rewrite them. 

### Analytical Solving Requirements


