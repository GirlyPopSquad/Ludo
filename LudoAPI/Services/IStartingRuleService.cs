using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IStartingRuleService
{
    int FindStartingPlayer(Lobby lobby);
}