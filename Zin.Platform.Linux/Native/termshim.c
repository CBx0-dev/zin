#include <termios.h>
#include <unistd.h>

struct termios orig_termios;

// Order must be in sync with InputChar.EscapeCode
typedef enum {
    EC_ESCAPE = 256,

    EC_ARROW_UP,
    EC_ARROW_DOWN,
    EC_ARROW_LEFT,
    EC_ARROW_RIGHT,
   
    EC_PAGE_UP,
    EC_PAGE_DOWN,
    EC_DELETE,
    EC_END,
    EC_HOME,

    EC_ENTER,
    EC_BACKSPACE
} EscapeCode;

int term_exit_raw_mode(void)
{
    write(STDOUT_FILENO, "\x1b[?1l\x1b[?1049l", 13);
 
    if (tcsetattr(STDIN_FILENO, TCSAFLUSH, &orig_termios) == -1)
    {
        return 0;
    }

    return 1;
}

int term_enter_raw_mode(void)
{
    if (tcgetattr(STDIN_FILENO, &orig_termios) == -1)
    {
        return 0;
    }

    struct termios raw = orig_termios;
    raw.c_iflag &= ~(BRKINT | ICRNL | INPCK | ISTRIP | IXON);
    raw.c_oflag &= ~(OPOST);
    raw.c_cflag |= (CS8);
    raw.c_lflag &= ~(ECHO | ICANON | IEXTEN | ISIG);
    raw.c_cc[VMIN] = 0;
    raw.c_cc[VTIME] = 1;

    if (tcsetattr(STDIN_FILENO, TCSAFLUSH, &raw) == -1)
    {
        return 0;
    }

    write(STDOUT_FILENO, "\x1b[?1049h\x1b[?1h", 13);

    return 1;
}

int parse_vt100(unsigned short* c_out)
{
    char seq[3];

    if (read(STDIN_FILENO, &seq[0], 1) != 1)
    {
        *c_out = EC_ESCAPE;
        return 1;
    }

    if (read(STDIN_FILENO, &seq[1], 1) != 1)
    {
        *c_out = EC_ESCAPE;
        return 1;
    }

    if (seq[0] == '[')
    {
        if (seq[1] >= '0' && seq[1] <= '9')
        {
            if (read(STDIN_FILENO, &seq[2], 1) != 1)
            {
                *c_out = EC_ESCAPE;
                return 1;
            }

            if (seq[2] == '~')
            {
                switch (seq[1])
                {
                case '1':
                    *c_out = EC_HOME;
                    return 1;
                case '3':
                    *c_out = EC_DELETE;
                    return 1;
                case '4':
                    *c_out = EC_END;
                    return 1;
                case '5':
                    *c_out = EC_PAGE_UP;
                    return 1;
                case '6':
                    *c_out = EC_PAGE_DOWN;
                    return 1;
                case '7':
                    *c_out = EC_HOME;
                    return 1;
                case '8':
                    *c_out = EC_END;
                    return 1;
                default:
                    *c_out = EC_ESCAPE;
                    return 1;
                }
            }

            switch (seq[1])
            {
            case 'A':
                *c_out = EC_ARROW_UP;
                return 1;
            case 'B':
                *c_out = EC_ARROW_DOWN;
                return 1;
            case 'C':
                *c_out = EC_ARROW_RIGHT;
                return 1;
            case 'D':
                *c_out = EC_ARROW_LEFT;
                return 1;
            case 'H':
                *c_out = EC_HOME;
                return 1;
            case 'F':
                *c_out = EC_END;
                return 1;
            default:
                *c_out = EC_ESCAPE;
                return 1;
            }
        }

        *c_out = EC_ESCAPE;
        return 1;
    }

    if (seq[0] == 'O')
    {
        switch (seq[1])
        {
        case 'A':
            *c_out = EC_ARROW_UP;
            return 1;
        case 'B':
            *c_out = EC_ARROW_DOWN;
            return 1;
        case 'C':
            *c_out = EC_ARROW_RIGHT;
            return 1;
        case 'D':
            *c_out = EC_ARROW_LEFT;
            return 1;
        case 'H':
            *c_out = EC_HOME;
            return 1;
        case 'F':
            *c_out = EC_END;
            return 1;
        default:
            *c_out = EC_ESCAPE;
            return 1;
        }
    }

    *c_out = EC_ESCAPE;
    return 1;
}

int term_read(unsigned short* c_out)
{
    char c;
    if (read(STDIN_FILENO, &c, 1) != 1) {
        return 0;
    }

    switch (c)
    {
    case 127: // Backspace
        *c_out = EC_BACKSPACE;
        return 1;
    case '\b': // Enter
        *c_out = EC_ENTER;
        return 1;
    }

    *c_out = c;

    if (c == '\x1b') {
        return parse_vt100(c_out);
    }
    
    return 1;
}

void term_write(char* str, int count)
{
    write(STDOUT_FILENO, str, count);
}
