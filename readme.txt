Конфигуриране на мрежа времено 
sudo ifconfig eth0 132.83.61.77 netmask 255.255.255.0
sudo route add default gw 132.83.61.178 eth0


SQLLite и netcore 2.0 на ubunto
1 Инсталиране на sqlLite и библиотеките sudo apt-get install sqlite3 libsqlite3-dev
2 Създавне на линк към библиотеката libsqlite3.so.0 в диркеторията на приложението
ln -s /usr/lib/arm-linux-gnueabihf/libsqlite3.so.0 e_sqlite3.so

Стартиране

1 nano /home/ha/Engine/startEngine.sh
cd /home/ha/Engine
dotnet HomeAutomation.Engine.dll
2 chmod +x /home/ha/Engine/startEngine.sh 
3 sudo nano /etc/rc.local
/home/ha/Engine/startEngine.sh &
exit 0


Timezone

sudo rm /etc/localtime
sudo ln -s /usr/share/zoneinfo/Europa/Sofia /etc/localtime
