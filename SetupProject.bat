
@echo off
cls
echo ============================================
echo   SETUP DO PROJETO OMNIA - .NET + DOCKER
echo ============================================
echo.

REM Passo 0: Clonar o repositorio
SET REPO_URL=https://github.com/FabriciOliver/Omnia.git
SET REPO_DIR=Omnia

IF EXIST "%REPO_DIR%" (
    echo Repositorio '%REPO_DIR%' ja existe. Pulando clone.
) ELSE (
    echo Clonando repositorio...
    git clone %REPO_URL%
    IF %ERRORLEVEL% NEQ 0 (
        echo [ERRO] Falha ao clonar o repositorio.
        echo Pressione qualquer tecla para sair...
        pause >nul
        exit /b
    )
)
cd %REPO_DIR%\template\backend

echo.

REM Passo 1: Verificar Docker
echo Verificando se o Docker esta em execucao...
docker info >nul 2>&1
IF %ERRORLEVEL% NEQ 0 (
    echo [ERRO] Docker Desktop nao esta ativo. Inicie o Docker antes de continuar.
    echo Pressione qualquer tecla para sair...
    pause >nul
    exit /b
)
echo Docker esta rodando!
echo.

REM Passo 2: Subir containers
echo Subindo os containers via docker-compose...
docker-compose down -v
docker-compose up -d --build
IF %ERRORLEVEL% NEQ 0 (
    echo [ERRO] Falha ao subir os containers Docker.
    echo Pressione qualquer tecla para sair...
    pause >nul
    exit /b
)
echo Containers iniciados com sucesso!
echo.
echo Pressione qualquer tecla para continuar ou aguarde 5 segundos...
choice /C X /T 5 /D X /N >nul

REM Passo 3: Restaurar dependencias
echo Restaurando dependencias do .NET...
dotnet restore Ambev.DeveloperEvaluation.sln
echo.

REM Passo 4: Compilar o projeto
echo Compilando o projeto...
dotnet build Ambev.DeveloperEvaluation.sln
IF %ERRORLEVEL% NEQ 0 (
    echo [ERRO] Falha ao compilar a solucao.
    echo Pressione qualquer tecla para sair...
    pause >nul
    exit /b
)
echo Compilacao concluida com sucesso!
echo.
echo Pressione qualquer tecla para continuar ou aguarde 5 segundos...
choice /C X /T 5 /D X /N >nul

REM Passo 5: Aplicar migrations
echo Aplicando migrations ao banco de dados...
cd src\Ambev.DeveloperEvaluation.WebApi
dotnet ef database update --project ../Ambev.DeveloperEvaluation.ORM --startup-project .
IF %ERRORLEVEL% NEQ 0 (
    echo [ERRO] Falha ao aplicar migrations.
    echo Pressione qualquer tecla para sair...
    pause >nul
    exit /b
)
echo Migrations aplicadas com sucesso!
cd ../..
echo.
echo Pressione qualquer tecla para continuar ou aguarde 5 segundos...
choice /C X /T 5 /D X /N >nul

REM Passo 6: Rodar o projeto
echo Iniciando o projeto...
echo Aguardando 5 segundos antes de abrir o navegador...
timeout /T 5 /NOBREAK >nul
start http://localhost:5119/swagger
dotnet run --project src\Ambev.DeveloperEvaluation.WebApi
echo.

REM Mensagem final
echo ============================================
echo     START DO PROJETO EFETUADO COM SUCESSO
echo ============================================
echo Pressione qualquer tecla para finalizar...
pause >nul
