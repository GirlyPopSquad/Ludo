using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IRollService
{
    Roll DoNextRoll(int gameId);
}