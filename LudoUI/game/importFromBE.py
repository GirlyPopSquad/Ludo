import json

import requests

from models.Board import Board

url = 'http://localhost:5276/api/Board'

def test_board():
    response = requests.get(url)
    board = Board.from_json(json.loads(response.text))
    return board

def get_board_from_game_id(game_id):
    response = requests.get(url + "/getByGameId/" + str(game_id))
    board = Board.from_json(json.loads(response.text))
    return board
