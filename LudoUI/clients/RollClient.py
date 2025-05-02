import requests

url = 'http://localhost:5276/api/Roll'

def Roll() -> int:
    response = requests.get(url)
    response.raise_for_status()
    return int(response.text)