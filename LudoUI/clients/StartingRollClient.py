import json

import requests

from LobbyStateManager import get_lobby
from models.Lobby import Lobby
from models.LobbyPlayer import LobbyPlayer
from models.Roll import Roll

url = 'http://localhost:5276/api/Starting'


def next_starting_roll():
    lobby = get_lobby()
    headers = {'Content-Type': 'application/json'}
    lobby_dict = lobby.to_dict()
    response = requests.post(url + '/startingroll', json=lobby_dict, headers=headers)
    updated_lobby = Lobby.from_json(response.json())
    return updated_lobby


def get_rerollers(rolls: list[Roll]) -> list[LobbyPlayer]:
    headers = {'Content-Type': 'application/json'}

    # Convert list of Roll objects to a list of dictionaries
    rolls_data = [roll.to_dict() for roll in rolls]

    # Send the list in the HTTP request
    response = requests.post(url + "/GetRerollers", json=rolls_data, headers=headers)

    # Handle API errors
    if response.status_code != 200:
        raise ValueError(f"API error: {response.status_code}, Message: {response.text}")

    # Convert response JSON into a list of LobbyPlayer objects
    rerollers = [LobbyPlayer(**player) for player in json.loads(response.text)]

    return rerollers


def get_should_reroll(rolls: list[Roll]) -> bool:
    headers = {'Content-Type': 'application/json'}

    # Convert list of Roll objects to JSON
    rolls_data = [roll.to_dict() for roll in rolls]

    # Send the request to the API
    response = requests.post(url + "/GetShouldReroll", json=rolls_data, headers=headers)

    # Handle API errors
    if response.status_code != 200:
        raise ValueError(f"API error: {response.status_code}, Message: {response.text}")

    # Return the boolean result from the API response
    return json.loads(response.text)
