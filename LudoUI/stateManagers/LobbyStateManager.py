from enum import Enum
from models.Lobby import Lobby

lobby: Lobby


def set_lobby(new_lobby: Lobby):
    global lobby
    lobby = new_lobby


def get_lobby_id():
    return lobby.lobby_id


class LobbyState(Enum):
    STARTING_ROLL = 0
    ROLLS_OVERVIEW = 1
    STARTING_REROLL = 2 
    
lobby_state: LobbyState = LobbyState.STARTING_ROLL

def get_lobby_state():
    return lobby_state

def set_lobby_state(new_state: LobbyState):
    global lobby_state
    lobby_state = new_state

