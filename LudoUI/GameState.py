import pygame
import sys

game_state = ""


def get_game_state():
    return game_state


def set_game_state(new_state):
    global game_state
    game_state = new_state


def quit_game():
    pygame.quit()
    sys.exit()
