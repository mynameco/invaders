﻿for (char[] d = "\0\0\0\0\0".ToCharArray(), m = ("|" + new string('\"', 78) + "|\n|.**..  **..  **..  **..  **..  **...|\n|  *0**0*.  *0**0*.  *0**0*.  *0**0*.  *0**0*.  *0**0*..  |\n|  ******.  ******.  ******.  ******.  ******.  ******..  |\n|   /  \\..<  >../  \\..<  >../  \\..<  >..   |\n|...................  |\n|   ****..****..****..****..****..****..   |\n| **0**0**.**0**0**.**0**0**.**0**0**.**0**0**.**0**0**.. |\n| ********.********.********.********.********.********.. |\n|  <.>.  /.\\.  <.>.  /.\\.  <.>.  /.\\..  |\n|...................  |\n|   *  *..*  *..*  *..*  *..*  *..*  *..   |\n| \\*0**0*/. *0**0*. \\*0**0*/. *0**0*. \\*0**0*/. *0**0*..  |\n|  ******. /******\\. ******. /******\\. ******. /******\\.. |\n|   /  \\..<  >../  \\..<  >../  \\..<  >..   |\n|...................  |\n|...................  |\n|...................  |\n|...................  |\n|...................  |\n|... ~~~~~~~~~~..   ~~~~~~~~~~~..   ~~~~~~~~~~~..   |\n|...................  |\n|.........   ^.........  |\n|......... #####.........|\n|.........#######........   |\n|".Replace(".", "    ") + new string('_', 78) + "|").ToCharArray(); true;)
{
	for (int i = 0, j = m.Length - i - 1, k = 0, p = 0, count = m.Length * 9; k < count; k++, i = k % m.Length, j = m.Length - i - 1, p = k / m.Length)
	{
		if (p == 0)
		{
			if (i == 0)
			{
				Thread.Sleep(20);
				Console.Beep(m.Contains('+') ? 800 : (m.Contains('!') ? 4500 : 100), 10);
				Console.SetCursorPosition(0, 0);
				Console.WriteLine(new string(m));
				d[0] = (char)(d[0] + 1);
				d[1] = (d[0] % 16 == 5) ? (char)(d[1] + 1) : d[1];
				d[2] = Console.KeyAvailable ? Console.ReadKey(true).KeyChar : '\0';
				d[3] = (char)(Array.IndexOf(m, '^') % 81);
				d[4] = (!m.Contains('0')) ? throw new Exception("\n\n\n\n\t\t\t\tWin\n\n\n") : d[4];
			}
		}

		if (p == 1)
			m[i] = ((m[i] == '_' || m[i] == '^' || m[i] == '#') && m[i - 162] == '*') ? throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n") : ((d[0] % 4 == 1) ? ((Console.KeyAvailable && Console.ReadKey(true).KeyChar == ' ' && false) ? m[i] : m[i]) : ((d[0] % 16 == 3) ? ((m[i] == '<') ? '/' : ((m[i] == '>') ? '\\' : ((m[i] == '/' && m[i - 81] == '*') ? '<' : ((m[i] == '\\' && m[i - 81] == '*') ? '>' : ((m[i] == ' ' && m[i + 2] == '0' && m[i + 81] == '/') ? '\\' : (m[i] == '/' && m[i - 81] == '\\') ? ' ' : ((m[i] == ' ' && m[i - 2] == '0' && m[i + 81] == '\\') ? '/' : ((m[i] == '\\' && m[i - 81] == '/') ? ' ' : ((m[i] == ' ' && m[i - 81] == '\\' && m[i - 79] == '0') ? '/' : ((m[i] == ' ' && m[i - 81] == '/' && m[i - 83] == '0') ? '\\' : m[i]))))))))) : m[i]));

		if (p == 2)
			m[i] = (d[0] % 16 == 3) ? ((m[i] == '\\' && m[i + 81] == '/') ? ' ' : ((m[i] == '/' && m[i + 81] == '\\') ? ' ' : m[i])) : m[i];

		if (p == 3)
			m[i] = (m[i] == '~' && (m[i - 162] == '*' || m[i - 163] == '*' || m[i - 161] == '*' || m[i - 164] == '*' || m[i - 160] == '*')) ? ' ' : ((m[i] == ' ' && m[i + 81] == '^' && d[2] == ' ' && !m.Contains('!')) ? '!' : ((m[i] == '~' && m[i + 81] == '!') ? 'l' : ((m[i] == '!' && (m[i - 81] == 'l' || m[i - 81] == '\"')) || (m[i] == 'l')) ? ' ' : ((m[i] == '+' && (m[i + 1] == '+' || m[i + 1] == ' ') && (m[i - 1] == '+' || m[i - 1] == ' ') && (m[i + 81] == '+' || m[i + 81] == ' ') && (m[i - 81] == '+' || m[i - 81] == ' ')) ? ' ' : m[i])));

		if (p == 4)
			m[i] = (d[0] % 4 == 1) ? ((m[i] == ' ' && m[i + 81] == '!') ? '!' : ((m[i] == '!' && m[i - 81] == '!') ? ' ' : m[i])) : ((d[0] % 64 == 7 && Random.Shared.Next(0, 100) > 50) ? ((m[i] == ' ' && m[i - 81] == '<' && m[i + 81] != '*' && m[i + 82] != '*' && m[i + 80] != '*') ? 'o' : m[i]) : m[i]);

		if (p == 5)
			m[i] = (d[0] % 4 == 1) ? (m[i] >= '*' && m[i] <= '\\' && m[i + 81] == '!') ? '+' : ((m[i] == '!' && m[i - 81] == '+') ? ' ' : m[i]) : ((m[i] >= '*' && m[i] <= '\\' && (m[i + 1] == '+' || m[i - 1] == '+' || m[i + 81] == '+' || m[i - 81] == '+')) ? '+' : m[i]);

		if (p == 6)
			m[j] = (d[0] % 4 == 1) ? ((m[j] == ' ' && m[j - 81] == 'o') ? 'o' : ((m[j] == 'o' && m[j + 81] == 'o') ? ' ' : ((m[j] == 'o' && ((m[j + 81] >= '*' && m[j + 81] <= '\\') || (m[j + 1] >= '*' && m[j + 1] <= '\\') || (m[j - 1] >= '*' && m[j - 1] <= '\\'))) ? ' ' : ((m[j] == 'o' && (m[j + 81] == '^' || m[j + 81] == '#')) ? throw new Exception("\n\n\n\n\t\t\t\tGame Over\n\n\n") : ((m[j] == '~' && m[j - 81] == 'o') ? 'l' : (((m[j] == 'o' && (m[j + 81] == 'l' || m[j + 81] == '_')) || (m[j] == 'o')) ? ' ' : m[j])))))) : m[j];

		if (p == 7)
			m[j] = (d[0] % 16 == 5) ? (((d[1] % 18) > 0 && (d[1] % 18) < 9) ? (((m[j] == ' ' || (m[j] >= '*' && m[j] <= '\\')) && m[j - 1] >= '*' && m[j - 1] <= '\\') ? m[j - 1] : ((m[j] >= '*' && m[j] <= '\\' && (m[j - 1] == ' ' || m[j - 1] == '\"')) ? ' ' : m[j])) : (((d[1] % 18) == 9 || (d[1] % 18) == 0) ? (((m[j] == ' ' || (m[j] >= '*' && m[j] <= '\\')) && m[j - 81] >= '*' && m[j - 81] <= '\\') ? m[j - 81] : ((m[j] >= '*' && m[j] <= '\\' && (m[j - 81] == ' ' || m[j - 81] == '\"')) ? ' ' : m[j])) : m[j])) : ((d[2] == 'd' && d[3] < 74) ? (((m[j] == ' ' || m[j] == '#' || m[j] == '^') && (m[j - 1] == '#' || m[j - 1] == '^')) ? m[j - 1] : (((m[j] == '#' || m[j] == '^') && (m[j - 1] == ' ' || m[j - 1] == '\"')) ? ' ' : m[j])) : m[j]);

		if (p == 8)
			m[i] = (d[0] % 16 == 5) ? (((d[1] % 18) > 9) ? (((m[i] == ' ' || (m[i] >= '*' && m[i] <= '\\')) && m[i + 1] >= '*' && m[i + 1] <= '\\') ? m[i + 1] : ((m[i] >= '*' && m[i] <= '\\' && (m[i + 1] == ' ' || m[i + 1] == '\"')) ? ' ' : m[i])) : m[i]) : ((d[2] == 'a' && d[3] > 5) ? (((m[i] == ' ' || m[i] == '#' || m[i] == '^') && (m[i + 1] == '#' || m[i + 1] == '^')) ? m[i + 1] : (((m[i] == '#' || m[i] == '^') && (m[i + 1] == ' ')) ? ' ' : m[i])) : m[i]);
	}
}