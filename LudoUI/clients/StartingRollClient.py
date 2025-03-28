import json

import requests

from stateManagers.LobbyStateManager import get_lobby
from models.Lobby import Lobby
from models.LobbyPlayer import LobbyPlayer
from models.Roll import Roll

url = 'http://localhost:5276/api/Starting'


def next_starting_roll():
    lobby = get_lobby()
    headers = {'Content-Type': 'application/json'}
    lobby_dict = lobby.to_dict()
    response = requests.post(url + '/startingroll', json=lobby_dict, headers=headers)

    if response.status_code != 200:
        raise ValueError(f"API error: {response.status_code}, Message: {response.text}")

    updated_lobby = Lobby.from_json(response.json())

    return updated_lobby


def get_rerollers(rolls: list[Roll]) -> list[LobbyPlayer]:
    headers = {'Content-Type': 'application/json'}
    rolls_data = [roll.to_dict() for roll in rolls]
    response = requests.post(url + "/GetRerollers", json=rolls_data, headers=headers)

    if response.status_code != 200:
        raise ValueError(f"API error: {response.status_code}, Message: {response.text}")
    
    rerollers = [LobbyPlayer(**player) for player in json.loads(response.text)]

    return rerollers


def get_should_reroll(rolls: list[Roll]) -> bool:
    headers = {'Content-Type': 'application/json'}

    rolls_data = [roll.to_dict() for roll in rolls]

    response = requests.post(url + "/GetShouldReroll", json=rolls_data, headers=headers)

    if response.status_code != 200:
        raise ValueError(f"API error: {response.status_code}, Message: {response.text}")
    
    return json.loads(response.text)
