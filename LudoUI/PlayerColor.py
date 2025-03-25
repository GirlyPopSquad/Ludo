from enum import Enum

from Constants import BLUE, YELLOW, RED, GREEN

class Playercolor(Enum):
    BLUE = 1
    GREEN = 2
    RED = 3
    YELLOW = 4

colorcode = {}
colorcode[Playercolor.BLUE] = BLUE
colorcode[Playercolor.GREEN] = GREEN
colorcode[Playercolor.RED] = RED
colorcode[Playercolor.YELLOW] = YELLOW

def get_piece_colorcode(piece_number):
    return colorcode[Playercolor(piece_number)]