var m = "|\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"|\n|    **          **          **          **          **          **            |\n|  *0**0*      *0**0*      *0**0*      *0**0*      *0**0*      *0**0*          |\n|  ******      ******      ******      ******      ******      ******          |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|   ****        ****        ****        ****        ****        ****           |\n| **0**0**    **0**0**    **0**0**    **0**0**    **0**0**    **0**0**         |\n| ********    ********    ********    ********    ********    ********         |\n|  <    >      /    \\      <    >      /    \\      <    >      /    \\          |\n|                                                                              |\n|   *  *        *  *        *  *        *  *        *  *        *  *           |\n| \\*0**0*/     *0**0*     \\*0**0*/     *0**0*     \\*0**0*/     *0**0*          |\n|  ******     /******\\     ******     /******\\     ******     /******\\         |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|             ~~~~~~~~~~           ~~~~~~~~~~~           ~~~~~~~~~~~           |\n|                                                                              |\n|                                       ^                                      |\n|                                     #####                                    |\n|                                    #######                                   |\n|______________________________________________________________________________|".ToCharArray();
var d = "\0\0\0\0\0".ToCharArray();
var indexCycle = 0;
var indexCycleMove = 1;
var indexInput = 2;
var indexPlayer = 3;
var indexPlayerWin = 4;
var p = 81;

var delayBullet = 4;
var delayAnimation = 16;
var delayMove = 16;
var delayEnemyShoot = 64;
var delayResetBuffer = 4;

var lookLeft = 1;
var lookRight = -1;
var lookDown = -p;

for (; ; )
{
	Thread.Sleep(20);

	Console.Beep(m.Contains('+') ? 800 : (m.Contains('!') ? 4500 : 100), 10);

	Console.SetCursorPosition(0, 0);

	Console.WriteLine(m.AsSpan().ToString());

	d[indexCycle] = (char)(d[indexCycle] + 1);

	d[indexCycleMove] = (d[indexCycle] % delayMove == 5) ? (char)(d[indexCycleMove] + 1) : d[indexCycleMove];

	d[indexInput] = Console.KeyAvailable ? (char)Console.ReadKey(true).Key : '\0';

	d[indexPlayer] = (char)(Array.IndexOf(m, '^') % p);

	d[indexPlayerWin] = (!m.Contains('0')) ? throw new Exception("\n\n\n\n\t\t\t\tWin\n\n\n") : d[indexPlayerWin];

	for (int i = 0; i < m.Length; i++)
	{
		// Конец игры
		if ((m[i] == '_' || m[i] == '^' || m[i] == '#') && m[i - p - p] == '*')
		{
			throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n");
		}

		if (d[indexCycle] % delayResetBuffer == 1)
		{
			// Сбрасываем буфер клавиш
			if (Console.KeyAvailable &&
				Console.ReadKey(true).KeyChar == ' ' &&
				false)
			{
			}
		}

		if (d[indexCycle] % delayAnimation == 3)
		{
			// Анимация ног
			if (m[i] == '<')
			{
				m[i] = '/';
				continue;
			}

			if (m[i] == '>')
			{
				m[i] = '\\';
				continue;
			}

			if (m[i] == '/' && m[i - p] == '*')
			{
				m[i] = '<';
				continue;
			}

			if (m[i] == '\\' && m[i - p] == '*')
			{
				m[i] = '>';
				continue;
			}

			// Анимация рук
			if (m[i] == ' ' && m[i + 2] == '0' && m[i + p] == '/')
			{
				m[i] = '\\';
				continue;
			}

			if (m[i] == '/' && m[i - p] == '\\')
			{
				m[i] = ' ';
				continue;
			}

			if (m[i] == ' ' && m[i - 2] == '0' && m[i + p] == '\\')
			{
				m[i] = '/';
				continue;
			}

			if (m[i] == '\\' && m[i - p] == '/')
			{
				m[i] = ' ';
				continue;
			}

			if (m[i] == ' ' && m[i - p] == '\\' && m[i - p + 2] == '0')
			{
				m[i] = '/';
				continue;
			}

			if (m[i] == ' ' && m[i - p] == '/' && m[i - p - 2] == '0')
			{
				m[i] = '\\';
				continue;
			}
		}
	}

	for (int i = 0; i < m.Length; i++)
	{
		if (d[indexCycle] % delayAnimation == 3)
		{
			// Анимация рук
			if (m[i] == '\\' && m[i + p] == '/')
			{
				m[i] = ' ';
				continue;
			}

			if (m[i] == '/' && m[i + p] == '\\')
			{
				m[i] = ' ';
				continue;
			}
		}
	}

	for (int i = 0; i < m.Length; i++)
	{
		// Разрушаем преграды врагами
		if (m[i] == '~' && (m[i - p - p] == '*' || m[i - p - p - 1] == '*' || m[i - p - p + 1] == '*' || m[i - p - p - 2] == '*' || m[i - p - p + 2] == '*'))
		{
			m[i] = ' ';
			continue;
		}

		// Выстрел
		if (m[i] == ' ' && m[i + p] == '^' && d[indexInput] == ' ' && !m.Contains('!'))
		{
			m[i] = '!';
			continue;
		}

		// Пуля и преграда
		if (m[i] == '~' && m[i + p] == '!')
		{
			m[i] = 'l';
			continue;
		}

		if ((m[i] == '!' && (m[i - p] == 'l' || m[i - p] == '\"')) || (m[i] == 'l'))
		{
			m[i] = ' ';
			continue;
		}

		// Убираем последствия взрыва
		if (m[i] == '+' && (m[i + 1] == '+' || m[i + 1] == ' ') && (m[i - 1] == '+' || m[i - 1] == ' ') && (m[i + p] == '+' || m[i + p] == ' ') && (m[i - p] == '+' || m[i - p] == ' '))
		{
			m[i] = ' ';
			continue;
		}
	}

	for (int i = 0; i < m.Length; i++)
	{
		if (d[indexCycle] % delayBullet == 1)
		{
			// Пуля
			if (m[i] == ' ' && m[i + p] == '!')
			{
				m[i] = '!';
				continue;
			}

			if (m[i] == '!' && m[i - p] == '!')
			{
				m[i] = ' ';
				continue;
			}
		}

		if (d[indexCycle] % delayEnemyShoot == 7 &&
			Random.Shared.Next(0, 100) > 50)
		{
			if (m[i] == ' ' && m[i - p] == '<' && m[i + p] != '*' && m[i + p + 1] != '*' && m[i + p - 1] != '*')
			{
				m[i] = 'o';
				continue;
			}
		}
	}

	for (int i = 0; i < m.Length; i++)
	{
		if (d[indexCycle] % delayBullet == 1)
		{
			// Попадание пули
			if (m[i] >= '*' && m[i] <= '\\' && m[i + p] == '!')
			{
				m[i] = '+';
				continue;
			}

			if (m[i] == '!' && m[i - p] == '+')
			{
				m[i] = ' ';
				continue;
			}
		}

		// Взрыв
		if (m[i] >= '*' && m[i] <= '\\' && (m[i + 1] == '+' || m[i - 1] == '+' || m[i + p] == '+' || m[i - p] == '+'))
		{
			m[i] = '+';
			continue;
		}
	}

	for (int j = m.Length - 1; j >= 0; j--)
	{
		if (d[indexCycle] % delayBullet == 1)
		{
			// Пуля
			if (m[j] == ' ' && m[j - p] == 'o')
			{
				m[j] = 'o';
				continue;
			}

			if (m[j] == 'o' && m[j + p] == 'o')
			{
				m[j] = ' ';
				continue;
			}

			// Попадание в своего
			if (m[j] == 'o' && ((m[j + p] >= '*' && m[j + p] <= '\\') || (m[j + 1] >= '*' && m[j + 1] <= '\\') || (m[j - 1] >= '*' && m[j - 1] <= '\\')))
			{
				m[j] = ' ';
				continue;
			}

			// Попадание в игрока
			if (m[j] == 'o' && (m[j + p] == '^' || m[j + p] == '#'))
			{
				throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n");
			}

			if (m[j] == '~' && m[j - p] == 'o')
			{
				m[j] = 'l';
				continue;
			}

			if ((m[j] == 'o' && (m[j + p] == 'l' || m[j + p] == '_')) || (m[j] == 'o'))
			{
				m[j] = ' ';
				continue;
			}
		}
	}

	for (int j = m.Length - 1; j >= 0; j--)
	{
		if (d[indexCycle] % delayMove == 5)
		{
			// Сдвиг врагов вправо
			if ((d[indexCycleMove] % 18) > 0 &&
				(d[indexCycleMove] % 18) < 9)
			{
				if ((m[j] == ' ' || (m[j] >= '*' && m[j] <= '\\')) && m[j + lookRight] >= '*' && m[j + lookRight] <= '\\')
				{
					m[j] = m[j + lookRight];
					continue;
				}

				if (m[j] >= '*' && m[j] <= '\\' && (m[j + lookRight] == ' ' || m[j + lookRight] == '\"'))
				{
					m[j] = ' ';
					continue;
				}
			}

			// Сдвиг врагов вниз
			if ((d[indexCycleMove] % 18) == 9 ||
				(d[indexCycleMove] % 18) == 0)
			{
				if ((m[j] == ' ' || (m[j] >= '*' && m[j] <= '\\')) && m[j + lookDown] >= '*' && m[j + lookDown] <= '\\')
				{
					m[j] = m[j + lookDown];
					continue;
				}

				if (m[j] >= '*' && m[j] <= '\\' && (m[j + lookDown] == ' ' || m[j + lookDown] == '\"'))
				{
					m[j] = ' ';
					continue;
				}
			}
		}

		// Сдвиг персонажа вправо
		if ((d[indexInput] == 68 || d[indexInput] == 39) &&
			d[indexPlayer] < p - 7)
		{
			if ((m[j] == ' ' || m[j] == '#' || m[j] == '^') && (m[j + lookRight] == '#' || m[j + lookRight] == '^'))
			{
				m[j] = m[j + lookRight];
				continue;
			}

			if ((m[j] == '#' || m[j] == '^') && (m[j + lookRight] == ' ' || m[j + lookRight] == '\"'))
			{
				m[j] = ' ';
				continue;
			}
		}
	}

	for (int i = 0; i < m.Length; i++)
	{
		if (d[indexCycle] % delayMove == 5)
		{
			// Сдвиг врагов влево
			if ((d[indexCycleMove] % 18) > 9)
			{
				if ((m[i] == ' ' || (m[i] >= '*' && m[i] <= '\\')) && m[i + lookLeft] >= '*' && m[i + lookLeft] <= '\\')
				{
					m[i] = m[i + lookLeft];
					continue;
				}

				if (m[i] >= '*' && m[i] <= '\\' && (m[i + lookLeft] == ' ' || m[i + lookLeft] == '\"'))
				{
					m[i] = ' ';
					continue;
				}
			}
		}

		// Сдвиг персонажа влево
		if ((d[indexInput] == 65 || d[indexInput] == 37) &&
			d[indexPlayer] > 5)
		{
			if ((m[i] == ' ' || m[i] == '#' || m[i] == '^') && (m[i + lookLeft] == '#' || m[i + lookLeft] == '^'))
			{
				m[i] = m[i + lookLeft];
				continue;
			}

			if ((m[i] == '#' || m[i] == '^') && (m[i + lookLeft] == ' '))
			{
				m[i] = ' ';
				continue;
			}
		}
	}
}
