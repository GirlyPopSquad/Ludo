class LobbyPlayer:
    def __init__(self, id:int):
        self.id = id

    @classmethod
    def from_json(cls, json_data):
        # Deserialize the JSON data to a LobbyPlayer object
        return cls(id=json_data['id'])

    def to_dict(self):
        # Convert the LobbyPlayer object to a dictionary
        return {
            'id': self.id
        }

    def __repr__(self):
        return f"LobbyPlayer(id='{self.id}')"