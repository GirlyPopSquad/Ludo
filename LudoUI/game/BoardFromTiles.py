from tkinter import *

from PlayerColor import get_tkinter_colorcode
from game.importFromBE import test_board
from models.Tile import Tile


class BoardFromTiles:

    board = test_board()

    grid_size = 40
    padding = 15

    grid_height = board.height
    grid_width = board.width

    board_x0 = padding
    board_y0 = padding
    board_x1 = padding + (grid_size * grid_height)
    board_y1 = padding + (grid_size * grid_width)

    def __init__(self, root):
        self.root = root
        self.root.title("Board Game")
        self.make_canvas = Canvas(root, width=800, height=700)
        self.make_canvas.pack()

        self.board_set_up()
        self.add_grid_overlay()

    def board_set_up(self):
        # Draw a frame for the board
        self.make_canvas.create_rectangle(self.board_x0, self.board_y0, self.board_x1, self.board_y1, width=2, fill="white")

        #self.make_canvas.create_rectangle(self.board_x0, self.board_y0, self.board_x0 + self.grid_size, self.board_y0 + self.grid_size, width=0, fill="red")
        #self.make_canvas.create_rectangle(self.board_x0 + self.grid_size, self.board_y0, self.board_x0 + self.grid_size * 2,self.board_y0 + self.grid_size, width=0, fill="blue")

        for(tile) in self.board.tiles.values():
            self.draw_from_tile(tile)


    def draw_from_tile(self, tile:Tile):

        coords = tile.coordinate

        color = "white"
        if tile.color is not None:
            print(coords)
            color = get_tkinter_colorcode(tile.color)

        self.make_canvas.create_rectangle(self.board_x0 + (self.grid_size * coords.x), self.board_y0 + (self.grid_size * coords.y),
                                          self.board_x0 + self.grid_size * (coords.x + 1),
                                          self.board_y0 + self.grid_size* (coords.y + 1), width=0, fill=color)

    def add_grid_overlay(self):
        dash_pattern = (2, 2)

        # vertical lines
        for x in range(self.padding, self.board_x1, self.grid_size):
            self.make_canvas.create_line(x, self.padding, x, self.board_x1, dash=dash_pattern)

        # horizontal lines
        for y in range(self.padding, self.board_y1, self.grid_size):
            self.make_canvas.create_line(self.padding, y, self.board_y1, y, dash=dash_pattern)

def open_ludoboard_window():
    root = Tk()
    BoardFromTiles(root)
    root.mainloop()

open_ludoboard_window()