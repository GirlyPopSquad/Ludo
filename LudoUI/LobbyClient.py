import json

import requests

from Lobby import Lobby

url = 'http://localhost:5276/api/Lobby'

def createLobby():
    createLobbyData = requests.post(url)
    createdLobby = Lobby(**json.loads(createLobbyData.text))
    return createdLobby
