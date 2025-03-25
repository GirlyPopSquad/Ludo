from LobbyPlayer import LobbyPlayer
from Roll import Roll

class Lobby:
    def __init__(self, id, players: [LobbyPlayer], startingRolls: [Roll]):
        self.id = id
        self.players = players
        self.startingRolls = startingRolls
