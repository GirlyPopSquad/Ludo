import sys
from enum import Enum

import pygame


class GameState(Enum):
    NOT_IMPLEMENTED = 0
    START_MENU = 1
    LOBBY = 2


game_state: GameState = GameState.START_MENU


def get_game_state():
    return game_state


def set_game_state(new_state: GameState):
    global game_state
    game_state = new_state


def quit_game():
    pygame.quit()
    sys.exit()
