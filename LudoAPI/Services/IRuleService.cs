using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IRuleService
{
    bool DoesRollAllowLeavingHome(Roll roll);
    bool PlayerIsAllowedAnotherRoll(int gameId);
    bool CanPiecePassCoordinate(int gameId, Piece piece, Coordinate coordinate);
    bool WillThisPieceBeKickedHome(int gameId, Coordinate potentialCoordinate);
    Piece[] GetPiecesThatWillBeKickedHome(int gameId, MovablePiece chosenPiece);
    bool WillThisBePlayersLastRound(int gameId, MovablePiece chosenPiece);
}