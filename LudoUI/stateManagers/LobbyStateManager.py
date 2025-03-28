from models.Lobby import Lobby

lobby: Lobby


def set_lobby(new_lobby: Lobby):
    global lobby
    lobby = new_lobby


def get_lobby():
    return lobby
