from models.LobbyPlayer import LobbyPlayer


class Roll:
    def __init__(self, lobby_player: LobbyPlayer, value: int):
        self.lobby_player: LobbyPlayer = lobby_player
        self.value: int = value

    @classmethod
    def from_json(cls, roll):
        lobby_player = LobbyPlayer(**roll['player'])
        value = roll['value']
        return cls(lobby_player, value)

    def to_dict(self):
        return {
            'lobby_player': self.lobby_player.to_dict(),
            'value': self.value
        }