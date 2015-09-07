namespace OthelloFS.Tests

open OthelloFS.Models
open Xunit
open FsUnit.Xunit

type ScoreKeeperTests() =

   [<Fact>]
   let ``getScores should return a tuple with players scores``() =
        let mgr = new GameboardManager()
        let scoreKeeper = new ScoreKeeper(mgr) 
        
        let gb = mgr.getNewBoard()
        let scores = scoreKeeper.getScores gb

        scores |> should equal (2,2)

        let updatedBoard = mgr.recordMove 1 1 2 gb
        let scoresAfterMove = scoreKeeper.getScores gb

        scoresAfterMove |> should equal (2,3)

   [<Fact>]
   let ``checkCell should return point for valid cell that belongs to the other player``() =
        let mgr = new GameboardManager()
        let scoreKeeper = new ScoreKeeper(mgr) 
        let cell = { X = 1; Y = 1; Player = 1; IsTarget = false }
        let (_,_,isPoint) = scoreKeeper.checkCell cell 2

        isPoint |> should be True


