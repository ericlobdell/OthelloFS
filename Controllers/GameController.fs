namespace OthelloFS.Controllers

open System.Net
open System.Net.Http
open System.Web.Http
open OthelloFS.Models

type MoveRequest = { X:int; Y:int; Player:int; Gameboard: GameBoard }

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
        mgr.recordMove req.X req.Y req.Player req.Gameboard
