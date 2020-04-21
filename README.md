# Fake logger using Serilog and Graylog
.Net Core Console application that generates logs with Serilog and sends them to Graylog. 
 
This projet is great to test Graylog in a .Net core solution.

The console app generate 
- random messages (using Bogus nuget package)
- random level (information, warning, error)
- random delay between each logs.

Logs are output to the console and to Graylog.

# How to use it

1. Launch the docker compose file
```
docker-compose up
```
2. Open the solution in the src folder with Visual Studio (2017/2019) and launch the console app.
3. In a browser (avoid Edge), open graylog at this url : https://localhost:9000
4. Use the default credential (admin/admin)
5. Enjoy!
