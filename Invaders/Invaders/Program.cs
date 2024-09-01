var m = "\0\0\0\0\0|\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"|\n|    **          **          **          **          **          **            |\n|  *0**0*      *0**0*      *0**0*      *0**0*      *0**0*      *0**0*          |\n|  ******      ******      ******      ******      ******      ******          |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|   ****        ****        ****        ****        ****        ****           |\n| **0**0**    **0**0**    **0**0**    **0**0**    **0**0**    **0**0**         |\n| ********    ********    ********    ********    ********    ********         |\n|  <    >      /    \\      <    >      /    \\      <    >      /    \\          |\n|                                                                              |\n|   *  *        *  *        *  *        *  *        *  *        *  *           |\n| \\*0**0*/     *0**0*     \\*0**0*/     *0**0*     \\*0**0*/     *0**0*          |\n|  ******     /******\\     ******     /******\\     ******     /******\\         |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|             ~~~~~~~~~~           ~~~~~~~~~~~           ~~~~~~~~~~~           |\n|                                                                              |\n|                                       ^                                      |\n|                                     #####                                    |\n|                                    #######                                   |\n|______________________________________________________________________________|".ToCharArray();
var offset = 5;
var indexCycle = 0;
var indexCycleMove = 1;
var indexInput = 2;
var indexPlayer = 3;
var indexPlayerTmp = 4;
var p = 81;

var delayBullet = 4;
var delayAnimation = 16;
var delayMove = 16;
var delayEnemyShoot = 64;

var lookLeft = 1;
var lookRight = -1;
var lookDown = -p;

loop:
Console.SetCursorPosition(0, 0);
Console.WriteLine(m.AsSpan(offset).ToString());

for (int i = 0; i < m.Length; i++)
{
	if (i <= offset)
	{
		// Число по кругу
		if (i == indexCycle)
		{
			m[i] = (char)(m[i] + 1);
			continue;
		}

		// Число по кругу
		if (i == indexCycleMove && m[indexCycle] % delayMove == 5)
		{
			m[i] = (char)(m[i] + 1);
			continue;
		}

		// Инпут
		if (i == indexInput && !Console.KeyAvailable)
		{
			m[i] = '\0';
			continue;
		}

		if (i == indexInput && Console.KeyAvailable)
		{
			m[i] = Console.ReadKey(true).KeyChar;
			continue;
		}

		// Позиция персонажа
		if (i == indexPlayer)
		{
			m[i] = (char)(m.AsSpan(offset).ToString().IndexOf('^') % p);
			continue;
		}

		// Победа
		if (i == indexPlayerTmp && m.AsSpan(offset).ToString().IndexOf('0') == -1)
		{
			throw new Exception("\n\n\n\n\t\t\t\tWin\n\n\n");
			continue;
		}
	}
	else
	{
		// Конец игры
		if ((m[i] == '_' || m[i] == '^' || m[i] == '#') && m[i - p - p] == '*')
		{
			throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n");
			continue;
		}

		if (m[indexCycle] % delayAnimation == 3)
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
}

for (int i = 0; i < m.Length; i++)
{
	if (i <= offset)
	{
		// Сбрасываем буфер клавиш
		if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == ' ' && false)
		{
		}
	}
	else
	{
		if (m[indexCycle] % delayAnimation == 3)
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
}

for (int i = 0; i < m.Length; i++)
{
	if (i > offset)
	{
		// Разрушаем преграды врагами
		if (m[i] == '~' && (m[i - p - p] == '*' || m[i - p - p - 1] == '*' || m[i - p - p + 1] == '*' || m[i - p - p - 2] == '*' || m[i - p - p + 2] == '*'))
		{
			m[i] = ' ';
			continue;
		}

		// Выстрел
		if (m[i] == ' ' && m[i + p] == '^' && m[indexInput] == ' ' && m.AsSpan(offset).ToString().IndexOf('!') == -1)
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
}

for (int i = 0; i < m.Length; i++)
{
	if (i > offset)
	{
		if (m[indexCycle] % delayBullet == 1)
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

		if (m[indexCycle] % delayEnemyShoot == 7 && Random.Shared.Next(0, 100) > 50)
		{
			if (m[i] == ' ' && m[i - p] == '<' && m[i + p] != '*' && m[i + p + 1] != '*' && m[i + p - 1] != '*')
			{
				m[i] = 'o';
				continue;
			}
		}
	}
}

for (int i = 0; i < m.Length; i++)
{
	if (i > offset)
	{
		if (m[indexCycle] % delayBullet == 1)
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
}

for (int i = m.Length - 1; i >= 0; i--)
{
	if (i > offset)
	{
		if (m[indexCycle] % delayMove == 5)
		{
			// Сдвиг врагов вправо
			if ((m[indexCycleMove] % 18) > 0 &&
				(m[indexCycleMove] % 18) < 9)
			{
				if ((m[i] == ' ' || (m[i] >= '*' && m[i] <= '\\')) && m[i + lookRight] >= '*' && m[i + lookRight] <= '\\')
				{
					m[i] = m[i + lookRight];
					continue;
				}

				if (m[i] >= '*' && m[i] <= '\\' && (m[i + lookRight] == ' ' || m[i + lookRight] == '\"'))
				{
					m[i] = ' ';
					continue;
				}
			}

			// Сдвиг врагов вниз
			if ((m[indexCycleMove] % 18) == 9 ||
				(m[indexCycleMove] % 18) == 0)
			{
				if ((m[i] == ' ' || (m[i] >= '*' && m[i] <= '\\')) && m[i + lookDown] >= '*' && m[i + lookDown] <= '\\')
				{
					m[i] = m[i + lookDown];
					continue;
				}

				if (m[i] >= '*' && m[i] <= '\\' && (m[i + lookDown] == ' ' || m[i + lookDown] == '\"'))
				{
					m[i] = ' ';
					continue;
				}
			}
		}

		// Сдвиг персонажа вправо
		if (m[indexInput] == 'd' &&
			m[indexPlayer] < p - 7)
		{
			if ((m[i] == ' ' || m[i] == '#' || m[i] == '^') && (m[i + lookRight] == '#' || m[i + lookRight] == '^'))
			{
				m[i] = m[i + lookRight];
				continue;
			}

			if ((m[i] == '#' || m[i] == '^') && (m[i + lookRight] == ' ' || m[i + lookRight] == '\"'))
			{
				m[i] = ' ';
				continue;
			}
		}
	}
}

for (int i = m.Length - 1; i >= 0; i--)
{
	if (i > offset)
	{
		if (m[indexCycle] % delayBullet == 1)
		{
			// Пуля
			if (m[i] == ' ' && m[i - p] == 'o')
			{
				m[i] = 'o';
				continue;
			}

			if (m[i] == 'o' && m[i + p] == 'o')
			{
				m[i] = ' ';
				continue;
			}

			// Попадание в своего
			if (m[i] == 'o' && ((m[i + p] >= '*' && m[i + p] <= '\\') || (m[i + 1] >= '*' && m[i + 1] <= '\\') || (m[i - 1] >= '*' && m[i - 1] <= '\\')))
			{
				m[i] = ' ';
				continue;
			}

			// Попадание в игрока
			if (m[i] == 'o' && (m[i + p] == '^' || m[i + p] == '#'))
			{
				throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n");
				continue;
			}

			if (m[i] == '~' && m[i - p] == 'o')
			{
				m[i] = 'l';
				continue;
			}

			if ((m[i] == 'o' && (m[i + p] == 'l' || m[i + p] == '_')) || (m[i] == 'o'))
			{
				m[i] = ' ';
				continue;
			}
		}
	}
}

for (int i = 0; i < m.Length; i++)
{
	if (i > offset)
	{
		if (m[indexCycle] % delayMove == 5)
		{
			// Сдвиг врагов влево
			if ((m[indexCycleMove] % 18) > 9)
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
		if (m[indexInput] == 'a' &&
			m[indexPlayer] > 5)
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

var fr = m.AsSpan(offset).ToString().IndexOf('+') != -1 ? 800 : (m.AsSpan(offset).ToString().IndexOf('!') != -1 ? 4500 : 100);
Console.Beep(fr, 10);

Thread.Sleep(10);

goto loop;
