import json

import requests

from models.Piece import Piece

url = 'http://localhost:5276/api/Piece'


def get_pieces_from_game(game_id: int):
    response = requests.get(f"{url}/getByGameId/{game_id}")
    pieces_data = json.loads(response.text)
    pieces = [Piece.from_json(piece_data) for piece_data in pieces_data]
    return pieces
