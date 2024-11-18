import requests

def get_game_by_id(game_id):
    url = f"http://localhost:8080/api/tivogames/{game_id}"
    
    response = requests.get(url)
    if response.status_code == 200:
        return True
    else:
        return False
    

def insert_game(game):
    url = "http://localhost:8080/api/tivogames"
    
    response = requests.post(url, json=game)
    if response.status_code == 200:
        print(f"Partida {game['GameId']} inserida com sucesso!")
        return True
    else:
        return False