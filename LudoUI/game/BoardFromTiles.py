from tkinter import *

import clients.GameClient as gameClient
from PlayerColor import get_tkinter_colorcode
from clients.BoardClient import get_board_from_game_id
from clients.GameplayClient import get_movable_pieces, move_piece
from clients.PieceClient import get_pieces_from_game
from clients.RollClient import do_next_roll
from models.ArrowTile import ArrowTile, ArrowDirection
from models.Tile import Tile
from stateManagers.GameStateManager import get_game_id


class BoardFromTiles:
    game_id = get_game_id()

    board = get_board_from_game_id(game_id)
    pieces = get_pieces_from_game(game_id)


    # Dictionary to store piece ID, when they are created with tkinter, so that they may be removed again
    pieces_dict = {}

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

    # Dice box coords
    dice_x0 = 20
    dice_y0 = 50
    dice_x1 = 80
    dice_y1 = 110

    def __init__(self, root):

        self.root = root
        self.root.title("Board Game")
        self.canvas = Canvas(root, width=self.canvas_width, height=self.canvas_height)
        self.canvas.pack()

        self.dice_dots = []  # store dot IDs

        self.board_set_up()

        self.add_pieces()

        self.add_clickable_dice()

    def board_set_up(self):
        self.canvas.create_rectangle(self.board_x0, self.board_y0, self.board_x1, self.board_y1, width=0,
                                     fill="white")

        for tile in self.board.tiles.values():

            if isinstance(tile, Tile):
                self.draw_from_tile(tile)
            if isinstance(tile, ArrowTile):
                self.draw_from_arrow_tile(tile)

    def add_pieces(self):
        for piece in self.pieces:
            coords = piece.coordinate
            color = piece.color.value
            piece_id = self.place_piece(coords, color)

            self.pieces_dict.update({piece.piece_number: piece_id})

    def place_piece(self, coords, color):
        # the amount the piece is offset from the tile
        piece_margin = 7

        x0 = self.board_x0 + (self.grid_size * coords.x) + piece_margin
        y0 = self.board_y0 + (self.grid_size * coords.y) + piece_margin
        x1 = x0 + self.grid_size - 2 * piece_margin
        y1 = y0 + self.grid_size - 2 * piece_margin

        return self.canvas.create_oval(
            x0, y0,
            x1, y1,
            fill=get_tkinter_colorcode(color), outline="black",
        )

    def redraw_pieces(self):
        # Remove all pieces from the canvas
        for piece_id in self.pieces_dict.values():
            self.canvas.delete(piece_id)

        # Clear the dictionary
        self.pieces_dict.clear()

        # Re-add all pieces to the canvas
        self.add_pieces()

    def draw_tile(self, coords, color):
        x0 = self.board_x0 + (self.grid_size * coords.x)
        y0 = self.board_y0 + (self.grid_size * coords.y)
        x1 = x0 + self.grid_size
        y1 = y0 + self.grid_size
        self.canvas.create_rectangle(x0, y0, x1, y1, width=1, fill=color, outline="black")

    def draw_from_tile(self, tile: Tile):
        coords = tile.coordinate

        color = "white"
        if tile.color is not None:
            color = get_tkinter_colorcode(tile.color)

        self.draw_tile(coords, color)

    def draw_from_arrow_tile(self, tile: ArrowTile):
        coords = tile.coordinate

        color = "white"
        self.draw_tile(coords, color)

        # Draw an arrow on the tile at (coords.x, coords.y)
        x0 = self.board_x0 + (self.grid_size * coords.x)
        y0 = self.board_y0 + (self.grid_size * coords.y)

        # Coordinates for the arrow (you can adjust these to make the arrow look better)
        arrow_width = self.grid_size * 0.5
        arrow_height = self.grid_size * 0.5

        arrow_points = []

        if tile.arrow_direction == ArrowDirection.Up:
            arrow_points = [
                (x0 + self.grid_size / 2 - arrow_width / 2, y0 + self.grid_size / 2),
                (x0 + self.grid_size / 2 + arrow_width / 2, y0 + self.grid_size / 2),
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 - arrow_height)
            ]
        elif tile.arrow_direction == ArrowDirection.Down:
            arrow_points = [
                (x0 + self.grid_size / 2 - arrow_width / 2, y0 + self.grid_size / 2),
                (x0 + self.grid_size / 2 + arrow_width / 2, y0 + self.grid_size / 2),
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 + arrow_height)
            ]
        elif tile.arrow_direction == ArrowDirection.Left:
            arrow_points = [
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 - arrow_height / 2),
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 + arrow_height / 2),
                (x0 + self.grid_size / 2 - arrow_width, y0 + self.grid_size / 2)
            ]
        elif tile.arrow_direction == ArrowDirection.Right:
            arrow_points = [
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 - arrow_height / 2),
                (x0 + self.grid_size / 2, y0 + self.grid_size / 2 + arrow_height / 2),
                (x0 + self.grid_size / 2 + arrow_width, y0 + self.grid_size / 2)
            ]

        color = get_tkinter_colorcode(tile.color)

        self.canvas.create_polygon(arrow_points, fill=color, outline="black", width=2)

    def add_clickable_dice(self):

        # Draw the dice rectangle
        self.canvas.create_rectangle(self.dice_x0, self.dice_y0, self.dice_x1, self.dice_y1, fill="white",
                                     outline="black", tags="dice_box")

        # Bind click to the box
        self.canvas.tag_bind("dice_box", "<Button-1>", self.roll_dice)
        
        starting_player = gameClient.get_current_playerid(self.game_id)

        self.draw_player_identifier(starting_player)

        # Initial roll
        self.draw_dice_eyes(1)

    def roll_dice(self, event=None):
        roll = do_next_roll(self.game_id)

        self.draw_dice_eyes(roll.value)

        movable_pieces = get_movable_pieces(self.game_id)

        if len(movable_pieces) > 0:
            self.highlight_movable_pieces(movable_pieces)

        player_id = gameClient.get_current_playerid(self.game_id)

        self.draw_player_identifier(player_id)

    def draw_player_identifier(self, player_id):
        self.canvas.delete("player_indicator")

        circle_radius = 10
        circle_center_x = (self.dice_x0 + self.dice_x1) / 2
        circle_center_y = self.dice_y1 + 20  # below the dice

        self.canvas.create_oval(
            circle_center_x - circle_radius, circle_center_y - circle_radius,
            circle_center_x + circle_radius, circle_center_y + circle_radius,
            fill=get_tkinter_colorcode(player_id), outline="black",
            tags="player_indicator"
        )

    def draw_dice_eyes(self, value):
        # Clear old dots
        for dot in self.dice_dots:
            self.canvas.delete(dot)
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
            'C': (cx, cy),
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
            dot = self.canvas.create_oval(x - radius, y - radius, x + radius, y + radius, fill="black")
            self.dice_dots.append(dot)
    
    def highlight_movable_pieces(self, movable_pieces):
        for piece in movable_pieces:
            self.highlight_piece(piece)
            
    def highlight_piece(self, piece):
        coords = piece.coordinate
        margin = 3  # Less margin so it looks like a ring around the piece

        x0 = self.board_x0 + (self.grid_size * coords.x) + margin
        y0 = self.board_y0 + (self.grid_size * coords.y) + margin
        x1 = x0 + self.grid_size - 2 * margin
        y1 = y0 + self.grid_size - 2 * margin

        highlight_id = self.canvas.create_oval(
            x0, y0, x1, y1,
            outline="black", width=4, tags=["highlight", "piece_selector:" + str(piece.piece_number)]
        )
        self.canvas.tag_bind(highlight_id, "<Button-1>", lambda event: self.choose_piece(piece.piece_number))

    def choose_piece(self, piece_number):
        updated_piece = move_piece(self.game_id, piece_number)
        self.pieces = get_pieces_from_game(self.game_id)

        self.redraw_pieces()
        self.clear_highlights()

    def clear_highlights(self):
        self.canvas.delete("highlight")



def open_ludoboard_window():
    root = Tk()
    BoardFromTiles(root)
    root.mainloop()
