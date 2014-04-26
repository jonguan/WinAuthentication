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


SSL Pinning:

5. Windows phone has issues with SSL pinning: http://stackoverflow.com/questions/17741740/read-ssl-certificate-details-on-wp8

6. Confirmed by MSFT employee: 
http://social.msdn.microsoft.com/Forums/wpapps/en-US/34b18974-f056-4717-a0d6-68c6ea2cbdd4/how-can-i-read-ssl-certificate-details-on-my-windows-phone-app?forum=wpdevelop

7. From the docs: http://social.msdn.microsoft.com/Forums/wpapps/en-US/34b18974-f056-4717-a0d6-68c6ea2cbdd4/how-can-i-read-ssl-certificate-details-on-my-windows-phone-app?forum=wpdevelop

"Although you can install trusted certificates on the Windows Phone, in the current release, the Windows Phone app platform does not expose those certificates’ values to apps. As a result, in the current release, you cannot implement mutual authentication scenarios – scenarios in which the client sends its own certificates to the web service in addition to receiving one -- using certificates installed in the root store."

