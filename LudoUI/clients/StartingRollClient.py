import json

import requests

from models.Lobby import Lobby
from models.LobbyPlayer import LobbyPlayer
from models.Roll import Roll
from stateManagers.LobbyStateManager import get_lobby_id, set_lobby

url = 'http://localhost:5276/api/Starting'


def next_starting_roll():
    lobby_id = get_lobby_id()
    response = requests.post(url + '/startingroll/' + str(lobby_id))

    if response.status_code != 200:
        raise ValueError(f"API error: {response.status_code}, Message: {response.text}")

    updated_lobby = Lobby.from_json(response.json())

    return updated_lobby


def get_rerollers(lobby_id:int) -> list[LobbyPlayer]:

    response = requests.get(url + "/GetRerollers/" + str(lobby_id))

    rerollers = [LobbyPlayer.from_json(player) for player in json.loads(response.text)]
    return rerollers


def get_should_reroll(rolls: list[Roll]) -> bool:
    headers = {'Content-Type': 'application/json'}

    rolls_data = [roll.to_dict() for roll in rolls]

    response = requests.post(url + "/GetShouldReroll", json=rolls_data, headers=headers)

    if response.status_code != 200:
        raise ValueError(f"API error: {response.status_code}, Message: {response.text}")

    return json.loads(response.text)


# returns the new value for the roll, that has been rerolled
def handle_reroll(player_id: int):
    lobby_id = get_lobby_id()
    headers = {'Content-Type': 'application/json'}
    response = requests.post(url + f"/HandleReroll/{lobby_id}", json=player_id, headers=headers)

    if response.status_code != 200:
        raise ValueError(f"API error: {response.status_code}, Message: {response.text}")

    updated_lobby = Lobby.from_json(response.json())
    set_lobby(updated_lobby)

    player_id = player_id
    rolls = updated_lobby.rolls

    for roll in rolls:
        if roll.player_id == player_id:
            return roll.value
    return None
