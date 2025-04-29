from tkinter import *


class BoardGame:
    def __init__(self, root):
        self.root = root
        self.root.title("Board Game")
        self.make_canvas = Canvas(root, width=800, height=700)
        self.make_canvas.pack()

        self.board_set_up()


    def board_set_up(self):
        # Cover Box made
        self.make_canvas.create_rectangle(100, 15, 100 + (40 * 15), 15 + (40 * 15), width=6, fill="white")
    
        # Square box
        self.make_canvas.create_rectangle(100, 15, 100+240, 15+240, width=3, fill="red")  # left up large square
        self.make_canvas.create_rectangle(100, (15+240)+(40*3), 100+240, (15+240)+(40*3)+(40*6), width=3, fill="#04d9ff")  # left down large square
        self.make_canvas.create_rectangle(340+(40*3), 15, 340+(40*3)+(40*6), 15+240, width=3, fill="#00FF00")  # right up large square
        self.make_canvas.create_rectangle(340+(40*3), (15+240)+(40*3), 340+(40*3)+(40*6), (15+240)+(40*3)+(40*6), width=3, fill="yellow")  # right down large square

        # Left 3 box(In white region)
        self.make_canvas.create_rectangle(100, (15+240), 100+240, (15+240)+40, width=3)
        self.make_canvas.create_rectangle(100+40, (15 + 240)+40, 100 + 240, (15 + 240) + 40+40, width=3, fill="#F00000")
        self.make_canvas.create_rectangle(100, (15 + 240)+80, 100 + 240, (15 + 240) + 80+40, width=3)

        # right 3 box(In white region)
        self.make_canvas.create_rectangle(100+240, 15, 100 + 240+40, 15 + (40*6), width=3)
        self.make_canvas.create_rectangle(100+240+40, 15+40, 100+240+80, 15 + (40*6), width=3, fill="#00FF00")
        self.make_canvas.create_rectangle(100+240+80, 15, 100 + 240+80+40, 15 + (40*6), width=3)

        # up 3 box(In white region)
        self.make_canvas.create_rectangle(340+(40*3), 15+240, 340+(40*3)+(40*6), 15+240+40, width=3)
        self.make_canvas.create_rectangle(340+(40*3), 15+240+40, 340+(40*3)+(40*6)-40, 15+240+80, width=3, fill="yellow")
        self.make_canvas.create_rectangle(340+(40*3), 15+240+80, 340+(40*3)+(40*6), 15+240+120, width=3)

        # down 3 box(In white region)
        self.make_canvas.create_rectangle(100, (15 + 240)+(40*3), 100 + 240+40, (15 + 240)+(40*3)+(40*6), width=3)
        self.make_canvas.create_rectangle(100+240+40, (15 + 240)+(40*3), 100 + 240+40+40, (15 + 240)+(40*3)+(40*6)-40, width=3, fill="#04d9ff")
        self.make_canvas.create_rectangle(100 + 240+40+40, (15 + 240)+(40*3), 100 + 240+40+40+40, (15 + 240)+(40*3)+(40*6), width=3)

        # All left separation line
        start_x = 100 + 40
        start_y = 15 + 240
        end_x = 100 + 40
        end_y = 15 + 240 + (40 * 3)
        for _ in range(5):
            self.make_canvas.create_line(start_x, start_y, end_x, end_y, width=3)
            start_x += 40
            end_x += 40

        # All right separation line
        start_x = 100+240+(40*3)+40
        start_y = 15 + 240
        end_x = 100+240+(40*3)+40
        end_y = 15 + 240 + (40 * 3)
        for _ in range(5):
            self.make_canvas.create_line(start_x, start_y, end_x, end_y, width=3)
            start_x += 40
            end_x += 40

        # All up separation done
        start_x = 100+240
        start_y = 15+40
        end_x = 100+240+(40*3)
        end_y = 15+40
        for _ in range(5):
            self.make_canvas.create_line(start_x, start_y, end_x, end_y, width=3)
            start_y += 40
            end_y += 40

        # All down separation done
        start_x = 100 + 240
        start_y = 15 + (40*6)+(40*3)+40
        end_x = 100 + 240 + (40 * 3)
        end_y = 15 + (40*6)+(40*3)+40
        for _ in range(5):
            self.make_canvas.create_line(start_x, start_y, end_x, end_y, width=3)
            start_y += 40
            end_y += 40

        # Square box(Coins containers) white region make
        self.make_canvas.create_rectangle(100+20, 15+40-20, 100 + 40 + 60 + 40 +60+20, 15+40+40+40+100-20, width=3, fill="white")
        self.make_canvas.create_rectangle(340+(40*3)+40 - 20, 15 + 40-20, 340+(40*3)+40 + 60 + 40 + 40+20+20, 15+40+40+40+100-20, width=3, fill="white")
        self.make_canvas.create_rectangle(100+20, 340+80-20+15, 100 + 40 + 60 + 40 +60+20, 340+80+60+40+40+20+15, width=3, fill="white")
        self.make_canvas.create_rectangle(340+(40*3)+40 - 20, 340 + 80 - 20+15, 340+(40*3)+40 + 60 + 40 + 40+20+20, 340 + 80 + 60 + 40 + 40 + 20+15, width=3, fill="white")

        # Adding 4 smaller starting squares inside each large area (left and right squares for example)
        # Left Starting Square (Red Area)
        self.create_centered_starting_squares(100, 15 + 240 - 240, "red")  # Adjusted y-coordinate to move it higher
        # Right Starting Square (Green Area)
        self.create_centered_starting_squares(340 + (40 * 3), 15, "green")
        # Bottom Starting Square (Sky Blue Area)
        self.create_centered_starting_squares(100, 15 + 240 + (40 * 3), "skyblue")
        # Top Starting Square (Yellow Area)
        self.create_centered_starting_squares(340 + (40 * 3), 15 + (40 * 6) + 120, "yellow")  # Adjusted y-coordinate to move it lower

        # Adding grid lines to the canvas (to show grid positions)
        self.add_grid_overlay()
        
        # Red Pieces
        self.place_object_on_grid(2, 2, "red")  # Placing a red circle
        self.place_object_on_grid(3, 2, "red")  # Placing a red circle 
        self.place_object_on_grid(2, 3, "red")  # Placing a red circle 
        self.place_object_on_grid(3, 3, "red")  # Placing a red circle 
        
        
        # Green pieces 
        self.place_object_on_grid(11, 2, "green") # Placing a green circle 
        self.place_object_on_grid(12, 2, "green") # Placing a green circle 
        self.place_object_on_grid(11, 3, "green") # Placing a green circle 
        self.place_object_on_grid(12, 3, "green") # Placing a green circle 

        
        #Blue pieces
        self.place_object_on_grid(2, 11, "skyblue")  # Placing a skyblue circle
        self.place_object_on_grid(3, 11, "skyblue")  # Placing a skyblue circle
        self.place_object_on_grid(2, 12, "skyblue")  # Placing a skyblue circle
        self.place_object_on_grid(3, 12, "skyblue")  # Placing a skyblue circle
        
        #Yellow Pieces
        self.place_object_on_grid(11, 11, "yellow") # Placing a yellow circle
        self.place_object_on_grid(12, 11, "yellow") # Placing a yellow circle
        self.place_object_on_grid(11, 12, "yellow") # Placing a yellow circle
        self.place_object_on_grid(12, 12, "yellow") # Placing a yellow circle
        
        
        self.color_grid_square(1, 6, "red")
        self.color_grid_square(8, 1, "green")
        self.color_grid_square(6, 13, "skyblue")
        self.color_grid_square(13, 8, "yellow")

        
    def place_object_on_grid(self, grid_x, grid_y, color):
        """
        Places a circle on the grid at the given (grid_x, grid_y) coordinates.
        :param grid_x: x-coordinate of the grid position (0-based)
        :param grid_y: y-coordinate of the grid position (0-based)
        :param color: color of the circle
        """
        grid_size = 40  # size of each square in the grid
        # Calculate the canvas position based on grid size
        canvas_x = 100 + grid_x * grid_size
        canvas_y = 15 + grid_y * grid_size
        
        return self.make_canvas.create_oval(
            canvas_x + 5, canvas_y + 5,  # Adjusting for a margin (5px offset)
            canvas_x + 35, canvas_y + 35,  # Adjusting for a margin (5px offset)
            fill=color, outline="black"
        )
        
    def color_grid_square(self, grid_x, grid_y, color):
        """
        Colors a grid square with the specified color.
        :param grid_x: x-coordinate of the grid position (0-based)
        :param grid_y: y-coordinate of the grid position (0-based)
        :param color: color to fill the square
        """
        grid_size = 40  # size of each square in the grid
        # Calculate the canvas position based on grid size
        canvas_x = 100 + grid_x * grid_size
        canvas_y = 15 + grid_y * grid_size
        
        # Draw a filled rectangle to color the square at the specified position
        self.make_canvas.create_rectangle(
            canvas_x, canvas_y,  # top-left corner of the square
            canvas_x + grid_size, canvas_y + grid_size,  # bottom-right corner of the square
            fill=color, outline="black"
        )

    def create_centered_starting_squares(self, x_start, y_start, color):
        """
        Creates 4 small squares centered inside a large starting area.
        :param x_start: x-coordinate of the top-left corner of the large area
        :param y_start: y-coordinate of the top-left corner of the large area
        :param color: The color of the starting squares
        """
        square_size = 40  # size of each starting square
        spacing = 10  # space between the squares
        large_area_width = 240  # width of each large starting area
        large_area_height = 240  # height of each large starting area

        # Calculate the starting position for the top-left corner of the 4 squares
        # Centering the squares in the large area
        x_center = x_start + (large_area_width - (2 * square_size + spacing)) // 2
        y_center = y_start + (large_area_height - (2 * square_size + spacing)) // 2

        for i in range(2):  # loop to create 2 rows of 2 squares
            for j in range(2):  # loop to create 2 squares per row
                self.make_canvas.create_rectangle(
                    x_center + j * (square_size + spacing),
                    y_center + i * (square_size + spacing),
                    x_center + j * (square_size + spacing) + square_size,
                    y_center + i * (square_size + spacing) + square_size,
                    fill=color, width=3
                )

    def add_grid_overlay(self):
        """ Adds a grid overlay to the board to show the grid positions. """
        grid_size = 40  # size of each square in the grid
        for x in range(100, 800, grid_size):  # vertical lines
            self.make_canvas.create_line(x, 15, x, 600, dash=(2, 2))  # dash pattern for vertical lines
        for y in range(15, 600, grid_size):  # horizontal lines
            self.make_canvas.create_line(100, y, 800, y, dash=(2, 2))  # dash pattern for horizontal lines

def open_ludoboard_window():
    root = Tk()
    BoardGame(root)
    root.mainloop()

#open_ludoboard_window()
