FROM registry.es.gov.br/espm/infraestrutura/containers/microsoft/dotnet:2.2-sdk

RUN mkdir -p /usr/app/src
WORKDIR /usr/app

COPY . ./
RUN dotnet restore

CMD ["dotnet","run"]
