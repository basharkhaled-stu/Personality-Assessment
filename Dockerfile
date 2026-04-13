# المرحلة 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# انسخ كل المشروع مرة واحدة لتجنب مشاكل COPY لكل .csproj
COPY . .

# Restore لكل الحل
RUN dotnet restore PersonalityAssessment.sln

# Build + Publish مشروع API فقط
RUN dotnet publish PersonalityAssessment.Api/PersonalityAssessment.Api.csproj -c Release -o /app/out

# المرحلة 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# اجعل API يستمع لكل الشبكات داخل الكونتينر
ENV ASPNETCORE_URLS=http://+:80

# افتح البورت 80
EXPOSE 80

# شغّل API
ENTRYPOINT ["dotnet", "PersonalityAssessment.Api.dll"]