import json

import requests

from models.Lobby import Lobby

url = 'http://localhost:5276/api/Lobby'


def create_lobby():
    create_lobby_data = requests.post(url)
    created_lobby = Lobby.from_json(json.loads(create_lobby_data.text))
    return created_lobby


def get_lobby(id):
    request = requests.get(url + "/" + str(id))
    lobby = Lobby.from_json(json.loads(request.text))
    return lobby
