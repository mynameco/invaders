﻿var m = "\0\0\0\0\0|\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"|\n|    **          **          **          **          **          **            |\n|  *0**0*      *0**0*      *0**0*      *0**0*      *0**0*      *0**0*          |\n|  ******      ******      ******      ******      ******      ******          |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|   ****        ****        ****        ****        ****        ****           |\n| **0**0**    **0**0**    **0**0**    **0**0**    **0**0**    **0**0**         |\n| ********    ********    ********    ********    ********    ********         |\n|  <    >      /    \\      <    >      /    \\      <    >      /    \\          |\n|                                                                              |\n|   *  *        *  *        *  *        *  *        *  *        *  *           |\n| \\*0**0*/     *0**0*     \\*0**0*/     *0**0*     \\*0**0*/     *0**0*          |\n|  ******     /******\\     ******     /******\\     ******     /******\\         |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|             ~~~~~~~~~~           ~~~~~~~~~~~           ~~~~~~~~~~~           |\n|                                                                              |\n|                                       ^                                      |\n|                                     #####                                    |\n|                                    #######                                   |\n|______________________________________________________________________________|".ToCharArray();
loop:
Console.SetCursorPosition(0, 0);
Console.WriteLine(m.AsSpan(5).ToString());
for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (i <= 5)
	{
		m[i] = i == 0 ? (char)(m[i] + 1) : ((i == 1 && m[0] % 16 == 0) ? (char)(m[i] + 1) : (i == 2 && !Console.KeyAvailable ? '\0' : (i == 2 && Console.KeyAvailable ? Console.ReadKey(true).KeyChar : i == 3 ? (char)(m.AsSpan(5).ToString().IndexOf('^') % 81) : (i == 4 && m.AsSpan(5).ToString().IndexOf('0') == -1 ? throw new Exception("\n\n\n\n\t\t\t\tWin\n\n\n") : m[i]))));
		if (m[i] != ch)
			continue;
	}
	else
	{
		m[i] = (m[i] == '_' || m[i] == '^' || m[i] == '#') && m[i - 81 - 81] == '*' ? throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n") : (m[0] % 16 == 0 ? (m[i] == '<' ? '/' : (m[i] == '>' ? '\\' : (m[i] == '/' && m[i - 81] == '*' ? '<' : (m[i] == '\\' && m[i - 81] == '*' ? '>' : (m[i] == ' ' && m[i + 2] == '0' && m[i + 81] == '/' ? '\\' : (m[i] == '/' && m[i - 81] == '\\' ? ' ' : (m[i] == ' ' && m[i - 2] == '0' && m[i + 81] == '\\' ? '/' : (m[i] == '\\' && m[i - 81] == '/' ? ' ' : (m[i] == ' ' && m[i - 81] == '\\' && m[i - 81 + 2] == '0' ? '/' : (m[i] == ' ' && m[i - 81] == '/' && m[i - 81 - 2] == '0' ? '\\' : m[i])))))))))) : (m[i] == '~' && (m[i - 81 - 81] == '*' || m[i - 81 - 81 - 1] == '*' || m[i - 81 - 81 + 1] == '*' || m[i - 81 - 81 - 2] == '*' || m[i - 81 - 81 + 2] == '*') ? ' ' : ((m[i] == ' ' && m[i + 81] == '^' && m[2] == ' ' && m.AsSpan(5).ToString().IndexOf('!') == -1) ? '!' : ((m[i] == '~' && m[i + 81] == '!') ? 'l' : ((m[i] == '!' && (m[i - 81] == 'l' || m[i - 81] == '\"')) || (m[i] == 'l') ? ' ' : m[i])))));
		if (m[i] != ch)
			continue;
	}
}

for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (i <= 5)
	{
		// Сбрасываем буфер клавиш
		m[i] = Console.KeyAvailable && Console.ReadKey(true).KeyChar == ' ' ? m[i] : m[i];
	}
	else
	{
		if (m[0] % 16 == 0)
		{
			// Анимация рук
			m[i] = m[i] == '\\' && m[i + 81] == '/' ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '/' && m[i + 81] == '\\' ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		if (m[0] % 4 == 0)
		{
			// Пуля
			m[i] = (m[i] == ' ' && m[i + 81] == '!') ? '!' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '!' && m[i - 81] == '!') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		// Сдвиг чтобы не конфликтовало с движением
		if (m[0] % 64 == 3 &&
			Random.Shared.Next(0, 100) > 50)
		{
			m[i] = (m[i] == ' ' && m[i - 81] == '<' && m[i + 81] != '*' && m[i + 81 + 1] != '*' && m[i + 81 - 1] != '*') ? 'o' : m[i];
			if (m[i] != ch)
				continue;
		}
	}
}

for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (i > 5)
	{
		if (m[0] % 4 == 0)
		{
			// Пуля
			m[i] = ((m[i] >= '*' && m[i] <= '\\') && m[i + 81] == '!') ? '+' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '!' && m[i - 81] == '+') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		// Взрыв
		m[i] = (m[i] >= '*' && m[i] <= '\\') && (m[i + 1] == '+' || m[i - 1] == '+' || m[i + 81] == '+' || m[i - 81] == '+') ? '+' : m[i];
		if (m[i] != ch)
			continue;

		m[i] = m[i] == '+' && (m[i + 1] == '+' || m[i + 1] == ' ') && (m[i - 1] == '+' || m[i - 1] == ' ') && (m[i + 81] == '+' || m[i + 81] == ' ') && (m[i - 81] == '+' || m[i - 81] == ' ') ? ' ' : m[i];
		if (m[i] != ch)
			continue;
	}
}

var cycle = m[1] % 18;

for (int i = m.Length - 1; i >= 0; i--)
{
	var ch = m[i];

	if (i > 5)
	{
		// Сдвиг персонажа вправо
		if (m[2] == 'd' &&
			m[3] < 81 - 7)
		{
			var look = -1;

			m[i] = (m[i] == ' ' || (m[i] == '#' || m[i] == '^')) && (m[i + look] == '#' || m[i + look] == '^') ? m[i + look] : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '#' || m[i] == '^') && (m[i + look] == ' ' || m[i + look] == '\"') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		if (m[0] % 16 == 0)
		{
			// Сдвиг врагов вправо
			if (cycle > 0 &&
				cycle < 9)
			{
				var look = -1;

				m[i] = (m[i] == ' ' || (m[i] >= '*' && m[i] <= '\\')) && (m[i + look] >= '*' && m[i + look] <= '\\') ? m[i + look] : m[i];
				if (m[i] != ch)
					continue;

				m[i] = (m[i] >= '*' && m[i] <= '\\') && (m[i + look] == ' ' || m[i + look] == '\"') ? ' ' : m[i];
				if (m[i] != ch)
					continue;
			}

			// Сдвиг врагов вниз
			if (cycle == 9 ||
				cycle == 0)
			{
				var look = -81;

				m[i] = (m[i] == ' ' || (m[i] >= '*' && m[i] <= '\\')) && (m[i + look] >= '*' && m[i + look] <= '\\') ? m[i + look] : m[i];
				if (m[i] != ch)
					continue;

				m[i] = (m[i] >= '*' && m[i] <= '\\') && (m[i + look] == ' ' || m[i + look] == '\"') ? ' ' : m[i];
				if (m[i] != ch)
					continue;
			}
		}

		if (m[0] % 4 == 0)
		{
			// Пуля
			m[i] = (m[i] == ' ' && m[i - 81] == 'o') ? 'o' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == 'o' && m[i + 81] == 'o') ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			// Попадание в своего
			m[i] = m[i] == 'o' && ((m[i + 81] >= '*' && m[i + 81] <= '\\') || (m[i + 1] >= '*' && m[i + 1] <= '\\') || (m[i - 1] >= '*' && m[i - 1] <= '\\')) ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			// Попадание в игрока
			m[i] = m[i] == 'o' && (m[i + 81] == '^' || m[i + 81] == '#') ? throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n") : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '~' && m[i - 81] == 'o') ? 'l' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == 'o' && (m[i + 81] == 'l' || m[i + 81] == '_')) || (m[i] == 'o') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}
	}
}

for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (i > 5)
	{
		// Сдвиг персонажа вправо
		if (m[2] == 'a' &&
			m[3] > 5)
		{
			var look = 1;

			m[i] = (m[i] == ' ' || (m[i] == '#' || m[i] == '^')) && (m[i + look] == '#' || m[i + look] == '^') ? m[i + look] : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '#' || m[i] == '^') && (m[i + look] == ' ') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		if (m[0] % 16 == 0)
		{
			// Сдвиг врагов влево
			if (cycle > 9)
			{
				var look = 1;

				m[i] = (m[i] == ' ' || (m[i] >= '*' && m[i] <= '\\')) && (m[i + look] >= '*' && m[i + look] <= '\\') ? m[i + look] : m[i];
				if (m[i] != ch)
					continue;

				m[i] = (m[i] >= '*' && m[i] <= '\\') && (m[i + look] == ' ' || m[i + look] == '\"') ? ' ' : m[i];
				if (m[i] != ch)
					continue;
			}
		}
	}
}

Console.Beep(m.AsSpan(5).ToString().IndexOf('+') != -1 ? 800 : (m.AsSpan(5).ToString().IndexOf('!') != -1 ? 4500 : 100), 10);
Thread.Sleep(20);
goto loop;
