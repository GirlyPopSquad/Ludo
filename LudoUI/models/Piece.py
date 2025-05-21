import PlayerColor
from PlayerColor import get_player_color_from_int
from models.Coordinate import Coordinate


class Piece:
    def __init__(self, piece_number: int, color: PlayerColor, coordinate: 'Coordinate'):
        self.piece_number = piece_number
        self.color = color
        self.coordinate = coordinate

    def __repr__(self):
        return f"Piece(piece_number={self.piece_number}, color={self.color}, coordinate={self.coordinate})"


    @classmethod
    def from_json(cls, data: dict):
        coordinate = Coordinate.from_json(data["coordinate"])
        color = get_player_color_from_int(data["color"])
        return cls(piece_number=data["pieceNumber"], color=color, coordinate=coordinate)
