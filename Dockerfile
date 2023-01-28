FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY BlogWebApi/BlogWebApi/dist /app/
EXPOSE 5000
ENTRYPOINT ["dotnet", "BlogWebApi.dll"]