from models.LobbyPlayer import LobbyPlayer
from models.Roll import Roll


class Lobby:
    def __init__(self, lobby_id, players: list[LobbyPlayer], rolls: list[Roll]):
        self.lobby_id = lobby_id
        self.players: list[LobbyPlayer] = players
        self.rolls: list[Roll] = rolls

    @classmethod
    def from_json(cls, json_data):
        players = [LobbyPlayer.from_json(player) for player in json_data['players']]
        rolls = [Roll.from_json(roll) for roll in json_data['rolls']]
        return cls(lobby_id=json_data['id'], players=players, rolls=rolls)

    def to_dict(self):
        return {
            'id': self.lobby_id,
            'players': [player.to_dict() for player in self.players],
            'rolls': [roll.to_dict() for roll in self.rolls]
        }

    def __repr__(self):
        return f"Lobby(lobby_id={self.lobby_id}, players={self.players}, rolls={self.rolls})"

