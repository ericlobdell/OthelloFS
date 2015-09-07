namespace OthelloFS.Controllers

open System.Net
open System.Net.Http
open System.Web.Http
open OthelloFS.Models

type MoveRequest = { X:int; Y:int; Player:int; Gameboard: GameBoard }


type MoveResonse = { Player1Score:int; Player2Score:int; GameOver:bool; Winner:int; Gameboard:GameBoard  }

/// Retrieves values.
[<RoutePrefix("api")>]
type GameController() =
    inherit ApiController()

    /// Gets all values.
    [<Route("game/new")>]
    member this.Get() = 
        let mgr = new GameboardManager()
        mgr.getNewBoard()

    [<Route("game/move")>]
    [<HttpPost>]
    member this.RecordMove(req: MoveRequest) =
        let mgr = new GameboardManager()
        let sk = new ScoreKeeper(mgr)
        let updatedBoard = mgr.recordMove req.X req.Y req.Player req.Gameboard
        let (p1,p2) = sk.getScores updatedBoard
        { Player1Score = p1; 
          Player2Score = p2; 
          GameOver =false; 
          Winner = int WinnerType.None; 
          Gameboard = updatedBoard  }
