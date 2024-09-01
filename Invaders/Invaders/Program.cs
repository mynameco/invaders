var m = "a+------------------------------------------------------------------------------+\n|    **          **          **          **          **          **            |\n|  *0**0*      *0**0*      *0**0*      *0**0*      *0**0*      *0**0*          |\n|  ******      ******      ******      ******      ******      ******          |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|   ****        ****        ****        ****        ****        ****           |\n| **0**0**    **0**0**    **0**0**    **0**0**    **0**0**    **0**0**         |\n| ********    ********    ********    ********    ********    ********         |\n|  <    >      /    \\      <    >      /    \\      <    >      /    \\          |\n|                                                                              |\n|   *  *        *  *        *  *        *  *        *  *        *  *           |\n| \\*0**0*/     *0**0*     \\*0**0*/     *0**0*     \\*0**0*/     *0**0*          |\n|  ******     /******\\     ******     /******\\     ******     /******\\         |\n|   /  \\        <  >        /  \\        <  >        /  \\        <  >           |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|                                                                              |\n|    ==========          ==========          ==========          ==========    |\n|                                                                              |\n|                                       ^                                      |\n|                                     #####                                    |\n|                                    #######                                   |\n+------------------------------------------------------------------------------+".ToCharArray();
var offset = 1;
var index1 = 0;
var page = 81;

loop:
Console.SetCursorPosition(0, 0);
Console.WriteLine(m.AsSpan(offset).ToString());

var pass = 0;
for (int i = 0; i < m.Length; i++)
{
	if (pass == 0)
	{
		if (i <= offset)
		{
			m[index1] = i == index1 ? (char)((m[index1] - 'a' + 1) % 2 + 'a') : m[i];
		}
		else
		{
			var ch = m[i];

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
		}
	}
}

pass = 1;
for (int i = 0; i < m.Length; i++)
{
	if (pass == 1)
	{
		if (i > offset)
		{
			var ch = m[i];

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

pass = 2;
for (int i = 0; i < m.Length; i++)
{
	if (pass == 2)
	{
		if (i > offset)
		{
			var i2 = m.Length - i - 1;
			var ch = m[i2];

			// Сдвиг в право
			m[i2] = (m[i2] == ' ' || m[i2] == '/' || m[i2] == '\\' || m[i2] == '*' || m[i2] == '0' || m[i2] == '<' || m[i2] == '>') && (m[i2 - 1] == '/' || m[i2 - 1] == '\\' || m[i2 - 1] == '*' || m[i2 - 1] == '0' || m[i2 - 1] == '<' || m[i2 - 1] == '>') ? m[i2 - 1] : m[i2];
			if (m[i2] != ch)
				continue;

			m[i2] = (m[i2] == '/' || m[i2] == '\\' || m[i2] == '*' || m[i2] == '0' || m[i2] == '<' || m[i2] == '>') && m[i2 - 1] == ' ' ? ' ' : m[i2];
			if (m[i2] != ch)
				continue;
		}
	}
}

Thread.Sleep(1000);
goto loop;
