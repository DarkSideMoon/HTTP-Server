;------------------------------------------------------------------------------
;   ���������� ��������� ���������
;------------------------------------------------------------------------------
                                          
#define Dir "..\HTTP-Server\HTTP-Server\bin\Release"
#define Path "..\HTTP-Server\HTTP-Server\bin\Release\HTTP-Server.exe"
#define PathToNet "D:\GitHub\InstallationScripts\"
#define Name GetStringFileInfo(Path, "ProductName")
#define Publisher GetStringFileInfo(Path, "CompanyName")
#define ExeName Name + ".exe"     
#define AppVersion GetFileVersion(Path)
#define URL "https://github.com/DarkSideMoon"
#define GUID "F829E35A-E5AB-4ACB-8A32-3DA72881CBBF"

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
OutputDir=D:\GitHub\Installation
OutputBaseFileName=Installation

; ���� ������
SetupIconFile=D:\GitHub\Media\web.ico

; ��������� ������
Compression=lzma
SolidCompression=yes

;------------------------------------------------------------------------------
;   ������������� ����� ��� �������� ���������
;------------------------------------------------------------------------------

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "License_ENG.txt"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"; LicenseFile: "License_RUS.txt"
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

; .NET Framework 4.0
;Source: "D:\GitHub\InstallationScripts\dotNetFx45_Full_setup.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall; Check: not IsRequiredDotNetDetected

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
[Code]
function isDotNetDetected(): Boolean;
var
  reg_key: string;
  key_value: cardinal;
  sub_key: string;
  success: boolean;
begin
  reg_key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\';
  sub_key := 'v4\Full';
  reg_key := reg_key + sub_key;
  success := RegQueryDWordValue(HKLM, reg_key, 'Install', key_value);
  success := success and (key_value = 1);
  result := success;
end;

function GetUninstallString: string;
var
  sUnInstPath: string;
  sUnInstallString: String;
begin
  Result := '';
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#GUID}_is1'); //Your App GUID/ID
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

function IsUpgrade: Boolean;
begin
  Result := (GetUninstallString() <> '');
end;

function InitializeSetup(): boolean;
var
  V: Integer;
  iResultCode: Integer;
  sUnInstallString: string;
begin
  Result := True; // in case when no previous version is found
  if RegValueExists(HKEY_LOCAL_MACHINE,'Software\Microsoft\Windows\CurrentVersion\Uninstall\{#GUID}_is1', 'UninstallString') then  //Your App GUID/ID
  begin
    V := MsgBox(ExpandConstant('Hey! An old version of app was detected. Do you want to uninstall it?'), mbInformation, MB_YESNO); //Custom Message if App installed
    if V = IDYES then
    begin
      sUnInstallString := GetUninstallString();
      sUnInstallString :=  RemoveQuotes(sUnInstallString);
      Exec(ExpandConstant(sUnInstallString), '', '', SW_SHOW, ewWaitUntilTerminated, iResultCode);
      Result := True;
    end
    else
      Result := False; //when older version present and not uninstalled
  end;
  if not isDotNetDetected() then
    begin
      MsgBox('{#Name} requires Microsoft .NET Framework 4.0 Full Profile.'#13#13
             'You can download it here: http://www.microsoft.com/en-us/download/details.aspx?id=17851', mbInformation, MB_OK);
    end;   

  result := true;
end;
[Run]
;------------------------------------------------------------------------------
;   ������ ������� ����� �����������
;------------------------------------------------------------------------------
Filename: "{app}\{#ExeName}"; Parameters: "/q:a /c:""install /l /q"""; Flags: nowait postinstall skipifsilent
;Check: not IsRequiredDotNetDetected; StatusMsg: Microsoft Framework 4.0 is installed. Please wait.
