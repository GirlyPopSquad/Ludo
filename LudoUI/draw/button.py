import pygame

from Constants import WHITE, WIDTH, HEIGHT, BLUE, GREEN, RED


class Button:
    def __init__(self, text, x, y, width, height, color, hover_color, action=None):
        self.text = text
        self.rect = pygame.Rect(x, y, width, height)
        self.color = color
        self.hover_color = hover_color
        self.action = action

    def draw(self, surface, font):
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


# default buttons
BUTTON_WIDTH, BUTTON_HEIGHT = 200, 60
PADDING = 20


def init_play_button(action):
    return Button("Play", WIDTH // 2 - BUTTON_WIDTH // 2, HEIGHT - BUTTON_HEIGHT - PADDING * 2 - BUTTON_HEIGHT,
                  BUTTON_WIDTH, BUTTON_HEIGHT, BLUE, GREEN, action)


def init_quit_button(action):
    return Button("Quit", WIDTH // 2 - BUTTON_WIDTH // 2, HEIGHT - BUTTON_HEIGHT - PADDING, BUTTON_WIDTH,
                  BUTTON_HEIGHT, RED, (255, 100, 100), action)


def init_standard_button(button_text, color, hover_color, action):
    return Button(button_text, WIDTH // 2 - BUTTON_WIDTH // 2, HEIGHT - BUTTON_HEIGHT - PADDING * 3,
                  BUTTON_WIDTH,
                  BUTTON_HEIGHT, color, hover_color, action)
