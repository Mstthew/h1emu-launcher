﻿using System;

namespace H1EmuLauncher.Classes
{
    class Info
    {
        public static string APPLICATION_DATA_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string NODEJS_VERSION = "18.12.1";

        public static string H1EMU_SERVER_IP = "loginserver.h1emu.com:1115";

        public static string SERVER_JSON_API = "https://api.github.com/repos/QuentinGruber/h1z1-server/releases/latest";

        public static string LAUNCHER_JSON_API = "https://api.github.com/repos/H1emu/h1emu-launcher/releases/latest";

        public static string SERVER_BUG_LINK = "https://github.com/QuentinGruber/h1z1-server/issues/new?assignees=&labels=bug&template=bug_report.md&title=";

        public static string LAUNCHER_BUG_LINK = "https://github.com/H1emu/h1emu-launcher/issues/new";

        public static string ACCOUNT_KEY_LINK = "https://www.h1emu.com/us/cockpit/my/account-key/";

        public static string ACCOUNT_KEY_CHECK_API = "https://www.h1emu.com/us/thermos/keyvalidator/?key=";

        public static string DISCORD_LINK = "https://discord.com/invite/RM6jNkj";

        public static string CHANGELOG = "https://www.h1emu.com/us/changelog/";

        public static string WEBSITE = "https://h1emu.com/";

        public static string GAME_CRASH_URL = "https://www.h1emu.com/us/game-error?code=G";

        public static string H1EMU_CHINESE_LINK = "https://azaz.ge/";
    }
}