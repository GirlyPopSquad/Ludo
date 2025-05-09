from typing import Dict

from models.Tile import Tile


class Board:
    def __init__(self, id: int, game_id: int, rows:int, cols:int, tiles: Dict[str, Tile]):
        self.id = id
        self.game_id = game_id
        self.cols = cols
        self.rows = rows
        self.tiles = tiles

    def __repr__(self):
        return f"Board(id={self.id}, width={self.cols}, height={self.rows}, game_id={self.game_id}, tiles={self.tiles})"

    @classmethod
    def from_json(cls, data: Dict):
        tiles = {
            key: Tile.from_json(value)
            for key, value in data["tiles"].items()
        }
        return cls(id=data["id"], game_id=data["gameId"], rows=data["rows"], cols=data["cols"], tiles=tiles)