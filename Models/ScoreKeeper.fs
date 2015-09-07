namespace OthelloFS.Models

type WinnerType =
    | None = 0
    | PlayerOne = 1
    | PlayerTwo = 2

type ScoreKeeper( boardManager: GameboardManager ) = 
    let _mgr = boardManager

    let getPointValue cell player = 
        if cell.Player = player then 1 else 0

    let getPlayerScores gameBoard playerNum = 
        gameBoard.Cells |> Array2D.map (fun cell -> getPointValue cell playerNum)

    member this.getScores (gameBoard : GameBoard) = 
        let playerOneScore = 
            getPlayerScores gameBoard 1
            |> Seq.cast<int>
            |> Seq.sum
        
        let playerTwoScore = 
            getPlayerScores gameBoard 2
            |> Seq.cast<int>
            |> Seq.sum
        
        (playerOneScore, playerTwoScore)

    member this.checkCell (cell: Cell) (player:int) =
        let isValid = _mgr.isValidMove cell.X cell.Y
        let isEmptyCell = cell.Player = 0
        let isPoint = isValid && not isEmptyCell && cell.Player <> player
        (isValid, isEmptyCell, isPoint)

    member this.calculatePoints (startingCell:Cell) (rowInc:int) (colInc:int) (player:int) (gameBoard:GameBoard) =
        let mutable score = 0
        let rec getScore (x:int) (y:int) =
            if _mgr.tryGetCell x y gameBoard = None then 0 
            else
                let cell = Array2D.get gameBoard.Cells y x
                let (isValid, isEmpty, isPoint) = this.checkCell cell player

                if not isValid || isEmpty then 0
                else if isPoint then
                    score <- score + 1
                    getScore (y + rowInc) (x + colInc)
                else score

        getScore startingCell.Y startingCell.X


                

            