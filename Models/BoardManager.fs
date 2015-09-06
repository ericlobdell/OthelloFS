namespace OthelloFS.Models

[<CLIMutable>]
type GameBoard =
    {   Cells : Cell [,]
        Moves : int }

type GameboardManager() =
    let getCellForPosition x y = 
        let initialPlayerForPosition x y = 
            match (x,y) with
            | (3,3) | (4,4) -> 1
            | (3,4) | (4,3) -> 2
            | _ -> 0

        { X = x; 
          Y = y; 
          Player = initialPlayerForPosition x y; 
          IsTarget = false }

    let getPointValue cell player = 
        if cell.Player = player then 1 else 0

    let getPlayerScores gameBoard playerNum = 
        gameBoard.Cells |> Array2D.map (fun cell -> getPointValue cell playerNum)

    member this.getNewBoard() =
        { Cells = Array2D.init 8 8 (fun i j -> getCellForPosition j i ); Moves = 0 }

    member this.recordMove (x:int) (y:int) (player:int) (gameboard: GameBoard) =
        let move = { X = x; Y = y; Player = player; IsTarget = false }
        Array2D.set gameboard.Cells y x move
        { Cells = gameboard.Cells; Moves = gameboard.Moves + 1 }

    member this.getScores (gameBoard:GameBoard) =
        let p1Score = getPlayerScores gameBoard 1 |> Seq.cast<int> |> Seq.sum
        let p2Score = getPlayerScores gameBoard 2 |> Seq.cast<int> |> Seq.sum
        (p1Score, p2Score)

        