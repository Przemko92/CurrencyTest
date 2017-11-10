FROM microsoft/aspnetcore:2.0
ENV ASPNETCORE_URLS="http://*:5000"
ENV AVAIBLE_CURRENCIES="USD,PLN,GBP,EUR"
WORKDIR /app
EXPOSE 5000
COPY ./Publish/Docker .
ENTRYPOINT ["dotnet", "CurrencyTest.dll"]