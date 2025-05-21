import json

import requests

from models.Roll import Roll

url = 'http://localhost:5276/api/Roll'

def do_next_roll(game_id: int) -> Roll:
    response = requests.post(f"{url}/NextRoll/{game_id}")
    roll_data = json.loads(response.text)
    roll = Roll.from_json(roll_data)

    return roll

