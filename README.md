WinAuthentication
=================

SSL Pinning for Windows Phone

Learn the lesson from IIS Express servers via 
http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj684580(v=vs.105).aspx

Basically, Windows Phone 8 Emulator runs on localhost, so you cant navigate to localhost.
Instead, navigate to the IP address (ex http://192.168.159.140), found with ipconfig


1. Install Apache 2.2 on Windows
2. Follow instructions here: http://rubayathasan.com/tutorial/apache-ssl-on-windows/
3. Turn off IIS: http://www.sitepoint.com/unblock-port-80-on-windows-run-apache/
4. For 64 bit windows, fix the SessionCache directory issue: https://wiki.apache.org/httpd/SSLSessionCache
5. 
