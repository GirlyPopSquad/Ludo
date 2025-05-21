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
        tile_type = data.get("$type", "Tile")
        coordinate = Coordinate.from_json(data["coordinate"])
        color = data.get("color")

        # Dynamically resolve the class based on the $type field
        if tile_type == "ArrowTile":
            from models.ArrowTile import ArrowTile, ArrowDirection
            return ArrowTile(coordinate=coordinate, color=color, arrow_direction=ArrowDirection(data.get("arrowDirection")))
        return cls(coordinate=coordinate, color=color)
