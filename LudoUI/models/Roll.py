from models.LobbyPlayer import LobbyPlayer


class Roll:
    def __init__(self, player, value):
        self.player = player  # Should be an instance of LobbyPlayer
        self.value = value

    @classmethod
    def from_json(cls, json_data):
        # Deserialize the JSON data to a Roll object
        player = LobbyPlayer.from_json(json_data['player'])
        value = json_data['value']
        return cls(player, value)

    def to_dict(self):
        # Convert the Roll object to a dictionary
        return {
            'player': self.player.to_dict(),  # Convert the player to a dictionary
            'value': self.value
        }