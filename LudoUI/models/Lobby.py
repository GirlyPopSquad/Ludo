from LobbyPlayer import LobbyPlayer
from Roll import Roll

class Lobby:
    def __init__(self, lobby_id, players: [LobbyPlayer], starting_rolls: [Roll]):
        self.lobby_id = lobby_id
        self.players: [LobbyPlayer] = players
        self.starting_rolls: [Roll] = starting_rolls

    @classmethod
    def from_json(cls, json_data):
        players = [LobbyPlayer(**player) for player in json_data['players']]
        starting_rolls = [Roll.from_json(roll) for roll in json_data['startingRolls']]
        return cls(lobby_id=json_data['id'], players=players, starting_rolls=starting_rolls)

    def to_dict(self):
        return {
            'lobby_id': self.lobby_id,
            'players': [player.to_dict() for player in self.players],
            'starting_rolls': [roll.to_dict() for roll in self.starting_rolls]
        }