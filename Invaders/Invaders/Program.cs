var m = "\0+------------------------------------------------------------------------------+\n|    **          **          **          **          **          **            |\n|  *0**0*      *0**0*      *0**0*      *0**0*      *0**0*      *0**0*          |\n|  ******      ******      ******      ******      ******      ******          |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|   ****        ****        ****        ****        ****        ****           |\n| **0**0**    **0**0**    **0**0**    **0**0**    **0**0**    **0**0**         |\n| ********    ********    ********    ********    ********    ********         |\n|  <    >      /    \\      <    >      /    \\      <    >      /    \\          |\n|                                                                              |\n|   *  *        *  *        *  *        *  *        *  *        *  *           |\n| \\*0**0*/     *0**0*     \\*0**0*/     *0**0*     \\*0**0*/     *0**0*          |\n|  ******     /******\\     ******     /******\\     ******     /******\\         |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|    ==========          ==========          ==========          ==========    |\n|                                                                              |\n|                                       ^                                      |\n|                                     #####                                    |\n|                                    #######                                   |\n|______________________________________________________________________________|".ToCharArray();
var offset = 1;
var indexCycle = 0;
var page = 81;

loop:
Console.SetCursorPosition(0, 0);
Console.WriteLine(m.AsSpan(offset).ToString());

var pass = 0;
for (int i = 0; i < m.Length; i++)
{
	if (pass == 0)
	{
		var ch = m[i];

		if (i <= offset)
		{
			m[i] = i == indexCycle ? (char)(m[i] + 1) : m[i];
			if (m[i] != ch)
				continue;
		}
		else
		{
			// Анимация ног
			m[i] = m[i] == '<' ? '/' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '>' ? '\\' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '/' && m[i - page] == '*' ? '<' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '\\' && m[i - page] == '*' ? '>' : m[i];
			if (m[i] != ch)
				continue;

			// Анимация рук
			m[i] = m[i] == ' ' && m[i + 2] == '0' && m[i + page] == '/' ? '\\' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '/' && m[i - page] == '\\' ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == ' ' && m[i - 2] == '0' && m[i + page] == '\\' ? '/' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '\\' && m[i - page] == '/' ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == ' ' && m[i - page] == '\\' && m[i - page + 2] == '0' ? '/' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == ' ' && m[i - page] == '/' && m[i - page - 2] == '0' ? '\\' : m[i];
			if (m[i] != ch)
				continue;

			// Разрушаем преграды
			m[i] = m[i] == '=' && (m[i - page] == '/' || m[i - page] == '\\' || m[i - page] == '<' || m[i - page] == '>') ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			// Конец игры
			m[i] = (m[i] == '_' || m[i] == '^' || m[i] == '#') && (m[i - page] == '/' || m[i - page] == '\\' || m[i - page] == '<' || m[i - page] == '>') ? throw new Exception("Game Over") : m[i];
			if (m[i] != ch)
				continue;
		}
	}
}

pass = 1;
for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (pass == 1)
	{
		if (i > offset)
		{
			// Анимация рук
			m[i] = m[i] == '\\' && m[i + page] == '/' ? ' ' : m[i];
			if (m[i] != ch)
				continue;

			m[i] = m[i] == '/' && m[i + page] == '\\' ? ' ' : m[i];
			if (m[i] != ch)
				continue;
		}
	}
}

var cycle = m[indexCycle] % 18;

pass = 2;
for (int i = 0; i < m.Length - 1; i++)
{
	var i2 = m.Length - i - 1;
	var ch = m[i2];

	if (pass == 2)
	{
		if (i > offset)
		{
			// Сдвиг вправо
			if (cycle > 0 &&
				cycle < 9)
			{
				var look = -1;
				m[i2] = (m[i2] == ' ' || m[i2] == '/' || m[i2] == '\\' || m[i2] == '*' || m[i2] == '0' || m[i2] == '<' || m[i2] == '>') && (m[i2 + look] == '/' || m[i2 + look] == '\\' || m[i2 + look] == '*' || m[i2 + look] == '0' || m[i2 + look] == '<' || m[i2 + look] == '>') ? m[i2 + look] : m[i2];
				if (m[i2] != ch)
					continue;

				m[i2] = (m[i2] == '/' || m[i2] == '\\' || m[i2] == '*' || m[i2] == '0' || m[i2] == '<' || m[i2] == '>') && (m[i2 + look] == ' ' || m[i2 + look] == '-') ? ' ' : m[i2];
				if (m[i2] != ch)
					continue;
			}

			// Сдвиг вниз
			if (cycle == 9 ||
				cycle == 0)
			{
				var look = -page;
				m[i2] = (m[i2] == ' ' || m[i2] == '/' || m[i2] == '\\' || m[i2] == '*' || m[i2] == '0' || m[i2] == '<' || m[i2] == '>') && (m[i2 + look] == '/' || m[i2 + look] == '\\' || m[i2 + look] == '*' || m[i2 + look] == '0' || m[i2 + look] == '<' || m[i2 + look] == '>') ? m[i2 + look] : m[i2];
				if (m[i2] != ch)
					continue;

				m[i2] = (m[i2] == '/' || m[i2] == '\\' || m[i2] == '*' || m[i2] == '0' || m[i2] == '<' || m[i2] == '>') && (m[i2 + look] == ' ' || m[i2 + look] == '-') ? ' ' : m[i2];
				if (m[i2] != ch)
					continue;
			}
		}
	}
}

pass = 3;
for (int i = 0; i < m.Length; i++)
{
	var ch = m[i];

	if (pass == 3)
	{
		if (i > offset)
		{
			// Сдвиг влево
			if (cycle > 9)
			{
				var look = 1;
				m[i] = (m[i] == ' ' || m[i] == '/' || m[i] == '\\' || m[i] == '*' || m[i] == '0' || m[i] == '<' || m[i] == '>') && (m[i + look] == '/' || m[i + look] == '\\' || m[i + look] == '*' || m[i + look] == '0' || m[i + look] == '<' || m[i + look] == '>') ? m[i + look] : m[i];
				if (m[i] != ch)
					continue;

				m[i] = (m[i] == '/' || m[i] == '\\' || m[i] == '*' || m[i] == '0' || m[i] == '<' || m[i] == '>') && (m[i + look] == ' ' || m[i + look] == '-') ? ' ' : m[i];
				if (m[i] != ch)
					continue;
			}
		}
	}
}

Thread.Sleep(500);
goto loop;
