import requests

url = 'http://localhost:5276/api/Roll'

def Roll() -> int:
    response = requests.get(url)
    response.raise_for_status()
    return int(response.text)

def Isita6(roll) -> bool:
    response = requests.post(f"{url}/IsItA6/{roll}")
    response.raise_for_status()
    return response.text