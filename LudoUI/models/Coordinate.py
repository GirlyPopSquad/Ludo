class Coordinate:
    def __init__(self, x: int, y: int):
        self.x = x
        self.y = y

    @classmethod
    def from_json(cls, json_data):
        return cls(json_data['x'], json_data['y'])

    def __repr__(self):
        return f"Coordinate(x={self.x}, y={self.y})"
