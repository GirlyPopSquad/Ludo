from enum import Enum

from PlayerColor import PlayerColor
from models.Coordinate import Coordinate
from models.Tile import Tile


class ArrowDirection(Enum):
    Up = 0
    Right = 1
    Down = 2
    Left = 3


class ArrowTile(Tile):

    def __init__(self, coordinate: Coordinate, color: PlayerColor, arrow_direction: ArrowDirection):
        super().__init__(coordinate, color)
        self.arrow_direction = arrow_direction
        print(type(arrow_direction))
        print(isinstance(arrow_direction, ArrowDirection))
        print(ArrowDirection.Up)

    def __repr__(self):
        return f"ArrowTile(coordinate={self.coordinate}, color={self.color}, arrow_direction={self.arrow_direction})"



