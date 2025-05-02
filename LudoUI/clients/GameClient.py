import requests

url = 'http://localhost:5276/api/Game'

def create_game(lobby_id: int) -> int:
    response = requests.post(f"{url}/create/{lobby_id}")
    response.raise_for_status()
    return int(response.text)

def next_turn(game_id: int) -> int:
    response = requests.post(f"{url}/nextturn/{game_id}")
    response.raise_for_status()
    return int(response.text)
