������������� �� ����� ������� 
sudo ifconfig eth0 132.83.61.77 netmask 255.255.255.0
sudo route add default gw 132.83.61.178 eth0


SQLLite � netcore 2.0 �� ubunto
1 ����������� �� sqlLite � ������������ sudo apt-get install sqlite3 libsqlite3-dev
2 �������� �� ���� ��� ������������ libsqlite3.so.0 � ������������ �� ������������
ln -s /usr/lib/arm-linux-gnueabihf/libsqlite3.so.0 e_sqlite3.so
