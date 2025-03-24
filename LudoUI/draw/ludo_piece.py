import pygame

from Constants import BLACK

def draw_ludo_piece(surface, x, y, color, piece_number, font):

    body_height = 50
    body_width = 50
    head_radius = 15
    head_offset = 10


    # 🔲 Draw black outline for the body (slightly bigger)
    pygame.draw.polygon(surface, BLACK, [(x - body_width // 2 - 2, y),
                                         (x + body_width // 2 + 2, y),
                                         (x, y - body_height - 2)])

    # 🔲 Draw black outline for the head (circle, slightly bigger)
    pygame.draw.circle(surface, BLACK, (x, y - body_height - head_radius + head_offset), head_radius + 2)

    # 🎨 Draw actual colored body (on top of the black outline)
    pygame.draw.polygon(surface, color, [(x - body_width // 2, y),
                                         (x + body_width // 2, y),
                                         (x, y - body_height)])

    # 🎨 Draw actual colored head (on top of the black outline)
    pygame.draw.circle(surface, color, (x, y - body_height - head_radius + head_offset), head_radius)

    piece_text = font.render(str(piece_number), True, BLACK)
    text_rect = piece_text.get_rect(center=(x, y + 30))
    surface.blit(piece_text, text_rect)
