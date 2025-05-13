import json

import requests

from models.Piece import Piece

url = 'http://localhost:5276/api/Gameplay'


def get_movable_pieces(game_id:int):
    response = requests.get(url + '/movablePieces/' + str(game_id))
    pieces_data = json.loads(response.text)
    pieces = [Piece.from_json(piece_data) for piece_data in pieces_data]
    return pieces

def move_piece(game_id:int, piece_number: int):
    response = requests.put(url + '/movePiece/' + str(game_id), json=piece_number)
    piece_data = json.loads(response.text)
    piece = Piece.from_json(piece_data)
    return piece
