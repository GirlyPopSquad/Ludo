import json

import requests

from Lobby import Lobby

url = 'http://localhost:5276/api/Starting'


def nextStartingRoll(lobby):
    headers = {'Content-Type': 'application/json'}
    nextStartingRollData = requests.post(url + "/startingroll", json.dumps(lobby.__dict__), headers=headers)
    updatedLobby = Lobby(**json.loads(nextStartingRollData.text))
    return updatedLobby
