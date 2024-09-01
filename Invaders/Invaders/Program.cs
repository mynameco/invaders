var m = "\0\0\0\0\0|\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"|\n|    **          **          **          **          **          **            |\n|  *0**0*      *0**0*      *0**0*      *0**0*      *0**0*      *0**0*          |\n|  ******      ******      ******      ******      ******      ******          |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|   ****        ****        ****        ****        ****        ****           |\n| **0**0**    **0**0**    **0**0**    **0**0**    **0**0**    **0**0**         |\n| ********    ********    ********    ********    ********    ********         |\n|  <    >      /    \\      <    >      /    \\      <    >      /    \\          |\n|                                                                              |\n|   *  *        *  *        *  *        *  *        *  *        *  *           |\n| \\*0**0*/     *0**0*     \\*0**0*/     *0**0*     \\*0**0*/     *0**0*          |\n|  ******     /******\\     ******     /******\\     ******     /******\\         |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|    ~~~~~~~~~~          ~~~~~~~~~~          ~~~~~~~~~~          ~~~~~~~~~~    |\n|                                                                              |\n|                                       ^                                      |\n|                                     #####                                    |\n|                                    #######                                   |\n|______________________________________________________________________________|".ToCharArray();
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

loop:
Console.SetCursorPosition(0, 0);
Console.WriteLine(m.AsSpan(offset).ToString());

for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (i <= offset)
	{
		// Число по кругу
		m[i] = i == indexCycle ? (char)(m[i] + 1) : m[i];
		if (m[i] != ch)
			continue;

		// Число по кругу
		m[i] = (i == indexCycleMove && m[indexCycle] % delayMove == 0) ? (char)(m[i] + 1) : m[i];
		if (m[i] != ch)
			continue;

		m[i] = i == indexInput && !Console.KeyAvailable ? '\0' : m[i];
		if (m[i] != ch)
			continue;

		m[i] = i == indexInput && Console.KeyAvailable ? Console.ReadKey(true).KeyChar : m[i];
		if (m[i] != ch)
			continue;

		m[i] = i == indexPlayer ? (char)(m.AsSpan(offset).ToString().IndexOf('^') % p) : m[i];
		if (m[i] != ch)
			continue;

		m[i] = i == indexPlayerTmp && m.AsSpan(offset).ToString().IndexOf('0') == -1 ? throw new Exception("Win") : m[i];
		if (m[i] != ch)
			continue;
	}
	else
	{
		// Конец игры
		m[i] = (m[i] == '_' || m[i] == '^' || m[i] == '#') && m[i - p - p] == '*' ? throw new Exception("Game Over") : m[i];
		if (m[i] != ch)
			continue;

		if (m[indexCycle] % delayAnimation == 0)
		{
			// Анимация ног
			m[i] = m[i] == '<' ? '/' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '>' ? '\\' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '/' && m[i - p] == '*' ? '<' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '\\' && m[i - p] == '*' ? '>' : m[i];
			if (m[i] != ch)
				continue;

			// Анимация рук
			m[i] = m[i] == ' ' && m[i + 2] == '0' && m[i + p] == '/' ? '\\' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '/' && m[i - p] == '\\' ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == ' ' && m[i - 2] == '0' && m[i + p] == '\\' ? '/' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '\\' && m[i - p] == '/' ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == ' ' && m[i - p] == '\\' && m[i - p + 2] == '0' ? '/' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == ' ' && m[i - p] == '/' && m[i - p - 2] == '0' ? '\\' : m[i];
			if (m[i] != ch)
				continue;
		}

		// Разрушаем преграды
		m[i] = m[i] == '~' && (m[i - p - p] == '*' || m[i - p - p - 1] == '*' || m[i - p - p + 1] == '*' || m[i - p - p - 2] == '*' || m[i - p - p + 2] == '*') ? ' ' : m[i];
		if (m[i] != ch)
			continue;

		// Выстрел
		m[i] = (m[i] == ' ' && m[i + p] == '^' && m[indexInput] == ' ') ? '!' : m[i];
		if (m[i] != ch)
			continue;

		// Пуля и преграда
		m[i] = (m[i] == '~' && m[i + p] == '!') ? 'o' : m[i];
		if (m[i] != ch)
			continue;

		m[i] = (m[i] == '!' && (m[i - p] == 'o' || m[i - p] == '\"')) || (m[i] == 'o') ? ' ' : m[i];
		if (m[i] != ch)
			continue;
	}
}

for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (i <= offset)
	{
		// Сбрасываем буфер клавиш
		m[i] = Console.KeyAvailable && Console.ReadKey(true).KeyChar == ' ' ? m[i] : m[i];
	}
	else
	{
		if (m[indexCycle] % delayAnimation == 0)
		{
			// Анимация рук
			m[i] = m[i] == '\\' && m[i + p] == '/' ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '/' && m[i + p] == '\\' ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		if (m[indexCycle] % delayBullet == 0)
		{
			// Пуля
			m[i] = (m[i] == ' ' && m[i + p] == '!') ? '!' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '!' && m[i - p] == '!') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}
	}
}

for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (i > offset)
	{
		if (m[indexCycle] % delayBullet == 0)
		{
			// Пуля
			m[i] = ((m[i] >= '*' && m[i] <= '\\') && m[i + p] == '!') ? '+' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '!' && m[i - p] == '+') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		// Взрыв
		m[i] = (m[i] >= '*' && m[i] <= '\\') && (m[i + 1] == '+' || m[i - 1] == '+' || m[i + p] == '+' || m[i - p] == '+') ? '+' : m[i];
		if (m[i] != ch)
			continue;

		m[i] = m[i] == '+' && (m[i + 1] == '+' || m[i + 1] == ' ') && (m[i - 1] == '+' || m[i - 1] == ' ') && (m[i + p] == '+' || m[i + p] == ' ') && (m[i - p] == '+' || m[i - p] == ' ') ? ' ' : m[i];
		if (m[i] != ch)
			continue;
	}
}

var cycle = m[indexCycleMove] % 18;

for (int i = m.Length - 1; i >= 0; i--)
{
	var ch = m[i];

	if (i > offset)
	{
		// Сдвиг персонажа вправо
		if (m[indexInput] == 'd' &&
			m[indexPlayer] < p - 7)
		{
			var look = -1;

			m[i] = (m[i] == ' ' || (m[i] == '#' || m[i] == '^')) && (m[i + look] == '#' || m[i + look] == '^') ? m[i + look] : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '#' || m[i] == '^') && (m[i + look] == ' ' || m[i + look] == '\"') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		if (m[indexCycle] % delayMove == 0)
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
				var look = -p;

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

for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (i > offset)
	{
		// Сдвиг персонажа вправо
		if (m[indexInput] == 'a' &&
			m[indexPlayer] > 5)
		{
			var look = 1;

			m[i] = (m[i] == ' ' || (m[i] == '#' || m[i] == '^')) && (m[i + look] == '#' || m[i + look] == '^') ? m[i + look] : m[i];
			if (m[i] != ch)
				continue;

			m[i] = (m[i] == '#' || m[i] == '^') && (m[i + look] == ' ') ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}

		if (m[indexCycle] % delayMove == 0)
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

Thread.Sleep(20);
goto loop;
