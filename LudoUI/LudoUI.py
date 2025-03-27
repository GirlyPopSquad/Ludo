import pygame
import pygame.freetype

from Constants import WHITE, BLACK, WIDTH, HEIGHT
from GameStateManager import set_game_state, get_game_state, quit_game, GameState
from LobbyStateManager import set_lobby
from StartMenu import start_menu
from StartingRoll import starting_roll
from clients.LobbyClient import create_lobby

# Initialize Pygame
pygame.init()

set_lobby(create_lobby())

# Set up display
screen = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("Ludo - Start Menu")

font = pygame.font.Font(None, 50)


def ludo():
    game_on = True

    set_game_state(GameState.START_MENU)

    while game_on:
        # Event handling
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                game_on = False
                quit_game()

        if get_game_state() == GameState.START_MENU:
            start_menu(screen, font)

        if get_game_state() == GameState.LOBBY:
            starting_roll(screen, font)

        if get_game_state() == GameState.NOT_IMPLEMENTED:
            screen.fill(WHITE)
            not_implemented_text = font.render("Not implemented yet", True, BLACK)
            screen.blit(not_implemented_text, (
                WIDTH // 2 - not_implemented_text.get_width() // 2,
                HEIGHT // 2 - not_implemented_text.get_height() // 2))

        pygame.display.update()


# Run the start menu
ludo()
