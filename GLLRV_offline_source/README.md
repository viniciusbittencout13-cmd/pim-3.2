
# GLLRV - Offline (WPF, JSON)
- Login: admin / admin
- Modo: Offline (sem banco)
- Pastas `data/` copiadas junto do .exe

## Como gerar o .exe sem instalar nada no seu PC (via GitHub Actions)
1) Crie um repositório no GitHub (privado mesmo).
2) Envie estes arquivos (tudo deste ZIP) para o repo.
3) No GitHub, vá em **Actions** → escolha **build-offline-win64** → **Run workflow**.
4) Ao concluir, baixe o artefato **GLLRV_offline_win64** (contém o `GLLRV.exe`).

## Como gerar localmente (se quiser)
```powershell
dotnet publish GLLRV.DesktopApp/GLLRV.DesktopApp.csproj -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishReadyToRun=true -o publish/win-x64
```
