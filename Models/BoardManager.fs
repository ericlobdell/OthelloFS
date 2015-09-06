namespace OthelloFS.Models

[<CLIMutable>]
type GameBoard =
    {   Cells : Cell [,]
        Moves : int }

type GameboardManager() =
    let getCellForPosition x y p isNewGame = 
        let initialPlayerForPosition x y = 
            match (x,y) with
            | (3,3) | (4,4) -> 1
            | (3,4) | (4,3) -> 2
            | _ -> 0

        { X = x; 
          Y = y; 
          Player = if isNewGame then initialPlayerForPosition x y else p; 
          IsTarget = false }

    member this.getNewBoard() =
        { Cells = Array2D.init 8 8 (fun i j -> getCellForPosition j i 0 true); Moves = 0 }

    member this.recordMove (x:int) (y:int) (player:int) (gameboard: GameBoard) =
        Array2D.set gameboard.Cells y x { X = x; Y = y; Player = player; IsTarget = false }
        gameboard