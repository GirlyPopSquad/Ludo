from models.LobbyPlayer import LobbyPlayer


class Roll:
    def __init__(self, player_id, value):
        self.player_id = player_id  # Should be an instance of LobbyPlayer
        self.value = value

    @classmethod
    def from_json(cls, json_data):
        # Deserialize the JSON data to a Roll object
        player_id = json_data['playerId']
        value = json_data['value']
        return cls(player_id, value)

    def to_dict(self):
        # Convert the Roll object to a dictionary
        return {
            'playerId': self.player_id,
            'value': self.value
        }

    def __repr__(self):
        return f"Roll(player_id={self.player_id}, value={self.value})"
    
    @classmethod
    def generate_test_rolls(cls):
        """Generates a test list of Roll objects."""
        players = [LobbyPlayer(id=i) for i in range(1, 4)]  # Create sample players
        rolls = [cls(player, value) for player, value in zip(players, [6, 2, 6])]
        return rolls
