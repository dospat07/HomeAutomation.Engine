Конфигуриране на мрежа времено 
sudo ifconfig eth0 132.83.61.77 netmask 255.255.255.0
sudo route add default gw 132.83.61.178 eth0

Инстанлиране на net.core runtime 2.2.1
1. sudo apt-get install curl libunwind8 gettext
2. wget https://download.visualstudio.microsoft.com/download/pr/9d049226-1f28-4d3d-a4ff-314e56b223c5/f67ab05a3d70b2bff46ff25e2b3acd2a/aspnetcore-runtime-2.2.1-linux-arm.tar.gz
3. mkdir -p $HOME/netcore
4. tar zxf aspnetcore-runtime-2.2.1-linux-arm.tar.gz -C $HOME/netcore
5  sudo ln -s  $HOME/netcore/dotnet /bin/dotnet

Publish HomeAutomation via Visual Studio

1 right click on HomeAutomation.Engine project => then click Publish > then press button Publish

как изглежда  linux-arm.pubxml
<?xml version="1.0" encoding="utf-8"?>
<!--
This file is used by the publish/package process of your Web project. You can customize the behavior of this process
by editing this MSBuild file. In order to learn more about this please visit https://go.microsoft.com/fwlink/?LinkID=208121. 
-->
<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <WebPublishMethod>FileSystem</WebPublishMethod>
    <PublishProvider>FileSystem</PublishProvider>
    <LastUsedBuildConfiguration>Release</LastUsedBuildConfiguration>
    <LastUsedPlatform>Any CPU</LastUsedPlatform>
    <SiteUrlToLaunchAfterPublish />
    <LaunchSiteAfterPublish>True</LaunchSiteAfterPublish>
    <ExcludeApp_Data>False</ExcludeApp_Data>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <RuntimeIdentifier>linux-arm</RuntimeIdentifier>
    <ProjectGuid>44a55a69-a8d9-436b-886e-9fed0de05fda</ProjectGuid>
    <SelfContained>false</SelfContained>
    <_IsPortable>true</_IsPortable>
    <publishUrl>bin\Release\netcoreapp2.2\rpi\</publishUrl>
    <DeleteExistingFiles>True</DeleteExistingFiles>
  </PropertyGroup>
</Project>

2 Копиране на съдържанието от <publishUrl> в папка на rpi примерно /home/pi/ha/Engine

Стартиране

1 nano /home/pi/ha/Engine/startEngine.sh
cd /home/pi/ha/Engine
dotnet HomeAutomation.Engine.dll
2 chmod +x /home/ha/Engine/startEngine.sh 
3 sudo nano /etc/rc.local
/home/pi/ha/Engine/startEngine.sh & exit 0


Timezone

sudo rm /etc/localtime
sudo ln -s /usr/share/zoneinfo/Europa/Sofia /etc/localtime

NGNIX

1 инсталиране :sudo apt-get install nginx
2 съдържание на /etc/nginx/sites-available/default
server {
        listen 80;

        root /home/pi/ha/www;

        # Add index.php to the list if you are using PHP
        index index.html

        server_name localhost;

        location / {
                # First attempt to serve request as file, then
                # as directory, then fall back to displaying a 404.
                try_files $uri /index.html;
        }

}

   
3  sudo ln -s /etc/nginx/sites-available/default /etc/nginx/sites-enabled/
