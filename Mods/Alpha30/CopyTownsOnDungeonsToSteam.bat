xcopy "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\TownsOnDungeons\version_info" "C:\KeeperRLMods\Alpha30\TownsOnDungeons\version_info" /y
RMDIR /S /Q "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\TownsOnDungeons\"
xcopy /s /i "C:\KeeperRLMods\Alpha30\TownsOnDungeons" "C:\Program Files (x86)\Steam\steamapps\common\KeeperRL\mods\TownsOnDungeons"
pause