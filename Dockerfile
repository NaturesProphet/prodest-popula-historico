FROM registry.es.gov.br/espm/dockers/microsoft/dotnet

RUN mkdir -p /usr/app/src
WORKDIR /usr/app

COPY . ./
RUN dotnet restore

CMD ["dotnet","run"]
