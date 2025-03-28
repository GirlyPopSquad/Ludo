from models.LobbyPlayer import LobbyPlayer
from models.Roll import Roll


class Lobby:
    def __init__(self, id, players, rolls):
        self.id = id
        self.players = players
        self.rolls = rolls

    @classmethod
    def from_json(cls, json_data):
        players = [LobbyPlayer.from_json(player) for player in json_data['players']]
        rolls = [Roll.from_json(roll) for roll in json_data['rolls']]
        return cls(id=json_data['id'], players=players, rolls=rolls)

    def to_dict(self):
        return {
            'id': self.id,
            'players': [player.to_dict() for player in self.players],
            'rolls': [roll.to_dict() for roll in self.rolls]
        }
