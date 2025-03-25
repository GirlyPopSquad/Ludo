import json
import requests
from models.Lobby import Lobby

url = 'http://localhost:5276/api/Starting'

def next_starting_roll(lobby):
    headers = {'Content-Type': 'application/json'}
    next_starting_roll_data = requests.post(url + "/startingroll", json=lobby.to_dict(), headers=headers)
    updated_lobby = Lobby.from_json(json.loads(next_starting_roll_data.text))
    return updated_lobby