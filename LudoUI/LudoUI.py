import sys

import pygame
import pygame.freetype

from Constants import WHITE, BLACK, RED, GREEN, BLUE
from clients.LobbyClient import create_lobby
from PlayerColor import get_piece_colorcode
from clients.StartingRollClient import next_starting_roll
from draw.dice import draw_dice
from draw.ludo_piece import draw_ludo_piece

# Initialize Pygame
pygame.init()

lobby = create_lobby()

# Set up display
WIDTH, HEIGHT = 600, 400
screen = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("Ludo - Start Menu")

font = pygame.font.Font(None, 50)


# Button class
class Button:
    def __init__(self, text, x, y, width, height, color, hover_color, action=None):
        self.text = text
        self.rect = pygame.Rect(x, y, width, height)
        self.color = color
        self.hover_color = hover_color
        self.action = action

    def draw(self, surface):
        mouse_pos = pygame.mouse.get_pos()
        color = self.hover_color if self.rect.collidepoint(mouse_pos) else self.color
        pygame.draw.rect(surface, color, self.rect)
        text_surf = font.render(self.text, True, WHITE)
        text_rect = text_surf.get_rect(center=self.rect.center)
        surface.blit(text_surf, text_rect)

    def check_click(self):
        if self.rect.collidepoint(pygame.mouse.get_pos()):
            if pygame.mouse.get_pressed()[0]:  # Left mouse click
                if self.action:
                    self.action()


# Functions for button actions
def start_game():
    starting_roll()

def on_starting_roll():
    global lobby
    updated_lobby = next_starting_roll(lobby)
    lobby = updated_lobby
    return updated_lobby.starting_rolls[-1].value

def starting_roll():
    current_player_rolling = len(lobby.starting_rolls) + 1
    running = True
    starting_roll_frame(running, current_player_rolling,"?", "Roll", on_starting_roll)

def starting_roll_frame(is_running, player_id, dice_value, button_text, button_action):
    new_screen = pygame.display.set_mode((WIDTH, HEIGHT))
    pygame.display.set_caption("Ludo - Starting Roll")
    new_screen.fill(WHITE)

    text = font.render("Starting Roll", True, BLACK)
    new_screen.blit(text, (WIDTH // 2 - text.get_width() // 2, HEIGHT // 5 - text.get_height() // 2))
    draw_ludo_piece(new_screen, WIDTH // 5, 180, player_id, font)
    draw_dice(new_screen, WIDTH // 5, 180, dice_value, font)

    player_color = get_piece_colorcode(player_id)

    def on_button_click():
        updated_dice_value = on_starting_roll()
        starting_roll_frame(is_running, player_id, updated_dice_value, button_text, button_action)

    new_button = Button(button_text, WIDTH // 2 - BUTTON_WIDTH // 2, HEIGHT - BUTTON_HEIGHT - PADDING * 3,
                        BUTTON_WIDTH,
                        BUTTON_HEIGHT, player_color, GREEN, on_button_click)

    while is_running:

        new_button.draw(new_screen)

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                is_running = False
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                new_button.check_click()

        pygame.display.update()


def quit_game():
    pygame.quit()
    sys.exit()


BUTTON_WIDTH, BUTTON_HEIGHT = 200, 60
PADDING = 20  # Space between buttons

# Create buttons
play_button = Button("Play", WIDTH // 2 - BUTTON_WIDTH // 2, HEIGHT - BUTTON_HEIGHT - PADDING * 2 - BUTTON_HEIGHT,
                     BUTTON_WIDTH, BUTTON_HEIGHT, BLUE, GREEN, start_game)
quit_button = Button("Quit", WIDTH // 2 - BUTTON_WIDTH // 2, HEIGHT - BUTTON_HEIGHT - PADDING, BUTTON_WIDTH,
                     BUTTON_HEIGHT, RED, (255, 100, 100), quit_game)


def start_menu():
    gameOn = True

    while gameOn:
        screen.fill(WHITE)

        # Display title
        title_text = font.render("Ludo Game", True, BLACK)
        screen.blit(title_text, (WIDTH // 2 - title_text.get_width() // 2, 50))

        piece_positions = [WIDTH // 5, WIDTH // 5 * 2, WIDTH // 5 * 3, WIDTH // 5 * 4]

        draw_ludo_piece(screen, piece_positions[0], 180, 1, font)
        draw_ludo_piece(screen, piece_positions[1], 180, 2, font)
        draw_ludo_piece(screen, piece_positions[2], 180, 3, font)
        draw_ludo_piece(screen, piece_positions[3], 180, 4, font)

        # Draw buttons
        play_button.draw(screen)
        quit_button.draw(screen)

        # Event handling
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                gameOn = False
                quit_game()
            elif event.type == pygame.MOUSEBUTTONDOWN:
                play_button.check_click()
                quit_button.check_click()

        pygame.display.update()


# Run the start menu
start_menu()
