using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IRuleService
{
    bool DoesRollAllowLeavingHome(Roll roll);
    bool PlayerIsAllowedAnotherRoll(int gameId);
    bool CanPiecePassTroughCoordinate(int gameId, Piece piece, Coordinate nextCoordinate);
    bool WillThisPieceBeKickedHome(int gameId, Coordinate potentialCoordinate);
    Piece[] GetPiecesThatWillBeKickedHome(int gameId, MovablePiece chosenPiece);
}