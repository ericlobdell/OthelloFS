namespace OthelloFS.Tests

open OthelloFS.Models
open Xunit
open FsUnit.Xunit

type BoardManagerTests() = 
   
   [<Fact>]
   let ``Testing if setup is correct ``() =
        true |> should equal true

   [<Fact>]
   let ``getNewBoard should return a game board initialized with first moves``() =
        let mgr = new GameboardManager()
        let test = mgr.getNewBoard()

        Array2D.length1 test.Cells |> should equal 8
        Array2D.length2 test.Cells |> should equal 8

        // player 1 cells
        let cell33 = Array2D.get test.Cells 3 3
        let cell44 = Array2D.get test.Cells 4 4

        cell33.Player |> should equal 1
        cell44.Player |> should equal 1

        // player 2 cells
        let cell34 = Array2D.get test.Cells 3 4
        let cell43 = Array2D.get test.Cells 4 3

        cell34.Player |> should equal 2
        cell43.Player |> should equal 2

   [<Fact>]
   let ``record move should return a new gameboard with the move applied``() =
        let mgr = new GameboardManager()

        let gb = mgr.getNewBoard()
        let cellBeforeMove = Array2D.get gb.Cells 1 1
        cellBeforeMove.Player |> should equal 0

        let updatedBoard = mgr.recordMove 1 1 2 gb
        let cellWithMove = Array2D.get updatedBoard.Cells 1 1
        cellWithMove.Player |> should equal 2

   [<Fact>]
   let ``getScores should return a tuple with players scores``() =
        let mgr = new GameboardManager()
        let gb = mgr.getNewBoard()
        let scores = mgr.getScores gb

        scores |> should equal (2,2)

        let updatedBoard = mgr.recordMove 1 1 2 gb
        let scoresAfterMove = mgr.getScores gb

        scoresAfterMove |> should equal (2,3)

   [<Fact>]
   let ``tryGetCell should return None for cell off gameboard``() =
        let mgr = new GameboardManager()
        let gb = mgr.getNewBoard()
        let result = mgr.tryGetCell 2 9 gb

        result.IsNone |> should be True

   [<Fact>]
   let ``tryGetCell should return the cell at location for cell on gameboard``() =
        let mgr = new GameboardManager()
        let gb = mgr.getNewBoard()
        let result = mgr.tryGetCell 2 3 gb
        let actualCell = Array2D.get gb.Cells 2 3 

        result.IsSome |> should be True 
        result.Value |> should equal actualCell




