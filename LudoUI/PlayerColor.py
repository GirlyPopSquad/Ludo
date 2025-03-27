from enum import Enum

from Constants import BLUE, YELLOW, RED, GREEN

class PlayerColor(Enum):
    BLUE = 1
    GREEN = 2
    RED = 3
    YELLOW = 4

colorcode = {}
colorcode[PlayerColor.BLUE] = BLUE
colorcode[PlayerColor.GREEN] = GREEN
colorcode[PlayerColor.RED] = RED
colorcode[PlayerColor.YELLOW] = YELLOW

def get_piece_colorcode(piece_number):
    return colorcode[PlayerColor(piece_number)]