;------------------------------------------------------------------------------
;   ���������� ��������� ���������
;------------------------------------------------------------------------------
                                          
#define Dir "..\HTTP-Server\HTTP-Server\bin\Release"
#define Path "..\HTTP-Server\HTTP-Server\bin\Release\HttpServer.exe"
#define Name "HttpServer"
#define ExeName Name + ".exe"     
#define AppVersion GetFileVersion(Path)
#define URL "https://github.com/DarkSideMoon"
#define Publisher "Pavel Romashchenko"
#define AppVersion "1.0.1.0"



;------------------------------------------------------------------------------
;   ��������� ���������
;------------------------------------------------------------------------------

[Setup]
; ���������� ������������� ����������, 
;��������������� ����� Tools -> Generate GUID
AppId={{F829E35A-E5AB-4ACB-8A32-3DA72881CBBF}

; ������ ����������, ������������ ��� ���������
AppName={#Name}
AppVersion={#AppVersion}
AppPublisher={#Publisher}
AppPublisherURL={#URL}
AppSupportURL={#URL}
AppUpdatesURL={#URL}

; ���� ��������� ��-���������
DefaultDirName={pf}\{#Name}
; ��� ������ � ���� "����"
DefaultGroupName={#Name}

; �������, ���� ����� ������� ��������� setup � ��� ������������ �����
OutputDir=..\http-server\HTTP-Server\bin\
OutputBaseFileName=Installation

; ���� ������
SetupIconFile=External\Install\web.ico

; ��������� ������
Compression=lzma
SolidCompression=yes

;------------------------------------------------------------------------------
;   ������������� ����� ��� �������� ���������
;------------------------------------------------------------------------------

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "External\Install\License_ENG.txt"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"; LicenseFile: "External\Install\License_RUS.txt"
;------------------------------------------------------------------------------
;   ����������� - ��������� ������, ������� ���� ��������� ��� ���������
;------------------------------------------------------------------------------

[Tasks]
; �������� ������ �� ������� �����
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

;------------------------------------------------------------------------------
;   �����, ������� ���� �������� � ����� �����������
;------------------------------------------------------------------------------

[Files]
; ����������� ����
Source: "{#Path}"; DestDir: "{app}"; Flags: ignoreversion

; ������������� �������
Source: "{#Dir}*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs


;------------------------------------------------------------------------------
;   ��������� �����������, ��� �� ������ ����� ������
;------------------------------------------------------------------------------ 
[Icons]

Name: "{group}\{#Name}"; Filename: "{app}\{#ExeName}"

Name: "{commondesktop}\{#Name}"; Filename: "{app}\{#ExeName}"; Tasks: desktopicon

;------------------------------------------------------------------------------
;   ������ ���� ���������� �� ���������� �����
;------------------------------------------------------------------------------
[Code]
#include "External\Install\dotnet.pas"
#include "External\Install\delversion.pas"
[Run]
;------------------------------------------------------------------------------
;   ������ ������� ����� �����������
;------------------------------------------------------------------------------
Filename: {tmp}\dotNetFx45_Full_setup.exe; Parameters: "/q:a /c:""install /l /q"""; Check: not IsRequiredDotNetDetected; StatusMsg: Microsoft Framework 4.0 is installed. Please wait.
