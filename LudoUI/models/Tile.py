from typing import Optional

from PlayerColor import PlayerColor
from models.Coordinate import Coordinate


class Tile:
    def __init__(self, coordinate: Coordinate, color: Optional[PlayerColor] = None):
        self.coordinate = coordinate
        self.color = color

    def __repr__(self):
        return f"Tile(coordinate={self.coordinate}, color={self.color})"

    @classmethod
    def from_json(cls, data):
        coordinate = Coordinate.from_json(data["coordinate"])
        return cls(coordinate=coordinate, color=data.get("color"))