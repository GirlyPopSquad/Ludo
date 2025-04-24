from typing import Dict

from models.Tile import Tile


class Board:
    def __init__(self, id: int, game_id: int,height:int, width:int, tiles: Dict[str, Tile]):
        self.id = id
        self.game_id = game_id
        self.width = width
        self.height = height
        self.tiles = tiles

    def __repr__(self):
        return f"Board(id={self.id}, width={self.width}, height={self.height}, game_id={self.game_id}, tiles={self.tiles})"

    @classmethod
    def from_json(cls, data: Dict):
        tiles = {
            key: Tile.from_json(value)
            for key, value in data["tiles"].items()
        }
        return cls(id=data["id"], game_id=data["gameId"], height=data["height"], width=data["width"], tiles=tiles)