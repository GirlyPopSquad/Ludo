using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IRuleService
{
    bool DoesRollAllowLeavingHome(Roll roll);
    bool PlayerIsAllowedAnotherRoll(int gameId);
    bool CanPiecePassTroughCoordinate(int gameId, Piece piece, Coordinate nextCoordinate);
}