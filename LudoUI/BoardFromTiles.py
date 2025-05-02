from tkinter import *
import random

from PlayerColor import get_tkinter_colorcode
from game.importFromBE import test_board
from models.Tile import Tile

class BoardFromTiles:

    board = test_board()

    grid_size = 40

    # Padding: more space on the left
    padding_left = 100
    padding_top = 15
    padding_right = 15
    padding_bottom = 15

    grid_height = board.rows
    grid_width = board.cols

    board_x0 = padding_left
    board_y0 = padding_top
    board_x1 = board_x0 + (grid_size * grid_width)
    board_y1 = board_y0 + (grid_size * grid_height)

    canvas_width = padding_left + (grid_size * grid_width) + padding_right
    canvas_height = padding_top + (grid_size * grid_height) + padding_bottom

    def __init__(self, root):
        self.root = root
        self.root.title("Board Game")
        self.make_canvas = Canvas(root, width=self.canvas_width, height=self.canvas_height)
        self.make_canvas.pack()

        self.dice_dots = []  # store dot IDs

        self.board_set_up()
        self.add_grid_overlay()
        self.add_clickable_dice()

    def board_set_up(self):
        self.make_canvas.create_rectangle(self.board_x0, self.board_y0, self.board_x1, self.board_y1, width=2, fill="white")

        for tile in self.board.tiles.values():
            self.draw_from_tile(tile)

    def draw_from_tile(self, tile: Tile):
        coords = tile.coordinate

        # Check if the tile is an arrow tile (you can define this with a condition)
        # For example, let's assume if the tile has a specific color or another attribute, it's an arrow tile
        color = "white"
        if tile.color is not None:
            color = get_tkinter_colorcode(tile.color)

        # Check if the tile should be an arrow, for example based on its coordinate or color
        # For now, we'll just make tiles at coordinates (2, 3) and (4, 4) as arrows as an example
        if (coords.x, coords.y) in [(0, 7), (7, 0), (14, 7), (7, 14)]:  
            self.draw_arrow_on_tile(coords)
        else:
            x0 = self.board_x0 + (self.grid_size * coords.x)
            y0 = self.board_y0 + (self.grid_size * coords.y)
            x1 = x0 + self.grid_size
            y1 = y0 + self.grid_size
            self.make_canvas.create_rectangle(x0, y0, x1, y1, width=0, fill=color)

    def draw_arrow_on_tile(self, coords):
        # Draw an arrow on the tile at (coords.x, coords.y)
        x0 = self.board_x0 + (self.grid_size * coords.x)
        y0 = self.board_y0 + (self.grid_size * coords.y)
        
        # Coordinates for the arrow (you can adjust these to make the arrow look better)
        arrow_width = self.grid_size * 0.4
        arrow_height = self.grid_size * 0.6
        
        direction = "up" 
        arrow_color = "blue"


        if (coords.x, coords.y) == (0, 7):
            direction = "right"  
            arrow_color = "red"

        elif (coords.x, coords.y) == (7, 0):
            direction = "down" 
            arrow_color = "green"
 
        elif (coords.x, coords.y) == (14, 7):
            direction = "left"
            arrow_color = "yellow"

        if direction == "up":
            arrow_points = [
                (x0 + self.grid_size / 2 - arrow_width / 2, y0 + self.grid_size / 2),
                (x0 + self.grid_size / 2 + arrow_width / 2, y0 + self.grid_size / 2),
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 - arrow_height)
            ]
        elif direction == "down":
            arrow_points = [
                (x0 + self.grid_size / 2 - arrow_width / 2, y0 + self.grid_size / 2),
                (x0 + self.grid_size / 2 + arrow_width / 2, y0 + self.grid_size / 2),
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 + arrow_height)
            ]
        elif direction == "left":
            arrow_points = [
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 - arrow_height / 2),
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 + arrow_height / 2),
                (x0 + self.grid_size / 2 - arrow_width, y0 + self.grid_size / 2)
            ]
        elif direction == "right":
            arrow_points = [
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 - arrow_height / 2),
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 + arrow_height / 2),
                (x0 + self.grid_size / 2 + arrow_width, y0 + self.grid_size / 2)
            ]
        
        self.make_canvas.create_polygon(arrow_points, fill=arrow_color, outline="black", width=2)
        
        
    def add_grid_overlay(self):
        dash_pattern = (2, 2)

        for x in range(self.board_x0, self.board_x1, self.grid_size):
            self.make_canvas.create_line(x, self.board_y0, x, self.board_y1, dash=dash_pattern)

        for y in range(self.board_y0, self.board_y1, self.grid_size):
            self.make_canvas.create_line(self.board_x0, y, self.board_x1, y, dash=dash_pattern)

    def add_clickable_dice(self):
        # Dice box coords
        self.dice_x0 = 20
        self.dice_y0 = 50
        self.dice_x1 = 80
        self.dice_y1 = 110

        # Draw the dice rectangle
        self.make_canvas.create_rectangle(self.dice_x0, self.dice_y0, self.dice_x1, self.dice_y1, fill="white", outline="black", tags="dice_box")

        # Bind click to the box
        self.make_canvas.tag_bind("dice_box", "<Button-1>", self.roll_dice)

        # Initial roll
        self.draw_dice_eyes(1)

    def roll_dice(self, event=None):
        roll = random.randint(1, 6)
        self.draw_dice_eyes(roll)
        return roll

    def draw_dice_eyes(self, value):
        # Clear old dots
        for dot in self.dice_dots:
            self.make_canvas.delete(dot)
        self.dice_dots.clear()

        # Dice face layout (3x3 positions)
        cx = (self.dice_x0 + self.dice_x1) // 2
        cy = (self.dice_y0 + self.dice_y1) // 2
        offset = 15

        positions = {
            'TL': (cx - offset, cy - offset),
            'TC': (cx, cy - offset),
            'TR': (cx + offset, cy - offset),
            'CL': (cx - offset, cy),
            'C':  (cx, cy),
            'CR': (cx + offset, cy),
            'BL': (cx - offset, cy + offset),
            'BC': (cx, cy + offset),
            'BR': (cx + offset, cy + offset),
        }

        # Dot positions for each dice value
        dice_faces = {
            1: ['C'],
            2: ['TL', 'BR'],
            3: ['TL', 'C', 'BR'],
            4: ['TL', 'TR', 'BL', 'BR'],
            5: ['TL', 'TR', 'C', 'BL', 'BR'],
            6: ['TL', 'CL', 'BL', 'TR', 'CR', 'BR']
        }

        radius = 4
        for pos in dice_faces[value]:
            x, y = positions[pos]
            dot = self.make_canvas.create_oval(x - radius, y - radius, x + radius, y + radius, fill="black")
            self.dice_dots.append(dot)

def open_ludoboard_window():
    root = Tk()
    BoardFromTiles(root)
    root.mainloop()

open_ludoboard_window()
