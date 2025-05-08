from enum import Enum

from Constants import BLUE, YELLOW, RED, GREEN

class PlayerColor(Enum):
    RED = 1
    GREEN = 2
    YELLOW = 3
    BLUE = 4

colorcode = {
    PlayerColor.BLUE: BLUE,
    PlayerColor.GREEN: GREEN,
    PlayerColor.RED: RED,
    PlayerColor.YELLOW: YELLOW
}

tkinter_colorcode = {
    #todo: these are from copilot, should be checked
    PlayerColor.BLUE: "#50A2FF",
    PlayerColor.GREEN: "#00C800",
    PlayerColor.RED: "#C80000",
    PlayerColor.YELLOW: "#FFFF00"
}

def get_player_color_from_int(i):
    return PlayerColor(i)

def get_piece_colorcode(piece_number):
    return colorcode[PlayerColor(piece_number)]

def get_tkinter_colorcode(piece_number):
    return tkinter_colorcode[PlayerColor(piece_number)]

