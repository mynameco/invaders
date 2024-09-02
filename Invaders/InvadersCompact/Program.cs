var m = "|\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"|\n|    **          **          **          **          **          **            |\n|  *0**0*      *0**0*      *0**0*      *0**0*      *0**0*      *0**0*          |\n|  ******      ******      ******      ******      ******      ******          |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|   ****        ****        ****        ****        ****        ****           |\n| **0**0**    **0**0**    **0**0**    **0**0**    **0**0**    **0**0**         |\n| ********    ********    ********    ********    ********    ********         |\n|  <    >      /    \\      <    >      /    \\      <    >      /    \\          |\n|                                                                              |\n|   *  *        *  *        *  *        *  *        *  *        *  *           |\n| \\*0**0*/     *0**0*     \\*0**0*/     *0**0*     \\*0**0*/     *0**0*          |\n|  ******     /******\\     ******     /******\\     ******     /******\\         |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|             ~~~~~~~~~~           ~~~~~~~~~~~           ~~~~~~~~~~~           |\n|                                                                              |\n|                                       ^                                      |\n|                                     #####                                    |\n|                                    #######                                   |\n|______________________________________________________________________________|".ToCharArray();
var d = "\0\0\0\0\0".ToCharArray();
var indexCycle = 0;
var indexCycleMove = 1;
var indexInput = 2;
var indexPlayer = 3;
var indexPlayerWin = 4;
var page = 81;

var delayBullet = 4;
var delayAnimation = 16;
var delayMove = 16;
var delayEnemyShoot = 64;
var delayResetBuffer = 4;

var lookLeft = 1;
var lookRight = -1;
var lookDown = -page;

for (; ; )
{
	Thread.Sleep(20);

	//Console.Beep(m.Contains('+') ? 800 : (m.Contains('!') ? 4500 : 100), 10);

	Console.SetCursorPosition(0, 0);

	Console.WriteLine(m.AsSpan().ToString());

	d[indexCycle] = (char)(d[indexCycle] + 1);

	d[indexCycleMove] = (d[indexCycle] % delayMove == 5) ? (char)(d[indexCycleMove] + 1) : d[indexCycleMove];

	d[indexInput] = Console.KeyAvailable ? Console.ReadKey(true).KeyChar : '\0';

	d[indexPlayer] = (char)(Array.IndexOf(m, '^') % page);

	d[indexPlayerWin] = (!m.Contains('0')) ? throw new Exception("\n\n\n\n\t\t\t\tWin\n\n\n") : d[indexPlayerWin];

	for (int i = 0, j = m.Length - i - 1, k = 0, p = 0, count = m.Length * 8; k < count; k++, i = k % m.Length, j = m.Length - i - 1, p = k / m.Length)
	{
		if (p == 0)
		{
			// Конец игры
			if ((m[i] == '_' || m[i] == '^' || m[i] == '#') && m[i - page - page] == '*')
			{
				throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n");
				continue;
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

				if (m[i] == '/' && m[i - page] == '*')
				{
					m[i] = '<';
					continue;
				}

				if (m[i] == '\\' && m[i - page] == '*')
				{
					m[i] = '>';
					continue;
				}

				// Анимация рук
				if (m[i] == ' ' && m[i + 2] == '0' && m[i + page] == '/')
				{
					m[i] = '\\';
					continue;
				}

				if (m[i] == '/' && m[i - page] == '\\')
				{
					m[i] = ' ';
					continue;
				}

				if (m[i] == ' ' && m[i - 2] == '0' && m[i + page] == '\\')
				{
					m[i] = '/';
					continue;
				}

				if (m[i] == '\\' && m[i - page] == '/')
				{
					m[i] = ' ';
					continue;
				}

				if (m[i] == ' ' && m[i - page] == '\\' && m[i - page + 2] == '0')
				{
					m[i] = '/';
					continue;
				}

				if (m[i] == ' ' && m[i - page] == '/' && m[i - page - 2] == '0')
				{
					m[i] = '\\';
					continue;
				}
			}
		}
		else if (p == 1)
		{
			m[i] = (d[indexCycle] % delayAnimation == 3) ? ((m[i] == '\\' && m[i + page] == '/') ? ' ' : ((m[i] == '/' && m[i + page] == '\\') ? ' ' : m[i])) : m[i];
		}
		else if (p == 2)
		{
			m[i] = (m[i] == '~' && (m[i - page - page] == '*' || m[i - page - page - 1] == '*' || m[i - page - page + 1] == '*' || m[i - page - page - 2] == '*' || m[i - page - page + 2] == '*')) ? ' ' : ((m[i] == ' ' && m[i + page] == '^' && d[indexInput] == ' ' && !m.Contains('!')) ? '!' : ((m[i] == '~' && m[i + page] == '!') ? 'l' : ((m[i] == '!' && (m[i - page] == 'l' || m[i - page] == '\"')) || (m[i] == 'l')) ? ' ' : ((m[i] == '+' && (m[i + 1] == '+' || m[i + 1] == ' ') && (m[i - 1] == '+' || m[i - 1] == ' ') && (m[i + page] == '+' || m[i + page] == ' ') && (m[i - page] == '+' || m[i - page] == ' ')) ? ' ' : m[i])));
		}
		else if (p == 3)
		{
			m[i] = (d[indexCycle] % delayBullet == 1) ? ((m[i] == ' ' && m[i + page] == '!') ? '!' : ((m[i] == '!' && m[i - page] == '!') ? ' ' : m[i])) : ((d[indexCycle] % delayEnemyShoot == 7 && Random.Shared.Next(0, 100) > 50) ? ((m[i] == ' ' && m[i - page] == '<' && m[i + page] != '*' && m[i + page + 1] != '*' && m[i + page - 1] != '*') ? 'o' : m[i]) : m[i]);
		}
		else if (p == 4)
		{
			m[i] = (d[indexCycle] % delayBullet == 1) ? (m[i] >= '*' && m[i] <= '\\' && m[i + page] == '!') ? '+' : ((m[i] == '!' && m[i - page] == '+') ? ' ' : m[i]) : ((m[i] >= '*' && m[i] <= '\\' && (m[i + 1] == '+' || m[i - 1] == '+' || m[i + page] == '+' || m[i - page] == '+')) ? '+' : m[i]);
		}
		else if (p == 5)
		{
			if (d[indexCycle] % delayBullet == 1)
			{
				// Пуля
				if (m[j] == ' ' && m[j - page] == 'o')
				{
					m[j] = 'o';
					continue;
				}

				if (m[j] == 'o' && m[j + page] == 'o')
				{
					m[j] = ' ';
					continue;
				}

				// Попадание в своего
				if (m[j] == 'o' && ((m[j + page] >= '*' && m[j + page] <= '\\') || (m[j + 1] >= '*' && m[j + 1] <= '\\') || (m[j - 1] >= '*' && m[j - 1] <= '\\')))
				{
					m[j] = ' ';
					continue;
				}

				// Попадание в игрока
				if (m[j] == 'o' && (m[j + page] == '^' || m[j + page] == '#'))
				{
					throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n");
					continue;
				}

				if (m[j] == '~' && m[j - page] == 'o')
				{
					m[j] = 'l';
					continue;
				}

				if ((m[j] == 'o' && (m[j + page] == 'l' || m[j + page] == '_')) || (m[j] == 'o'))
				{
					m[j] = ' ';
					continue;
				}
			}
		}
		else if (p == 6)
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
			if (d[indexInput] == 'd' &&
				d[indexPlayer] < page - 7)
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
		else if (p == 7)
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
			if (d[indexInput] == 'a' &&
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
}
