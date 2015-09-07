namespace OthelloFS.Models

[<CLIMutable>]
type Cell =
    {   X : int
        Y : int
        Player: int
        IsTarget: bool }

[<CLIMutable>]
type GameBoard = 
    { Cells : Cell [,]
      Moves : int }


type GameboardManager() = 
    
    let getCellForPosition x y = 
        let initialPlayerForPosition x y = 
            match (x, y) with
            | (3, 3) | (4, 4) -> 1
            | (3, 4) | (4, 3) -> 2
            | _ -> 0
        { X = x
          Y = y
          Player = initialPlayerForPosition x y
          IsTarget = false }
    let getCellAt x y gameBoard = 
         Array2D.get gameBoard.Cells x y
    
    member this.isValidMove x y =
        ( x >=0 && x < 8 ) && ( y >= 0 && y < 8 )

    member this.getNewBoard() = 
        { Cells = Array2D.init 8 8 (fun i j -> getCellForPosition j i)
          Moves = 0 }
    
    member this.recordMove (x : int) (y : int) (player : int) (gameboard : GameBoard) = 
        let move = 
            { X = x
              Y = y
              Player = player
              IsTarget = false }

        Array2D.set gameboard.Cells y x move
        { Cells = gameboard.Cells
          Moves = gameboard.Moves + 1 }
    
    member this.tryGetCell x y gameBoard = 
        if this.isValidMove x y 
        then Some(getCellAt x y gameBoard) 
        else None

    member this.getFlatGameboard gameBoard =
        gameBoard.Cells |> Seq.cast<Cell>

    member this.getEmptyCells gameBoard =
        this.getFlatGameboard gameBoard
        |> Seq.filter (fun c -> c.Player = 0)

